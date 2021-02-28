#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/wait.h>
#include <unistd.h>

int main()
{
    int pipe_desc[2];
    if (pipe(pipe_desc) == -1)
    {
        perror("pipe");
        return -1;
    }
    if (fork() == 0)
    {
        char c;
        while(true)
        {
            if (read(pipe_desc[0], &c, sizeof(c)) == -1)
            {
                perror("read");
                return -2;
            }
            if (c == '|')
                exit(0);
            printf("%c", c);
        }
    }
    else
    {
        printf("Type to send chars to other program (character '|' ends execution)\n");
        char c;
        do
        {
            c = getchar();
            if (write(pipe_desc[1], &c, sizeof(c)) == -1)
            {
                perror("write");
                return -3;
            }
        } while (c != '|');
        wait(NULL);
    }
    return 0;
}