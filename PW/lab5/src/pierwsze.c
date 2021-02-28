#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h> 
#include <sys/types.h>
#include <sys/wait.h>
#include <sys/time.h>

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

void run_job(job_result job, int out_pipe);
bool is_first(int n);
unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end);

int main(int argc, char* argv[])
{
    if (argc >= 3)
    {
        job_result job_data;
        job_data.start = abs(atoi(argv[1]));
        job_data.end = abs(atoi(argv[2])) + 1;
        unsigned int proc = (argc == 4 ? abs(atoi(argv[3])) : 1);
        struct timespec start, end;
        clock_gettime(CLOCK_MONOTONIC, &start);
        job_data.count = job_data.end - job_data.start;
        if (job_data.count < proc)
            proc = job_data.count;
        unsigned int remaining_jobs = job_data.count % proc;
        job_data.count /= proc;
        int pipe_desc[2];
        if (pipe(pipe_desc) == -1)
        {
            perror("pipe");
            return -2;
        }
        for (unsigned int i = 0; i < proc; ++i)
        {
            if (fork() == 0)
            {
                job_data.start = job_data.start + i * job_data.count;
                job_data.end = job_data.start + job_data.count;
                run_job(job_data, pipe_desc[1]);
            }
        }
        if (remaining_jobs)
        {
            ++proc;
            if (fork() == 0)
            {
                job_data.start = job_data.end - remaining_jobs;
                run_job(job_data, pipe_desc[1]);
            }
        }
        unsigned int first_count = 0;
        for (unsigned int i = 0; i < proc; ++i)
        {
            if (read(pipe_desc[0], &job_data, sizeof(job_result)) == -1)
            {
                perror("read");
                return -3;
            }
            first_count += job_data.count;
        }
        clock_gettime(CLOCK_MONOTONIC, &end);
        printf("Time: %u us\nCount: %u\n", get_microsec_diff(&start, &end), first_count);
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}

void run_job(job_result job, int out_pipe)
{
    job.count = 0;
    for (; job.start < job.end; ++job.start)
        if(is_first(job.start))
            ++job.count;
    if (write(out_pipe, &job, sizeof(job_result)) == -1)
    {
        perror("write");
        exit(-1);
    }
    exit(0);
}

bool is_first(int n)
{
    for (int i = 2; i*i <= n; ++i)
        if (n%i == 0)
            return false;
    return true;
} 

unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end)
{
    return (end->tv_nsec - start->tv_nsec) / 1000 + (end->tv_sec - start->tv_sec) * 1000000;
}