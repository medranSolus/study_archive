#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h> 
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <sys/wait.h>
#include <sys/time.h>

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

const char* input_fifo = "bin/input.fif";
const char* output_fifo = "bin/output.fif";

void run_jobs(job_result job, unsigned int proc, unsigned int length);
unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end);

int main(int argc, char* argv[])
{
    if (argc == 5)
    {
        job_result job_data;
        job_data.start = abs(atoi(argv[1]));
        job_data.end = abs(atoi(argv[2])) + 1;
        unsigned int length = abs(atoi(argv[4]));
        struct timespec start, end;
        clock_gettime(CLOCK_MONOTONIC, &start);
        if (mkfifo(input_fifo, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH) == -1)
        {
            perror("mkfifo");
            return -2;
        }
        if (mkfifo(output_fifo, S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH) == -1)
        {
            perror("mkfifo");
            return -3;
        }
        if (fork() == 0)
            run_jobs(job_data, abs(atoi(argv[3])), length);
        job_data.count = job_data.end - job_data.start;
        unsigned int jobs_count = job_data.count / length + (job_data.count % length > 0 ? 1 : 0);
        int result = open(output_fifo, O_RDWR);
        if (result == -1)
        {
            perror("open");
            fprintf(stderr, "Cannot open fifo: %s\n", output_fifo);
            return -4;
        }
        unsigned int first_count = 0;
        for (unsigned int i = 0; i < jobs_count; ++i)
        {
            if (read(result, &job_data, sizeof(job_result)) == -1)
            {
                perror("read");
                return -5;
            }
            first_count += job_data.count;
        }
        close(result);
        wait(NULL);
        unlink(input_fifo);
        unlink(output_fifo);
        clock_gettime(CLOCK_MONOTONIC, &end);
        printf("Time: %u us\nCount: %u\n", get_microsec_diff(&start, &end), first_count);
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}

void run_jobs(job_result job, unsigned int proc, unsigned int length)
{
    int job_out = open(input_fifo, O_RDWR);
    if (job_out == -1)
    {
        perror("open");
        fprintf(stderr, "Cannot open fifo: %s\n", input_fifo);
        exit(-6);
    }
    for (unsigned int i = 0; i < proc; ++i)
    {
        if (fork() == 0)
        {
            execl("bin/licz", "licz", NULL);
            perror("execl");
            exit(-1);
        }
    }
    unsigned int end = job.end;
    do
    {
        job.end = job.start + length;
        if (job.end > end)
            job.end = end;
        if (write(job_out, &job, sizeof(job_result)) == -1)
        {
            perror("write");
            exit(-7);
        }
        job.start = job.end;
    } while (job.end < end);
    job.start = 0;
    job.end = 0;
    for (unsigned int i = 0; i < proc; ++i)
    {
        if (write(job_out, &job, sizeof(job_result)) == -1)
        {
            perror("write");
            exit(-8);
        }
    }
    close(job_out);
    for (unsigned int i = 0; i < proc; ++i)
        wait(NULL);
    exit(0);
}

unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end)
{
    return (end->tv_nsec - start->tv_nsec) / 1000 + (end->tv_sec - start->tv_sec) * 1000000;
}