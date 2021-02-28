#include "utils.h"
#include <sys/time.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <sched.h>
#include <map>
#include <fstream>

void oneRowPerCore(BMP *image, char *name, const std::string &program);
void maxRowPerCore(BMP *image, char *name, const std::string &program);
cpu_set_t **setCpuMasks(int coreCount, size_t size);
void clearCpuMasks(cpu_set_t **masks, int cores);

int main(int argc, char **argv) //system("ulimit -c unlimited); system(sysctl -w kernel.core_pattern=/tmp/core-%e.%p.%h.%t");
{
    if (argc >= 2)
    {
        timespec start, end;
        clock_gettime(CLOCK_BOOTTIME, &start);
        bool rowMode = true, mode = false;
        if (argc >= 3)
            rowMode = atoi(argv[2]);
        if (argc >= 4)
            mode = atoi(argv[3]);
        BMP *image = (BMP *)malloc(sizeof(BMP));
        int8_t status = image->read(argv[1]);
        if (status)
            quit(status, image);
        if (mode)
        {
            BMP *sharedImage = createSharedImage(image, argv[1]);
            if (rowMode)
                maxRowPerCore(sharedImage, argv[1], "negParRow");
            else
                oneRowPerCore(sharedImage, argv[1], "negParRow");
            sharedImage = openSharedImage(argv[1]);
            sharedImage->write(argv[1]);
            if (closeSharedImage(sharedImage))
                return CANNOT_CLOSE_MEMORY;
            destroySharedMemory(argv[1]);
        }
        else
        {
            if (rowMode)
                maxRowPerCore(image, argv[1], "negParRowLoad");
            else
                oneRowPerCore(image, argv[1], "negParRowLoad");
            for (size_t i = 0, rows = image->rows(); i < rows; ++i)
                image->readRow(argv[1], i);
            image->write(argv[1]);            
        }
        int32_t width = image->columns();
        int32_t height = image->rows();
        free(image);
        clock_gettime(CLOCK_BOOTTIME, &end);
        unsigned long diff = (end.tv_nsec - start.tv_nsec) / 1000 + (end.tv_sec - start.tv_sec) * 1000000;
        std::ofstream fout("_imageTime.txt");
        fout << diff << ' ' << width << ' ' << height << std::endl;
        fout.close();
        return SUCCESS;
    }
    else
        return NO_INPUT_PARAMS;
}

cpu_set_t **setCpuMasks(int coreCount, size_t size)
{
    cpu_set_t **cores = new cpu_set_t *[coreCount];
    for (int i = 0; i < coreCount; ++i)
    {
        cores[i] = CPU_ALLOC(coreCount);
        CPU_ZERO_S(size, cores[i]);
        CPU_SET_S(i, size, cores[i]);
    }
    return cores;
}

void clearCpuMasks(cpu_set_t **masks, int cores)
{
    for (int i = 0; i < cores; i++)
        CPU_FREE(masks[i]);
    delete[] masks;
}

void oneRowPerCore(BMP *image, char *name, const std::string &program)
{
    int coreCount = sysconf(_SC_NPROCESSORS_ONLN);
    size_t cpuSize = CPU_ALLOC_SIZE(coreCount);
    cpu_set_t **cores = setCpuMasks(coreCount, cpuSize);
    std::map<pid_t, int> processes;
    int code = CANNOT_CREATE_CHILD;
    uint32_t rows = image->rows();
    if (rows < coreCount)
    {
        for (uint32_t i = 0; i < rows; ++i)
        {
            code = fork();
            if (code == 0)
            {
                system("ulimit -c unlimited");
                execl(("./bin/" + program).c_str(), name, std::to_string(i).c_str(), nullptr);
            }
            else
            {
                sched_setaffinity(code, cpuSize, cores[i]);
                processes.insert(std::pair<pid_t, int>(code, i));
            }
        }
    }
    else
    {
        for (uint32_t i = 0; i < coreCount; ++i)
        {
            code = fork();
            if (code == 0)
            {
                system("ulimit -c unlimited");
                execl(("./bin/" + program).c_str(), name, std::to_string(i).c_str(), nullptr);
            }
            else
            {
                sched_setaffinity(code, cpuSize, cores[i]);
                processes.insert(std::pair<pid_t, int>(code, i));
            }
        }
        for (int32_t i = coreCount, id = -1, cpu = 0; i < rows; ++i)
        {
            id = wait(&code);
            if (id != -1)
            {
                cpu = processes.at(id);
                processes.erase(id);
                id = fork();
                if (id == 0)
                {
                    system("ulimit -c unlimited");
                    execl(("./bin/" + program).c_str(), name, std::to_string(i).c_str(), nullptr);
                }
                else
                {
                    sched_setaffinity(id, cpuSize, cores[cpu]);
                    processes.insert(std::pair<pid_t, int>(id, cpu));
                }
            }
            else
                break;
        }
    }
    while (wait(&code) > 0) {}
    clearCpuMasks(cores, coreCount);
}

void maxRowPerCore(BMP *image, char *name, const std::string &program)
{
    int coreCount = sysconf(_SC_NPROCESSORS_ONLN);
    size_t cpuSize = CPU_ALLOC_SIZE(coreCount);
    cpu_set_t **cores = setCpuMasks(coreCount, cpuSize);
    std::map<pid_t, int> processes;
    uint32_t rows = image->rows();
    int code = CANNOT_CREATE_CHILD, rowsPerCore = rows / coreCount;
    if (rows < coreCount)
    {
        for (uint32_t i = 0; i < rows; ++i)
        {
            code = fork();
            if (code == 0)
            {
                system("ulimit -c unlimited");
                execl(("./bin/" + program).c_str(), name, std::to_string(i).c_str(), nullptr);
            }
            else
            {
                sched_setaffinity(code, cpuSize, cores[i]);
                processes.insert(std::pair<pid_t, int>(code, i));
            }
        }
    }
    else
    {
        for (uint32_t i = 0; i < coreCount; ++i)
        {
            code = fork();
            if (code == 0)
            {
                system("ulimit -c unlimited");
                execl(("./bin/" + program).c_str(), name, std::to_string(i * rowsPerCore).c_str(), std::to_string(rowsPerCore).c_str(), nullptr);
            }
            else
            {
                sched_setaffinity(code, cpuSize, cores[i]);
                processes.insert(std::pair<pid_t, int>(code, i));
            }
        }
        rowsPerCore = rows % coreCount;
        if (rowsPerCore)
        {
            int id = wait(&code);
            int cpu = processes.at(id);
            processes.erase(id);
            id = fork();
            if (id == 0)
            {
                system("ulimit -c unlimited");
                execl(("./bin/" + program).c_str(), name, std::to_string(rows - rowsPerCore).c_str(), std::to_string(rowsPerCore).c_str(), nullptr);
            }
            else
            {
                sched_setaffinity(id, cpuSize, cores[cpu]);
                processes.insert(std::pair<pid_t, int>(id, cpu));
            }
        }
    }
    while (wait(&code) > 0) {}
    clearCpuMasks(cores, coreCount);
}
