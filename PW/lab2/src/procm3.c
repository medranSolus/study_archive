#include <stdio.h>
#include <stdlib.h>
#include <unistd.h> 
#include <sys/types.h>
#include <sys/wait.h>
#include <errno.h>
#include <string.h>

int main(int argc, char* argv[])
{
    if (argc == 1)
    {
        printf("Proces macierzysty krok 0\n");
        return -1;
    }
    const int child_count = argc - 2;
    for (int i = 1; i <= child_count; ++i)
        if (fork() == 0)
        {
            char buff[13];
            sprintf(buff, "%d", i);
            if (execl("proc_pot", "proc_pot", buff, argv[i + 1], (char*)NULL) == -1)
            {
                if (errno == 2)
                    execl("bin/proc_pot", "proc_pot", buff, argv[i + 1], (char*)NULL);
                printf("Error: %s\n", strerror(errno));
                return -2;
            }
        }
    const int steps = atoi(argv[1]);
    for (int i = 1; i <= steps; ++i)
    {
        printf("Proces macierzysty krok %d\n", i);
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