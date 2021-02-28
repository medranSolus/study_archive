#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <pthread.h>
#include <semaphore.h>
#include <unistd.h>

#define MAX_SLEEP 3
#define PHILO_COUNT 5
typedef enum
{
    Operation_sleep = 'M',
    Operation_get_left = 'L',
    Operation_get_right = 'R',
    Operation_eat = 'E'
} Operation;

typedef struct
{
    Operation philo_status[PHILO_COUNT];
    sem_t mutex;
    sem_t forks[PHILO_COUNT];
    unsigned char max_time;
} Table;

void print_state(unsigned char philo_id, Operation op);
char* get_state_string(Operation op);
void sem_op(int sem_id, bool increment);
void delay();
void* philo(void* philo_ptr);

Table memory;

int main(int argc, char** argv)
{
    memory.max_time = (argc == 2 ? (unsigned char)atoi(argv[1]) : 15);
    if (sem_init(&memory.mutex, 0, 1))
    {
        perror("sem_init mutex");
        return -2;
    }
    for (unsigned char i = 0; i < PHILO_COUNT; ++i)
    {
        memory.philo_status[i] = Operation_sleep;
        if (sem_init(&memory.forks[i], 0, 1))
        {
            perror("sem_init");
            return -3;
        }
    }
    unsigned char* ids = (unsigned char*)calloc(PHILO_COUNT, sizeof(unsigned char));
    pthread_t philosephers[PHILO_COUNT];
    for (unsigned char i = 0; i < PHILO_COUNT; ++i)
    {
        ids[i] = i;
        const int ret = pthread_create(&philosephers[i], NULL, philo, &ids[i]);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot create thread %u. Error number: %i", i, ret);
            return -4;
        }
    }
    for (unsigned char i = 0; i < PHILO_COUNT; ++i)
    {
        const int ret = pthread_join(philosephers[i], NULL);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot join thread %u. Error number: %i", i, ret);
            return -5;
        }
    }
    free(ids);
    return 0;
}

void* philo(void* philo_ptr)
{
    const unsigned char philo_id = *(unsigned char*)philo_ptr;
    unsigned char left_fork, right_fork;
    bool left_handed = philo_id % 2;
    if (left_handed)
    {
        left_fork = philo_id;
        right_fork = (philo_id + 1) % PHILO_COUNT;
    }
    else
    {
        right_fork = philo_id;
        left_fork = (philo_id + 1) % PHILO_COUNT;
    }
    for (unsigned char i = memory.max_time; i; --i)
    {
        print_state(philo_id, left_handed ? Operation_get_left : Operation_get_right);
        sem_op(left_fork, false);
        print_state(philo_id, left_handed ? Operation_get_right : Operation_get_left);
        sem_op(right_fork, false);
        print_state(philo_id, Operation_eat);
        delay();
        // Give forks away
        sem_op(left_fork, true);
        sem_op(right_fork, true);
        print_state(philo_id, Operation_sleep);
        if (i > 1)
            delay();
    }
    return NULL;
}

void delay()
{
    sleep(1 + rand() % (MAX_SLEEP));
}

void sem_op(int sem_id, bool increment)
{
    if (increment ? sem_post(&memory.forks[sem_id]) : sem_wait(&memory.forks[sem_id]))
    {
        perror("sem_op");
        pthread_exit(NULL);
    }
}

char* get_state_string(Operation op)
{
    switch (op)
    {
    case Operation_sleep:
        return "meditates";
    case Operation_get_left:
        return "takes left fork";
    case Operation_get_right:
        return "takes right fork";
    case Operation_eat:
        return "eats";
    default:
        return "pledged alliance to dark gods";
    }
}

void print_state(unsigned char philo_id, Operation op)
{
    if (sem_wait(&memory.mutex))
    {
        perror("sem_wait");
        pthread_exit(NULL);
    }
    memory.philo_status[philo_id] = op;
    printf("[%c %c %c %c %c] Philosopher %u. %s\n",
        memory.philo_status[0], memory.philo_status[1],
        memory.philo_status[2], memory.philo_status[3],
        memory.philo_status[4], philo_id,
        get_state_string(op));
    if (sem_post(&memory.mutex))
    {
        perror("sem_post");
        pthread_exit(NULL);
    }
}