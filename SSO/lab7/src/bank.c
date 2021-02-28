#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <pthread.h>
#include <unistd.h>

#define MAX_SLEEP 3
#define INITIAL_MONEY 2000
#define OPERATION_MONEY 50
#define GET(_worker) (_worker->account ? bank.account1 : bank.account2)
#define GET_MUTEX_B(_acc) (_acc ? &bank.account1_mutex : &bank.account2_mutex)
#define GET_MUTEX(_worker) GET_MUTEX_B(_worker->account)
#define GET_QUEUE_B(_acc) (_acc ? &bank.account1_queue : &bank.account2_queue)
#define GET_QUEUE(_worker) GET_QUEUE_B(_worker->account)
typedef enum
{
    Operation_earn = 'E',
    Operation_pay = 'P',
    Operation_transfer = 'T'
} Operation;

typedef struct
{
    bool account;
    Operation op;
} Worker;

typedef struct
{
    uint64_t time;
    pthread_mutex_t accounts_mutex;
    pthread_cond_t accounts_queue;

    uint64_t account1;
    pthread_mutex_t account1_mutex;
    pthread_cond_t account1_queue;

    uint64_t account2;
    pthread_mutex_t account2_mutex;
    pthread_cond_t account2_queue;
} Bank;

void op_transfer(const Worker* worker, uint64_t money);
void op_pay(const Worker* worker, uint64_t money);
void op_earn(const Worker* worker, uint64_t money);
void* worker(void* worker_ptr);
void log_info(const Worker* worker, uint64_t money);
void delay();

Bank bank;

int main(int argc, char** argv)
{
    srand(time(NULL));
    bank.account1 = INITIAL_MONEY;
    bank.account2 = INITIAL_MONEY;
    bank.time = (argc >= 2 ? (uint64_t)atoll(argv[1]) : 6);
    if (pthread_mutex_init(&bank.accounts_mutex, NULL))
    {
        perror("pthread_mutex_init accounts");
        return -1;
    }
    if (pthread_cond_init(&bank.accounts_queue, NULL))
    {
        perror("pthread_cond_init accounts");
        return -2;
    }
    if (pthread_mutex_init(&bank.account1_mutex, NULL))
    {
        perror("pthread_mutex_init account1");
        return -3;
    }
    if (pthread_cond_init(&bank.account1_queue, NULL))
    {
        perror("pthread_cond_init account1");
        return -4;
    }
    if (pthread_mutex_init(&bank.account2_mutex, NULL))
    {
        perror("pthread_mutex_init account2");
        return -5;
    }
    if (pthread_cond_init(&bank.account2_queue, NULL))
    {
        perror("pthread_cond_init account2");
        return -6;
    }
    const uint64_t worker_count = (argc == 3 ? (uint64_t)atoll(argv[2]) : 4) * 3;
    Worker* work_data = (Worker*)calloc(worker_count, sizeof(Worker));
    pthread_t* workers = (pthread_t*)calloc(worker_count, sizeof(pthread_t));
    for (uint64_t i = 0; i < worker_count; ++i)
    {
        work_data[i].account = (bool)(rand() % 2);
        work_data[i].op = Operation_earn;
        int ret = pthread_create(&workers[i], NULL, worker, &work_data[i]);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot create thread %lu. Error number: %i", i, ret);
            return -7;
        }
        work_data[++i].account = (bool)(rand() % 2);
        work_data[i].op = Operation_pay;
        ret = pthread_create(&workers[i], NULL, worker, &work_data[i]);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot create thread %lu. Error number: %i", i, ret);
            return -8;
        }
        work_data[++i].account = (bool)(rand() % 2);
        work_data[i].op = Operation_transfer;
        ret = pthread_create(&workers[i], NULL, worker, &work_data[i]);
        if (ret != 0)
        {
           fprintf(stderr, "Cannot create thread %lu. Error number: %i", i, ret);
           return -9;
        }
    }
    for (uint64_t i = 0; i < worker_count; ++i)
    {
        const int ret = pthread_join(workers[i], NULL);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot join thread %lu. Error number: %i", i, ret);
            return -10;
        }
    }
    if (pthread_mutex_destroy(&bank.accounts_mutex))
    {
        perror("pthread_mutex_destroy accounts");
        return -11;
    }
    if (pthread_cond_destroy(&bank.accounts_queue))
    {
        perror("pthread_cond_destroy accounts");
        return -12;
    }
    if (pthread_mutex_destroy(&bank.account1_mutex))
    {
        perror("pthread_mutex_destroy account1");
        return -13;
    }
    if (pthread_cond_destroy(&bank.account1_queue))
    {
        perror("pthread_cond_destroy account1");
        return -14;
    }
    if (pthread_mutex_destroy(&bank.account2_mutex))
    {
        perror("pthread_mutex_destroy account2");
        return -15;
    }
    if (pthread_cond_destroy(&bank.account2_queue))
    {
        perror("pthread_cond_destroy account2");
        return -16;
    }
    free(workers);
    free(work_data);
    printf("[1] %lu\t[2] %lu\t| Final state\n", bank.account1, bank.account2);
    return 0;
}

void delay()
{
    sleep(1 + rand() % MAX_SLEEP);
}

