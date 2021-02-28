#define _GNU_SOURCE
#include <stdbool.h>
#include <stdio.h>
#include <fcntl.h>
#include <unistd.h>
#include <sys/stat.h>
#include <sys/types.h>

int main(int argc, char *argv[])
{
    if (argc == 2)
    {
        int file = open(argv[1], O_RDONLY | __O_PATH | __O_NOFOLLOW);
        if (file == -1)
        {
            perror("open");
            fprintf(stderr, "Cannot open file: %s\n", argv[1]);
            return -2;
        }
        struct stat file_stat;
        if (fstat(file, &file_stat) == -1)
        {
            perror("fstat");
            return -3;
        }
        close(file);
        printf("Plik: %s\n", argv[1]);
        printf("Rozmiar: %d\n", file_stat.st_size);
        printf("Liczba hard linków: %d\n", file_stat.st_nlink);
        printf("Atrybuty: %c%c%c%c%c%c%c%c%c\n",
            file_stat.st_mode & S_IRUSR ? 'r' : '-',
            file_stat.st_mode & S_IWUSR ? 'w' : '-',
            file_stat.st_mode & S_IXUSR ? 'x' : '-',
            file_stat.st_mode & S_IRGRP ? 'r' : '-',
            file_stat.st_mode & S_IWGRP ? 'w' : '-',
            file_stat.st_mode & S_IXGRP ? 'x' : '-',
            file_stat.st_mode & S_IROTH ? 'r' : '-',
            file_stat.st_mode & S_IWOTH ? 'w' : '-',
            file_stat.st_mode & S_IXOTH ? 'x' : '-');
        printf("Link: %s\n", S_ISLNK(file_stat.st_mode) ? "Tak" : "Nie");
        return 0;
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}