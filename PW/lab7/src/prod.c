#include "prodkons.h"
#include <stdlib.h>

int main(int argc, char* argv[])
{
    if (argc == 3)
    {
        sm_msg msg;
        msg.id = atoi(argv[1]);
        const size_t count = atoll(argv[2]);
        int sm_fd = shm_open(sm_name, O_RDWR, 0666);
        if (sm_fd == -1)
        {
            perror("shm_open");
            return -2;
        }
        sm_buff* buff = (sm_buff*)mmap(NULL, sizeof(sm_buff), PROT_WRITE | PROT_READ, MAP_SHARED, sm_fd, 0);
        if (buff == NULL)
        {
            perror("mmap");
            return -3;
        }
        for (size_t i = 0; i < count; ++i)
        {
            sprintf(msg.text, "Prod id=%i, i=%i", msg.id, i);
            sem_wait(&buff->sem_write);
            sem_wait(&buff->mutex);
            buff->msg_buf[buff->write_pos] = msg;
            buff->write_pos = (buff->write_pos + 1) % BUF_SIZE;
            ++buff->count;
            sem_post(&buff->mutex);
            sem_post(&buff->sem_read);
            sleep(1);
        }
        close(sm_fd);
        return 0;
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}