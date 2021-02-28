#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h> 
#include <sys/types.h>
#include <sys/wait.h>
#include <sys/time.h>
#include <sys/stat.h>
#include <fcntl.h>

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

void run_job(job_result job);
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
        int out_file = creat("wynik.bin", S_IRUSR | S_IWUSR | S_IRGRP | S_IWGRP | S_IROTH | S_IWOTH);
        if (out_file == -1)
        {
            perror("creat");
            fprintf(stderr, "Cannot create file wynik.bin!\n");
            return -2;
        }
        close(out_file);
        for (unsigned int i = 0; i < proc; ++i)
        {
            if (fork() == 0)
            {
                job_data.start = job_data.start + i * job_data.count;
                job_data.end = job_data.start + job_data.count;
                run_job(job_data);
            }
        }
        if (remaining_jobs)
        {
            ++proc;
            if (fork() == 0)
            {
                job_data.start = job_data.end - remaining_jobs;
                run_job(job_data);
            }
        }
        unsigned int first_count = 0;
        for (unsigned int i = 0; i < proc; ++i)
            wait(NULL);
        out_file = open("wynik.bin", O_RDONLY);
        if (out_file == -1)
        {
            perror("open");
            fprintf(stderr, "Cannot open file wynik.bin!\n");
            return -3;
        }
        for (unsigned int i = 0; i < proc; ++i)
        {
            if (read(out_file, &job_data, sizeof(job_result)) == -1)
            {
                perror("read");
                return -4;
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

void run_job(job_result job)
{
    char buff_start[11];
    char buff_end[11];
    sprintf(buff_start, "%u", job.start);
    sprintf(buff_end, "%u", job.end);
    execl("bin/licz", "licz", buff_start, buff_end, NULL);
    perror("execl");
    exit(-1);
}

unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end)
{
    return (end->tv_nsec - start->tv_nsec) / 1000 + (end->tv_sec - start->tv_sec) * 1000000;
}