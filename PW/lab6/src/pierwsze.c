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
#include <mqueue.h>

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

const char* input_mq = "/input.mq";
const char* output_mq = "/output.mq";

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
        if (fork() == 0)
            run_jobs(job_data, abs(atoi(argv[3])), length);
        job_data.count = job_data.end - job_data.start;
        unsigned int jobs_count = job_data.count / length + (job_data.count % length > 0 ? 1 : 0);
        struct mq_attr attr;
        attr.mq_msgsize = sizeof(job_result);
        attr.mq_maxmsg = 4;
        attr.mq_flags = 0;
        mqd_t result = mq_open(output_mq, O_RDWR | O_CREAT, 0660, &attr);
        if (result == -1)
        {
            perror("mq_open");
            fprintf(stderr, "Cannot open mq: %s\n", output_mq);
            return -4;
        }
        unsigned int first_count = 0, priority = 1;
        for (unsigned int i = 0; i < jobs_count; ++i)
        {
            if (mq_receive(result, &job_data, sizeof(job_result), &priority) == -1)
            {
                perror("mq_receive");
                return -5;
            }
            first_count += job_data.count;
        }
        mq_close(result);
        wait(NULL);
        mq_unlink(input_mq);
        mq_unlink(output_mq);
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
    struct mq_attr attr;
    attr.mq_msgsize = sizeof(job_result);
    attr.mq_maxmsg = 4;
    attr.mq_flags = 0;
    mqd_t job_out = mq_open(input_mq, O_RDWR | O_CREAT, 0660, &attr);
    if (job_out == -1)
    {
        perror("mq_open");
        fprintf(stderr, "Cannot open mq: %s\n", input_mq);
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
        if (mq_send(job_out, &job, sizeof(job_result), 1) == -1)
        {
            perror("mq_send");
            exit(-7);
        }
        job.start = job.end;
    } while (job.end < end);
    job.start = 0;
    job.end = 0;
    for (unsigned int i = 0; i < proc; ++i)
    {
        if (mq_send(job_out, &job, sizeof(job_result), 1) == -1)
        {
            perror("mq_send");
            exit(-8);
        }
    }
    mq_close(job_out);
    for (unsigned int i = 0; i < proc; ++i)
        wait(NULL);
    exit(0);
}

unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end)
{
    return (end->tv_nsec - start->tv_nsec) / 1000 + (end->tv_sec - start->tv_sec) * 1000000;
}