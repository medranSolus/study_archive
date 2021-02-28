#define _DEFAULT_SOURCE
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <fcntl.h>
#include <unistd.h>
#include <mqueue.h>

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

const char* input_mq = "/input.mq";
const char* output_mq = "/output.mq";

unsigned int get_first_count(unsigned int start, unsigned int end);
bool is_first(int n);

int main(int argc, char *argv[])
{
    mqd_t in = mq_open(input_mq, O_RDONLY, 0660);
    if (in == -1)
    {
        perror("mq_open");
        fprintf(stderr, "Cannot open mq: %s\n", input_mq);
        return -1;
    }
    mqd_t out = mq_open(output_mq, O_WRONLY, 0660);
    if (out == -1)
    {
        perror("mq_open");
        fprintf(stderr, "Cannot open mq: %s\n", output_mq);
        return -2;
    }
    job_result job;
    unsigned int priority = 1;
    while (true)
    {
        if (mq_receive(in, &job, sizeof(job_result), &priority) == -1)
        {
            perror("mq_receive");
            return -3;
        }
        if (job.start == 0 && job.end == 0)
            break;
        job.count = get_first_count(job.start, job.end);
        mq_send(out, &job, sizeof(job_result), 1);
    }
    mq_close(in);
    mq_close(out);
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