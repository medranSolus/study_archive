#define _POSIX_C_SOURCE 200809L
#include <semaphore.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <sys/types.h>
#include <fcntl.h>
#include <stdio.h>
#include <unistd.h>

#define MSG_SIZE 512
#define BUF_SIZE 4

typedef struct
{
    pid_t id;
    char text[MSG_SIZE];
} sm_msg;

typedef struct
{
    int count;
    int read_pos;
    int write_pos;
    sem_t mutex;
    sem_t sem_read;
    sem_t sem_write;
    sm_msg msg_buf[BUF_SIZE];
} sm_buff;

const char sm_name[] = "msg_buf";