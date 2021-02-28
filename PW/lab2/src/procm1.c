#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> 
#include <sys/types.h>
#include <sys/wait.h>

int main(int argc, char* argv[])
{
    if (argc == 1)
    {
        printf("Macierzysty krok 0\n");
        return -1;
    }
    const int child_count = argc - 2;
    for (int i = 1; i <= child_count; ++i)
    {
        if (fork() == 0)
        {
            const int steps = atoi(argv[i + 1]);
            for (int j = 1; j <= steps; ++j)
            {
                printf("Proces %d krok %d\n", i, j);
                sleep(1);
            }
            exit(i);
        }
    }
    const int steps = atoi(argv[1]);
    for (int i = 1; i <= steps; ++i)
    {
        printf("Macierzysty krok %d\n", i);
        sleep(1);
    }
    int status = 0;
    for (int i = 1; i <= child_count; ++i)
    {
        int pid = wait(&status);
        printf("Potomny %d, pid: %d\n", WEXITSTATUS(status), pid);
    }
    return 0;
}