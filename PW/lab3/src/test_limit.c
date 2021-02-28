#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <unistd.h>
#include <sys/wait.h>

void cpu_time_waste();
void file_open_limit();
void file_size_limit();
void task_overflow();
void memory_overflow();
void stack_overflow(unsigned long long call);
void help();

int main(int argc, char* argv[])
{
    if (argc > 1)
    {
        for (int i = 1; i < argc; ++i)
        {
            for (int j = 0; argv[i][j] != '\0' ; ++j)
            {
                switch (argv[i][j])
                {
                case 'c':
                {
                    cpu_time_waste();
                    break;
                }
                case 'o':
                {
                    file_open_limit();
                    break;
                }
                case 'f':
                {
                    file_size_limit();
                    break;
                }
                case 'p':
                {
                    task_overflow();
                    break;
                }
                case 'm':
                {
                    memory_overflow();
                    break;
                }
                case 's':
                {
                    stack_overflow(0);
                    break;
                }
                case 'h':
                {
                    help();
                    break;
                }
                case '-':
                    break;
                default:
                {
                    fprintf(stderr, "Unkown argument: %c\n", argv[i][j]);
                    break;
                }
                }
            }
        }
    }
    else
    {
        fprintf(stderr, "No argument specified!\n");
        help();
        return -1;
    }
}

void cpu_time_waste()
{
    while(true)
        printf("Testing time...\n");
}

void file_open_limit()
{
    char name[15] = "test";
    FILE* file = NULL;
    unsigned long i = 0;
    do
    {
        sprintf(name + 4, "%u", i++);
        file = fopen(name, "w");
    } while (file != NULL);
    printf("Cannot open more files than: %u\n", i);
}

void file_size_limit()
{
    FILE* file = fopen("overflow_file", "w");
    long long buffer[1024] = { 0 };
    do
    {
        fwrite (buffer , sizeof(long long), sizeof(buffer), file);
    } while (true);
    fclose(file);
}

void task_overflow()
{
    unsigned long long i = 0;
    int pid = 0;
    bool run = true;
    while (run)
    {
        pid = fork();
        switch (pid)
        {
        case 0:
        {
            unsigned long long i = 0;
            while (true)
            {
                ++i;
            }
            sleep(120);
            exit(0);
        }
        case -1:
        {
            run = false;
            break;
        }        
        default:
            break;
        }
        printf("%u. process %d created\n", i++, pid);
    }
    printf("Cannot create more processes than %u\n", i);
    for (unsigned long long j = 0; j < i; ++j)
        wait(NULL);
    
}

void memory_overflow()
{
    unsigned long long i = 0;
    while (malloc(1024) != NULL)
        ++i;
    printf("Cannot allocate more than %u kbytes\n", i);
}

void stack_overflow(unsigned long long call)
{
    printf("Call %u\n", call);
    stack_overflow(call + 1);
}

void help()
{
    printf("Argument types:\n \
        [-c] Exceed CPU time\n \
        [-o] Break open files limit\n \
        [-f] Exceed file size limit (multiplier of 1024 bytes)\n \
        [-p] Exceed child processes limit\n \
        [-m] Memory overflow\n \
        [-s] Stack overflow\n");
}