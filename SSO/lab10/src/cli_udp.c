#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>
#include <unistd.h>
#include <pthread.h>
#include <sys/stat.h>
#include <sys/socket.h>
#include <netinet/in.h>

#define PORT 4200
#define INPUT_CHUNK 100

bool run = true;
int soc;
struct sockaddr_in addr;

void* receive();
void stop(bool server);
uint64_t get_string(char* buff, uint64_t* buff_size);

int main(int argc, char* argv[])
{
    soc = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (soc == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create socket!\n");
        return -1;
    }
    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = htons(argc == 2 ? atoi(argv[1]) : PORT);
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    uint64_t buff_size = INPUT_CHUNK; 
    char* buff = (char*)malloc(buff_size + sizeof(buff_size));
    pthread_t receive_thread;
    if (pthread_create(&receive_thread, NULL, receive, NULL))
    {
        perror("pthread_create");
        return -2;
    }
    uint64_t name_size = -1;
    if (sendto(soc, &name_size, sizeof(name_size), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(struct sockaddr_in)) == -1)
    {
        perror("sendto");
        fprintf(stderr, "Cannot send connect server message!\n");
        run = false;
    }
    else
    {
        printf("Choose your name: ");
        name_size = get_string(buff + sizeof(buff_size), &buff_size) + sizeof(buff_size);
        buff[name_size - 1] = ' ';
        buff[name_size - 2] = ':';
        buff_size -= name_size;
        printf("Connected. Type | to disconnect.\n");
    }
    while (run)
    {
        uint64_t input_size = get_string(buff + name_size, &buff_size);
        if (buff[name_size] == '|')
        {
            stop(false);
            break;
        }
        input_size += name_size;
        *(u_int64_t*)buff = input_size - sizeof(buff_size);
        if (sendto(soc, buff, input_size, MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(struct sockaddr_in)) == -1)
        {
            perror("sendto");
            fprintf(stderr, "Cannot send message to server!\n");
            break;
        }
    }
    free(buff);
    shutdown(soc, SHUT_RDWR);
    int ret = pthread_join(receive_thread, NULL);
    if (ret != 0)
    {
        fprintf(stderr, "Cannot join cleaner thread. Error number: %i", ret);
        ret = -3;
    }
    if (close(soc))
    {
        perror("close");
        fprintf(stderr, "Cannot close socket!\n");
        ret = -4;
    }
    return ret;
}

uint64_t get_string(char* buff, uint64_t* buff_size)
{
    uint64_t string_size = 0;
    while (true)
    {
        buff[string_size] = getchar();
        ++string_size;
        if (string_size == *buff_size)
        {
            *buff_size += INPUT_CHUNK;
            buff = (char*)realloc(buff, *buff_size);
        }
        if (buff[string_size - 1] == '\n')
        {
            buff[string_size] = '\0';
            break;
        }
    }
    return string_size + 1;
}

void stop(bool server)
{
    uint64_t end = server ? -1 : 0;
    if (sendto(soc, &end, sizeof(end), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(struct sockaddr_in)) == -1)
    {
        perror("write");
        fprintf(stderr, "Cannot send end server message!\n");
    }
    run = false;
}

void* receive()
{
    uint64_t size = 0;
    char* buff = NULL;
    while (run)
    {
        if (recvfrom(soc, &size, sizeof(size), MSG_PEEK, NULL, NULL) != sizeof(size))
        {
            if (run)
            {
                perror("recvfrom");
                fprintf(stderr, "Error reading size of data!\n");
                continue;
            }
            break;
        }
        size += sizeof(size);
        buff = (char*)realloc(buff, size);
        u_int64_t size_read = recvfrom(soc, buff, size, MSG_WAITALL, NULL, NULL);
        if (size_read != size)
        {
            perror("recvfrom");
            fprintf(stderr, "Error reading data, expected %lu, read %lu!\n", size, size_read);
            continue;
        }
        printf("%s", buff + sizeof(size));
    }
    free(buff);
    return NULL;
}