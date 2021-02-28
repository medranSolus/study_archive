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
#include <semaphore.h>
#include <sys/mman.h>

#define BUF_SIZE 4

typedef struct job_result
{
    unsigned int start;
    unsigned int end;
    unsigned int count;
} job_result;

typedef struct
{
    int read_pos;
    int write_pos;
    sem_t mutex;
    sem_t sem_read;
    sem_t sem_write;
    job_result job_buf[BUF_SIZE];
} sm_mq;

typedef struct
{
    sm_mq input;
    sm_mq output;
} sm_buff;

const char sm_name[] = "job_buff";
sm_buff* buff = NULL;

int init_memory();
void submit_job(job_result* job);
void run_jobs(job_result job, unsigned int proc, unsigned int length);
unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end);
void run_job();
unsigned int get_first_count(unsigned int start, unsigned int end);
bool is_first(int n);

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
        int sm_fd = init_memory();
        if (fork() == 0)
            run_jobs(job_data, abs(atoi(argv[3])), length);
        job_data.count = job_data.end - job_data.start;
        unsigned int jobs_count = job_data.count / length + (job_data.count % length > 0 ? 1 : 0);
        unsigned int first_count = 0;
        for (unsigned int i = 0; i < jobs_count; ++i)
        {
            sem_wait(&buff->output.sem_read);
            sem_wait(&buff->output.mutex);
            job_data = buff->output.job_buf[buff->output.read_pos];
            buff->output.read_pos = (buff->output.read_pos + 1) % BUF_SIZE;
            sem_post(&buff->output.mutex);
            sem_post(&buff->output.sem_write);
            first_count += job_data.count;
        }
        wait(NULL);
        sem_close(&buff->input.mutex);
        sem_close(&buff->input.sem_read);
        sem_close(&buff->input.sem_write);
        sem_close(&buff->output.mutex);
        sem_close(&buff->output.sem_read);
        sem_close(&buff->output.sem_write);
        munmap(buff, sizeof(sm_buff));
        close(sm_fd);
        shm_unlink(sm_name);
        clock_gettime(CLOCK_MONOTONIC, &end);
        printf("Time: %u us\nCount: %u\n", get_microsec_diff(&start, &end), first_count);
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}

int init_memory()
{
    shm_unlink(sm_name);
    int sm_fd = shm_open(sm_name, O_RDWR | O_CREAT, 0666);
    if (sm_fd == -1)
    {
        perror("shm_open");
        return -2;
    }
    if (ftruncate(sm_fd, sizeof(sm_buff)) < 0)
    {
        perror("ftruncate");
        return -3;
    }
    buff = (sm_buff*)mmap(NULL, sizeof(sm_buff), PROT_READ | PROT_WRITE, MAP_SHARED, sm_fd, 0);
    if (buff == NULL)
    {
        perror("mmap");
        return -4;
    }
    buff->input.read_pos = buff->input.write_pos = 0;
    if (sem_init(&buff->input.mutex, 1, 1) == -1)
    {
        perror("input.mutex");
        return -5;
    }
    if (sem_init(&buff->input.sem_read, 1, 0) == -1)
    {
        perror("input.sem_read");
        return -6;
    }
    if (sem_init(&buff->input.sem_write, 1, BUF_SIZE) == -1)
    {
        perror("input.sem_write");
        return -7;
    }
    buff->output.read_pos = buff->output.write_pos = 0;
    if (sem_init(&buff->output.mutex, 1, 1) == -1)
    {
        perror("output.mutex");
        return -8;
    }
    if (sem_init(&buff->output.sem_read, 1, 0) == -1)
    {
        perror("output.sem_read");
        return -9;
    }
    if (sem_init(&buff->output.sem_write, 1, BUF_SIZE) == -1)
    {
        perror("output.sem_write");
        return -10;
    }
    return sm_fd;
}

void submit_job(job_result* job)
{
    sem_wait(&buff->input.sem_write);
    sem_wait(&buff->input.mutex);
    buff->input.job_buf[buff->input.write_pos] = *job;
    buff->input.write_pos = (buff->input.write_pos + 1) % BUF_SIZE;
    sem_post(&buff->input.mutex);
    sem_post(&buff->input.sem_read);
}

void run_jobs(job_result job, unsigned int proc, unsigned int length)
{
    for (unsigned int i = 0; i < proc; ++i)
        if (fork() == 0)
            run_job();
    unsigned int end = job.end;
    do
    {
        job.end = job.start + length;
        if (job.end > end)
            job.end = end;
        submit_job(&job);
        job.start = job.end;
    } while (job.end < end);
    job.start = 0;
    job.end = 0;
    for (unsigned int i = 0; i < proc; ++i)
        submit_job(&job);
    for (unsigned int i = 0; i < proc; ++i)
        wait(NULL);
    exit(0);
}

unsigned long long get_microsec_diff(struct timespec* start, struct timespec* end)
{
    return (end->tv_nsec - start->tv_nsec) / 1000 + (end->tv_sec - start->tv_sec) * 1000000;
}

void run_job()
{
    while (true)
    {
        sem_wait(&buff->input.sem_read);
        sem_wait(&buff->input.mutex);
        job_result job = buff->input.job_buf[buff->input.read_pos];
        buff->input.read_pos = (buff->input.read_pos + 1) % BUF_SIZE;
        sem_post(&buff->input.mutex);
        sem_post(&buff->input.sem_write);
        if (job.start == 0 && job.end == 0)
            break;
        job.count = get_first_count(job.start, job.end);
        sem_wait(&buff->output.sem_write);
        sem_wait(&buff->output.mutex);
        buff->output.job_buf[buff->output.write_pos] = job;
        buff->output.write_pos = (buff->output.write_pos + 1) % BUF_SIZE;
        sem_post(&buff->output.mutex);
        sem_post(&buff->output.sem_read);
    }
    exit(0);
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