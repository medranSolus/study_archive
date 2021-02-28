#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>
#include <sys/sem.h>
#include <errno.h>
#include <unistd.h>

#define MAX_SLEEP 3
#define PHILO_COUNT 5
typedef enum
{
    Operation_sleep = 'M',
    Operation_get_forks = 'F',
    Operation_eat = 'E'
} Operation;

typedef struct
{
    Operation philo_status[PHILO_COUNT];
} Table;

typedef union
{
    int val;
    struct semid_ds *buf;
    unsigned short *array;
    struct seminfo *__buf;
} semun;

void print_state(unsigned char philo_id, Operation op);
char* get_state_string(Operation op);
void sem_op(int sem_id, struct sembuf* op);
void delay();

Table* memory = NULL;

int main(int argc, char** argv)
{
    if (argc >= 2)
    {
        const unsigned char philo_id = (unsigned char)atoi(argv[1]);
        if (philo_id >= PHILO_COUNT)
        {
            fprintf(stderr, "Philosopher number must be smaller than %i!", PHILO_COUNT);
            return -1;
        }
        const key_t key = 1000 + rand();
        srand(time(NULL));
        bool first_use = true;
        int mem_id = shmget(key, sizeof(Table), IPC_CREAT | IPC_EXCL | 0660);
        if (mem_id == -1)
        {
            if (errno == EEXIST)
            {
                mem_id = shmget(key, sizeof(Table), 0660);
                if (mem_id == -1)
                {
                    perror("shmget get");
                    return -2;
                }
            }
            else
            {
                perror("shmget create");
                return -3;
            }
            first_use = false;
        }
        memory = (Table*)shmat(mem_id, NULL, 0);
        if (memory == (Table*)-1)
        {
            perror("shmat");
            return -4;
        }
        int sem_id;
        if (first_use)
        {
            memset(memory->philo_status, Operation_sleep, sizeof(Operation) * PHILO_COUNT);
            sem_id = semget(key, PHILO_COUNT, IPC_CREAT | IPC_EXCL | 0660);
            if (sem_id == -1)
            {
                perror("semget create");
                return -5;
            }
            semun argument;
            unsigned short init_values[PHILO_COUNT] = { 1, 1, 1, 1, 1 };
            argument.array = init_values;
            if (semctl(sem_id, 0, SETALL, argument) == -1)
            {
                perror("semctl");
                return -6;
            }
        }
        else
        {
            sem_id = semget(key, PHILO_COUNT, 0660);
            if (sem_id == -1)
            {
                perror("semget get");
                return -7;
            }
        }
        struct sembuf operations[2];
        operations[0].sem_flg = operations[1].sem_flg = SEM_UNDO;
        operations[0].sem_num = philo_id;
        operations[1].sem_num = (philo_id + 1) % PHILO_COUNT;
        for (unsigned char max_time = (argc == 3 ? (unsigned char)atoi(argv[2]) : 15); max_time; --max_time)
        {
            operations[0].sem_op = operations[1].sem_op = -1;
            print_state(philo_id, Operation_get_forks);
            sem_op(sem_id, operations);
            print_state(philo_id, Operation_eat);
            delay();
            // Give forks away
            operations[0].sem_op = operations[1].sem_op = 1;
            sem_op(sem_id, operations);
            print_state(philo_id, Operation_sleep);
            if (max_time > 1)
                delay();
        }
        if (shmdt(memory) == -1)
        {
            perror("shmdt");
            return -8;
        }
    }
    else
    {
        fprintf(stderr, "Specify philosopher number!");
        return -9;
    }
    return 0;
}

void delay()
{
    sleep(1 + rand() % (MAX_SLEEP));
}

void sem_op(int sem_id, struct sembuf* op)
{
    if (semop(sem_id, op, 2) == -1)
    {
        perror("semop");
        exit(-10);
    }
}

char* get_state_string(Operation op)
{
    switch (op)
    {
    case Operation_sleep:
        return "meditates";
    case Operation_get_forks:
        return "takes forks";
    case Operation_eat:
        return "eats";
    default:
        return "pledged alliance to dark gods";
    }
}

void print_state(unsigned char philo_id, Operation op)
{
    memory->philo_status[philo_id] = op;
    printf("[%c %c %c %c %c] Philosopher %u. %s\n",
        memory->philo_status[0], memory->philo_status[1],
        memory->philo_status[2], memory->philo_status[3],
        memory->philo_status[4], philo_id,
        get_state_string(op));
}