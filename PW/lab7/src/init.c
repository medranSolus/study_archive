#include "prodkons.h"

int main()
{
    shm_unlink(sm_name);
    int sm_fd = shm_open(sm_name, O_RDWR | O_CREAT, 0666);
    if (sm_fd == -1)
    {
        perror("shm_open");
        return -1;
    }
    if (ftruncate(sm_fd, sizeof(sm_buff)) < 0)
    {
        perror("ftruncate");
        return -2;
    }
    sm_buff* buff = (sm_buff*)mmap(NULL, sizeof(sm_buff), PROT_READ | PROT_WRITE, MAP_SHARED, sm_fd, 0);
    if (buff == NULL)
    {
        perror("mmap");
        return -3;
    }
    buff->count = buff->read_pos = buff->write_pos = 0;
    if (sem_init(&buff->mutex, 1, 1) == -1)
    {
        perror("mutex");
        return -4;
    }
    if (sem_init(&buff->sem_read, 1, 0) == -1)
    {
        perror("sem_read");
        return -5;
    }
    if (sem_init(&buff->sem_write, 1, BUF_SIZE) == -1)
    {
        perror("sem_write");
        return -6;
    }
    munmap(buff, sizeof(sm_buff));
    close(sm_fd);
    return 0;
}