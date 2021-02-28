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

const char* input_fifo = "bin/input.fif";
const char* output_fifo = "bin/output.fif";

unsigned int get_first_count(unsigned int start, unsigned int end);
bool is_first(int n);

int main(int argc, char *argv[])
{
    int in = open(input_fifo, O_RDWR);
    if (in == -1)
    {
        perror("open");
        fprintf(stderr, "Cannot open fifo: %s\n", input_fifo);
        return -1;
    }
    int out = open(output_fifo, O_RDWR);
    if (out == -1)
    {
        perror("open");
        fprintf(stderr, "Cannot open fifo: %s\n", output_fifo);
        return -2;
    }
    job_result job;
    while (true)
    {
        if (read(in, &job, sizeof(job_result)) == -1)
        {
            perror("read");
            return -3;
        }
        if (job.start == 0 && job.end == 0)
            break;
        job.count = get_first_count(job.start, job.end);
        write(out, &job, sizeof(job_result));
    }
    close(in);
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