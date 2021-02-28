#define _POSIX_C_SOURCE 200809L
#include <stdio.h>
#include <unistd.h> 
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/wait.h>

typedef struct
{
    char more_junk[4096];
} junk;
#define JUNK_COUNT 32

const char* fifo = "bin/capactiy.fif";

int main()
{
    junk stuff;
    if (mkfifo(fifo, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH) == -1)
    {
        perror("mkfifo");
        return -1;
    }
    if (fork() == 0)
    {
        int fd = open(fifo, O_RDWR);
        if (fd == -1)
        {
            perror("open");
            return -2;
        }
        for (unsigned char i = 0; i < JUNK_COUNT; ++i)
        {
            if (read(fd, &stuff, sizeof(stuff)) == -1)
            {
                perror("read");
                return -3;
            }
            printf("%i.Received %li bytes of junk\n", i + 1, sizeof(junk));
            if (i % 4 && i < 9)
                sleep(5);
        }
    }
    else
    {
        printf("Capacity test\n\n");
        int fd = open(fifo, O_RDWR);
        if (fd == -1)
        {
            perror("open");
            return -4;
        }
        for (unsigned char i = 0; i < JUNK_COUNT; ++i)
        {
            printf("%i.Writing %li bytes of junk\n", i + 1, sizeof(junk));
            if (write(fd, &stuff, sizeof(stuff)) == -1)
            {
                perror("write");
                return -5;
            }
        }
        close(fd);
        printf("%i packets of junk written\n", JUNK_COUNT);
        wait(NULL);
        unlink(fifo);
    }
    return 0;
}