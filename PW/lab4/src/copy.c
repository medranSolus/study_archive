#include <stdbool.h>
#include <stdio.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>

int main(int argc, char *argv[])
{
    if (argc == 3)
    {
        unsigned char buffer[512];
        int file1 = open(argv[1], O_RDONLY);
        if (file1 == -1)
        {
            perror("open");
            fprintf(stderr, "Cannot open file: %s\n", argv[1]);
            return -2;
        }
        int file2 = open(argv[2], O_WRONLY | O_TRUNC | O_CREAT, S_IRUSR | S_IWUSR);
        if (file2 == -1)
        {
            perror("open");
            fprintf(stderr, "Cannot open file: %s\n", argv[2]);
            close(file1);
            return -3;
        }
        int ret = -1;
        while (true)
        {
            ret = read(file1, buffer, 512);
            if (ret == -1)
            {
                fprintf(stderr, "Error reading from file: %s\n", argv[1]);
                close(file1);
                close(file2);
                return -4;
            }
            if (write(file2, buffer, ret) == -1)
            {
                fprintf(stderr, "Error writing to file: %s\n", argv[2]);
                close(file1);
                close(file2);
                return -5;
            }
            if (ret < 512)
                break;
        }
        close(file1);
        close(file2);
        return 0;
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}