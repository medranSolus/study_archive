#include "BMP.h"
#include <fstream>
#include <unistd.h>
#include <sys/wait.h>

int main(int argc, char **argv)
{
    std::string program = "./bin/negSeq";
    if (argc < 4)
        return NO_INPUT_PARAMS;
    bool isPar = false;
    if (argc >= 5)
    {
        isPar = atoi(argv[4]);
        if (isPar)
            program = "./bin/negPar";
    }
    int width = atoi(argv[1]);
    int height = atoi(argv[2]);
    BMP *image = new BMP(width, height);
    image->write(argv[3], false);
    delete image;
    int id = -1;
    long long count = 0;
    unsigned long long testTime = 0;
    unsigned long time = 0;
    std::ifstream fin;
    for (long long i = 0; i < 100; ++i)
    {
        id = fork();
        if (id == 0)
        {
            if (isPar)
            {
                if (argc >= 7)
                    execl(program.c_str(), program.c_str(), argv[3], argv[5], argv[6], nullptr);
                if (argc >= 6)
                    execl(program.c_str(), program.c_str(), argv[3], argv[5], nullptr);
            }
            execl(program.c_str(), program.c_str(), argv[3], nullptr);
        }
        wait(&id);
        if (id == SUCCESS)
        {
            fin.open("_imageTime.txt");
            if (fin.good())
            {
                fin >> time;
                testTime += time;
                ++count;
            }
            fin.close();
        }
    }
    program = argv[3];
    std::ofstream fout(program + "_testTime.txt");
    if (fout.good())
    {
        if (count)
            fout << testTime / count << ' ' << width << ' ' << height;
        fout.close();
        return SUCCESS;
    }
    else
    {
        fout.close();
        return CANNOT_OPEN_FILE;
    }
}