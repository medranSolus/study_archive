#include <string>
#include <unistd.h>
#include <sys/wait.h>
#define program "./bin/testGen"
using std::to_string;

int main()
{
    uint64_t sizes[]{100, 200, 500, 1000, 2000, 5000, 10000};
    for (uint64_t i = 0; i < 7; ++i)
    {
        for (uint64_t j = 0; j < 7; ++j)
        {
            int id = fork();
            if (id == 0)
                execl(program, program, to_string(sizes[i]).c_str(), to_string(sizes[j]).c_str(), (to_string(sizes[i]) + "x" + to_string(sizes[j]) + "_seq_test.bmp").c_str(), nullptr);
            wait(&id);
        }
    }
    std::string types[]{"_one", "_max"};
    for (uint8_t type = 0; type <= 1; ++type)
    {
        for (uint64_t i = 0; i < 7; ++i)
        {
            for (uint64_t j = 0; j < 7; ++j)
            {
                int id = fork();
                if (id == 0)
                    execl(program, program, to_string(sizes[i]).c_str(), to_string(sizes[j]).c_str(), (to_string(sizes[i]) + "x" + to_string(sizes[j]) + "_par" + types[type] + "_test.bmp").c_str(), to_string(type), nullptr);
                wait(&id);
            }
        }
    }
    return 0;
}