#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>

typedef struct
{
    char more_junk[4096];
} junk;
#define JUNK_COUNT 32

int main()
{
    junk stuff;
    int pipe_desc[2];
    if (pipe(pipe_desc) == -1)
    {
        perror("pipe");
        return -1;
    }
    if (fork() == 0)
    {
        for (unsigned char i = 0; i < JUNK_COUNT; ++i)
        {
            if (read(pipe_desc[0], &stuff, sizeof(stuff)) == -1)
            {
                perror("read");
                return -2;
            }
            printf("%i.Received %li bytes of junk\n", i + 1, sizeof(junk));
            if (i % 4 && i < 9)
                sleep(5);
        }
        exit(0);
    }
    else
    {
        printf("Capacity test\n\n");
        for (unsigned char i = 0; i < JUNK_COUNT; ++i)
        {
            printf("%i.Writing %li bytes of junk\n", i + 1, sizeof(junk));
            if (write(pipe_desc[1], &stuff, sizeof(stuff)) == -1)
            {
                perror("write");
                return -3;
            }
        }
        printf("%i packets of junk written\n", JUNK_COUNT);
        wait(NULL);
    }
    return 0;
}