void log_info(const Worker* worker, uint64_t money)
{
    const char* msg_earn = "Adding";
    const char* msg_pay = "Taking";
    const char* msg_transfer = "Transfering";
    const char* op_type_to = "to";
    const char* op_type_from = "from";

    const char* msg;
    const char* op_type;
    switch (worker->op)
    {
    case Operation_earn:
    {
        msg = msg_earn;
        op_type = op_type_to;
        break;
    }
    case Operation_pay:
    {
        msg = msg_pay;
        op_type = op_type_from;
        break;
    }
    case Operation_transfer:
    {
        msg = msg_transfer;
        op_type = op_type_from;
        break;
    }
    }
    if (worker->op != Operation_transfer)
    {
        if (worker->account)
        {
            if (pthread_mutex_lock(&bank.account2_mutex))
            {
                perror("log lock2");
                pthread_exit(NULL);
            }
        }
        else
        {
            if (pthread_mutex_lock(&bank.account1_mutex))
            {
                perror("log lock1");
                pthread_exit(NULL);
            }
        }
    }
    printf("[1] %lu\t[2] %lu\t| %s %lu %s [%u]\n",
        bank.account1, bank.account2, msg, money, op_type, 2 - worker->account);
    if (worker->op != Operation_transfer)
    {
        if (worker->account)
        {
            if (pthread_mutex_unlock(&bank.account2_mutex))
            {
                perror("log unlock2");
                pthread_exit(NULL);
            }
        }
        else
        {
            if (pthread_mutex_unlock(&bank.account1_mutex))
            {
                perror("log unlock1");
                pthread_exit(NULL);
            }
        }
    }
}

void* worker(void* worker_ptr)
{
    const Worker* work_data = (Worker*)worker_ptr;
    for (uint64_t i = bank.time; i > 0; --i)
    {
        const uint64_t money = OPERATION_MONEY;
        switch (work_data->op)
        {
        case Operation_earn:
        {
            op_earn(work_data, money);
            break;
        }
        case Operation_pay:
        {
            op_pay(work_data, money);
            break;
        }
        case Operation_transfer:
        {
            op_transfer(work_data, money);
            break;
        }
        }
        if (i > 1)
            delay();
    }
    return NULL;
}

void op_earn(const Worker* worker, uint64_t money)
{
    if (pthread_mutex_lock(GET_MUTEX(worker)))
    {
        perror("op_earn lock");
        pthread_exit(NULL);
    }
    log_info(worker, money);
    if (worker->account)
        bank.account1 += money;
    else
        bank.account2 += money;
    if (pthread_cond_broadcast(&bank.accounts_queue))
    {
        perror("op_earn signal all");
        pthread_exit(NULL);
    }
    if (pthread_cond_broadcast(GET_QUEUE(worker)))
    {
        perror("op_earn signal");
        pthread_exit(NULL);
    }
    if (pthread_mutex_unlock(GET_MUTEX(worker)))
    {
        perror("op_earn unlock");
        pthread_exit(NULL);
    }
}

void op_pay(const Worker* worker, uint64_t money)
{
    if (pthread_mutex_lock(GET_MUTEX(worker)))
    {
        perror("op_pay lock");
        pthread_exit(NULL);
    }
    log_info(worker, money);
    while (true)
    {
        if (GET(worker) >= money)
        {
            if (worker->account)
                bank.account1 -= money;
            else
                bank.account2 -= money;
            if (pthread_mutex_unlock(GET_MUTEX(worker)))
            {
                perror("op_pay unlock");
                pthread_exit(NULL);
            }
            break;
        }
        else
        {
            if (pthread_cond_wait(GET_QUEUE(worker), GET_MUTEX(worker)))
            {
                perror("op_pay wait");
                pthread_exit(NULL);
            }
        }
    }
}

void op_transfer(const Worker* worker, uint64_t money)
{
    if (pthread_mutex_lock(&bank.accounts_mutex))
    {
        perror("op_transfer lock all");
        pthread_exit(NULL);
    }
    while (true)
    {
        if (pthread_mutex_lock(GET_MUTEX(worker)))
        {
            perror("op_transfer lock take");
            pthread_exit(NULL);
        }
        if (GET(worker) >= money)
        {
            if (pthread_mutex_lock(GET_MUTEX_B(!worker->account)))
            {
                perror("op_transfer lock give");
                pthread_exit(NULL);
            }
            log_info(worker, money);
            if (worker->account)
            {
                bank.account1 -= money;
                bank.account2 += money;
            }
            else
            {
                bank.account2 -= money;
                bank.account1 += money;
            }
            if (pthread_cond_broadcast(GET_QUEUE_B(!worker->account)))
            {
                perror("op_transfer signal give");
                pthread_exit(NULL);
            }
            if (pthread_mutex_unlock(GET_MUTEX_B(!worker->account)))
            {
                perror("op_transfer unlock give");
                pthread_exit(NULL);
            }
            if (pthread_mutex_unlock(GET_MUTEX(worker)))
            {
                perror("op_transfer unlock take");
                pthread_exit(NULL);
            }
            if (pthread_cond_broadcast(&bank.accounts_queue))
            {
                perror("op_transfer signal all");
                pthread_exit(NULL);
            }
            if (pthread_mutex_unlock(&bank.accounts_mutex))
            {
                perror("op_transfer unlock");
                pthread_exit(NULL);
            }
            break;
        }
        else
        {
            if (pthread_mutex_unlock(GET_MUTEX(worker)))
            {
                perror("op_transfer unlock take");
                pthread_exit(NULL);
            }
            if (pthread_cond_wait(&bank.accounts_queue, &bank.accounts_mutex))
            {
                perror("op_transfer wait");
                pthread_exit(NULL);
            }
        }
    }
}