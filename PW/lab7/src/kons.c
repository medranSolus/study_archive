#include "prodkons.h"
#include <stdlib.h>

int main(int argc, char* argv[])
{
    if (argc == 2)
    {
        const size_t count = atoll(argv[1]);
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
            sem_wait(&buff->sem_read);
            sem_wait(&buff->mutex);
            sm_msg msg = buff->msg_buf[buff->read_pos];
            buff->read_pos = (buff->read_pos + 1) % BUF_SIZE;
            --buff->count;
            sem_post(&buff->mutex);
            sem_post(&buff->sem_write);
            printf("Received: %s\n", msg.text);
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