#define _POSIX_C_SOURCE 200809L
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <unistd.h> 
#include <sys/types.h>
#include <sys/wait.h>
#include <sys/time.h>

void run_job(unsigned int first, unsigned int last);
unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end);

int main(int argc, char* argv[])
{
    if (argc >= 3)
    {
        unsigned int first = abs(atoi(argv[1]));
        unsigned int last = abs(atoi(argv[2])) + 1;
        unsigned int proc = (argc == 4 ? abs(atoi(argv[3])) : 1);
        struct timespec start, end;
        clock_gettime(CLOCK_MONOTONIC, &start);
        unsigned int jobs = last - first;
        if (jobs < proc)
            proc = jobs;
        unsigned int remaining_jobs = jobs % proc;
        jobs /= proc;
        for (unsigned int i = 0; i < proc; ++i)
        {
            if (fork() == 0)
            {
                first = first + i * jobs;
                run_job(first, first + jobs);
            }
        }
        if (remaining_jobs)
        {
            ++proc;
            if (fork() == 0)
                run_job(last - remaining_jobs, last);
        }
        unsigned int first_count = 0;
        for (unsigned int i = 0; i < proc; ++i)
        {
            wait(&jobs);
            first_count += WEXITSTATUS(jobs);
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

void run_job(unsigned int first, unsigned int last)
{
    char buff_first[11];
    char buff_last[11];
    sprintf(buff_first, "%u", first);
    sprintf(buff_last, "%u", last);
    execl("bin/licz", "licz", buff_first, buff_last, NULL);
    perror("execl");
    exit(-1);
}

unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end)
{
    return (end->tv_nsec - start->tv_nsec) / 1000 + (end->tv_sec - start->tv_sec) * 1000000;
}