#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <errno.h>
#include <sys/types.h>
#include <sys/ipc.h>
#include <sys/shm.h>

int main()
{
    key_t key = 1000 + rand();
    bool first_use = true;
    int mem_id = shmget(key, sizeof(unsigned long long), IPC_CREAT | IPC_EXCL | 0660);
    if (mem_id == -1)
    {
        if (errno == EEXIST)
        {
            mem_id = shmget(key, sizeof(unsigned long long), 0660);
            if (mem_id == -1)
            {
                perror("shmget get");
                return -1;
            }
        }
        else
        {
            perror("shmget create");
            return -2;
        }
        first_use = false;
    }
    unsigned long long* memory = (unsigned long long*)shmat(mem_id, NULL, 0);
    if (memory == (unsigned long long*)-1)
    {
        perror("shmat");
        return -3;
    }
    if (first_use)
    {
        printf("Initializing memory to 0\n");
        *memory = 0;
    }
    else
    {
        printf("Read value %llu\n", *memory);
        ++(*memory);
    }
    if (shmdt(memory) == -1)
    {
        perror("shmdt");
        return -4;
    }
    return 0;
}