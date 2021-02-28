#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/stat.h>
#include <sys/socket.h>
#include <netinet/in.h>

#define PORT 4200
#define INPUT_CHUNK 100

int main(int argc, char* argv[])
{
    int soc = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (soc == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create socket!\n");
        return -1;
    }
    struct sockaddr_in addr;
    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = argc == 2 ? atoi(argv[1]) : PORT;
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    int buff_size = INPUT_CHUNK; 
    char* buff = (char*)malloc(buff_size);
    if (connect(soc, (struct sockaddr*)&addr, sizeof(addr)))
    {
        perror("connect");
        fprintf(stderr, "Cannot connect to server on port %i!\n", addr.sin_port);
        return -2;
    }
    while (true)
    {
        int string_size = 0;
        while (true)
        {
            buff[string_size] = getchar();
            ++string_size;
            if (string_size == buff_size)
            {
                buff_size += INPUT_CHUNK;
                buff = (char*)realloc(buff, buff_size);
            }
            if (buff[string_size - 1] == '\n')
            {
                buff[string_size] = '\0';
                break;
            }
        }
        if (buff[0] == '|')
        {
            int end = 0;
            if (write(soc, &end, sizeof(end)) == -1)
            {
                perror("write");
                fprintf(stderr, "Cannot send end server message!\n");
            }
            break;
        }
        if (write(soc, &string_size, sizeof(string_size)) == -1)
        {
            perror("write");
            fprintf(stderr, "Cannot send size of message!\n");
            break;
        }
        if (write(soc, buff, string_size) == -1)
        {
            perror("write");
            fprintf(stderr, "Cannot send message!\n");
            break;
        }
        char* input = (char*)malloc(string_size);
        if (read(soc, input, string_size) == -1)
        {
            perror("read");
            fprintf(stderr, "Cannot read message!\n");
            free(input);
            break;
        }
        printf("Received: %s", input);
        free(input);
    }
    free(buff);
    if (close(soc))
    {
        perror("close");
        fprintf(stderr, "Cannot close socket!\n");
        return -3;
    }
    return 0;
}