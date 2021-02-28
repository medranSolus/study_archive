#define _POSIX_C_SOURCE 200809L
#include <stdio.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>

int main()
{
    pid_t id = fork();
    if (id == 0)
        printf("Hello child world! PID:%i\n", getpid());
    else
    {
        printf("Hello adult world! PID:%i CHILD_PID:%i\n", getpid(), id);
        wait(NULL);
    }
    return 0;
}