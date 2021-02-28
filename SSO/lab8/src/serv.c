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
#define MAX_WAIT_QUEUE 100

void soc_close(int soc);

int main(int argc, char* argv[])
{
    int serv_soc = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (serv_soc == -1)
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
    if (bind(serv_soc, (struct sockaddr*)&addr, sizeof(addr)))
    {
        perror("bind");
        fprintf(stderr, "Cannot open server on port %i!\n", addr.sin_port);
        return -2;
    }
    if (listen(serv_soc, MAX_WAIT_QUEUE))
    {
        perror("listen");
        fprintf(stderr, "Cannot listen on socket!\n");
        return -3;
    }
    while (true)
    {
        int socket = accept(serv_soc, NULL, NULL);
        if (socket < 0)
        {
            perror("accept");
            fprintf(stderr, "Error accepting incoming call!\n");
            continue;
        }
        int size = 0;
        if (read(socket, &size, sizeof(size)) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading size of data!\n");
            soc_close(socket);
            continue;
        }
        if (size == 0)
        {
            soc_close(socket);
            break;
        }
        int pid = fork();
        if (pid < 0)
        {
            perror("fork");
            soc_close(socket);
        }
        else if (pid == 0)
        {
            char* buff = (char*)malloc(size);
            while (true)
            {
                if (read(socket, buff, size) == -1)
                {
                    perror("read");
                    fprintf(stderr, "Error reading data!\n");
                    break;
                }
                if (write(socket, buff, size) == -1)
                {
                    perror("read");
                    fprintf(stderr, "Error sending data!\n");
                    break;
                }
                if (read(socket, &size, sizeof(size)) == -1)
                {
                    perror("read");
                    fprintf(stderr, "Error reading size of data!\n");
                    break;
                }
                if (size == 0)
                    break;
                buff = (char*)realloc(buff, size);
            }
            soc_close(socket);
            free(buff);
            exit(0);
        }

    }
    if (shutdown(serv_soc, 2))
        perror("shutdown");
    if (close(serv_soc))
    {
        perror("close");
        fprintf(stderr, "Cannot close server!\n");
        return -4;
    }
    return 0;
}

void soc_close(int soc)
{
    if (shutdown(soc, 2))
        perror("shutdown");
    if (close(soc))
    {
        perror("close");
        fprintf(stderr, "Cannot close socket!\n");
    }
}