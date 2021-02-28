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

void* receive();
void stop();
uint64_t get_string(char* buff, uint64_t* buff_size);

int main(int argc, char* argv[])
{
    soc = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (soc == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create socket!\n");
        return -1;
    }
    struct sockaddr_in addr;
    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = htons(argc == 2 ? atoi(argv[1]) : PORT);
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    uint64_t buff_size = INPUT_CHUNK; 
    char* buff = (char*)malloc(buff_size);
    uint64_t name_size = 1;
    if (connect(soc, (struct sockaddr*)&addr, sizeof(addr)))
    {
        perror("connect");
        fprintf(stderr, "Cannot connect to server on port %i!\n", htons(addr.sin_port));
        return -2;
    }
    pthread_t receive_thread;
    if (pthread_create(&receive_thread, NULL, receive, NULL))
    {
        perror("pthread_create");
        return -3;
    }
    printf("[C]onnect or [S]hutdown server?\n");
    if (toupper(getchar()) == 'S')
    {
        stop();
        printf("Server is shutting down.\n");
    }
    else
    {
        if (write(soc, &name_size, sizeof(name_size)) == -1)
        {
            perror("write");
            fprintf(stderr, "Cannot send connect server message!\n");
            run = false;
        }
        else
        {
            char c;
            while ((c = getchar()) != '\n' && c != EOF);
            printf("Choose your name: ");
            name_size = get_string(buff, &buff_size);
            buff[name_size - 1] = ' ';
            buff[name_size - 2] = ':';
            buff_size -= name_size;
            printf("Connected. Type | to disconnect.\n");
        }
    }
    while (run)
    {
        uint64_t input_size = get_string(buff + name_size, &buff_size);
        if (buff[name_size] == '|')
        {
            stop();
            break;
        }
        input_size += name_size;
        if (write(soc, &input_size, sizeof(input_size)) == -1)
        {
            perror("write");
            fprintf(stderr, "Cannot send size of message!\n");
            break;
        }
        if (write(soc, buff, input_size) == -1)
        {
            perror("write");
            fprintf(stderr, "Cannot send message!\n");
            break;
        }
    }
    free(buff);
    shutdown(soc, SHUT_RDWR);
    int ret = pthread_join(receive_thread, NULL);
    if (ret != 0)
    {
        fprintf(stderr, "Cannot join receive thread. Error number: %i", ret);
        ret = -4;
    }
    if (close(soc))
    {
        perror("close");
        fprintf(stderr, "Cannot close socket!\n");
        ret = -5;
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

void stop()
{
    uint64_t end = 0;
    if (write(soc, &end, sizeof(end)) == -1)
    {
        perror("write");
        fprintf(stderr, "Cannot send end server message!\n");
    }
    run = false;
}

void* receive()
{
    uint64_t buff_size = INPUT_CHUNK, size = 0;
    char* buff = (char*)malloc(INPUT_CHUNK);
    while (run)
    {
        if (read(soc, &size, sizeof(size)) == -1)
        {
            if (run)
            {
                perror("read");
                fprintf(stderr, "Error reading size of data!\n");
                continue;
            }
            break;
        }
        if (size > buff_size)
        {
            buff_size = size;
            buff = (char*)realloc(buff, buff_size);
        }
        if (read(soc, buff, size) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading data!\n");
            continue;
        }
        printf("%s", buff);
    }
    free(buff);
    return NULL;
}