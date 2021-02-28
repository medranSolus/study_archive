#define _DEFAULT_SOURCE
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <fcntl.h>
#include <unistd.h>

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

unsigned int get_first_count(unsigned int start, unsigned int end);
bool is_first(int n);

int main(int argc, char *argv[])
{
    job_result job;
    job.start = abs(atoi(argv[1]));
    job.end = abs(atoi(argv[2]));
    job.count = get_first_count(job.start, job.end);
    int out = open("wynik.bin", O_WRONLY | O_APPEND);
    if (out == -1)
    {
        perror("open");
        fprintf(stderr, "Cannot open file: wynik.bin\n");
        return -1;
    }
    if (lockf(out, F_LOCK, 0) == -1)
    {
        perror("lockf");
        fprintf(stderr, "Error locking file: wynik.bin\n");
        return -2;
    }
    write(out, &job, sizeof(job_result));
    if (lockf(out, F_ULOCK, 0) == -1)
    {
        perror("lockf");
        fprintf(stderr, "Error unlocking file: wynik.bin\n");
        return -4;
    }
    close(out);
    return 0;
}

unsigned int get_first_count(unsigned int start, unsigned int end)
{
    unsigned int count = 0;
    for (; start < end; ++start)
        if (is_first(start))
            ++count;
    return count;
}

bool is_first(int n)
{
    for (int i = 2; i * i <= n; ++i)
        if (n % i == 0)
            return false;
    return true;
}