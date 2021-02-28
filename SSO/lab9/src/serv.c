#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <pthread.h>
#include <sys/stat.h>
#include <sys/socket.h>
#include <netinet/in.h>

#define TIMEOUT 5000
#define PORT 4200
#define MAX_WAIT_QUEUE 100
#define INITIAL_CLIENT_COUNT 4

typedef struct
{
    uint64_t id;
    uint64_t size;
    char* buff;
} ClientData;

typedef struct
{
    int soc;
    pthread_t* thread;
    pthread_mutex_t mutex;
} Client;

uint64_t client_count = INITIAL_CLIENT_COUNT;
uint64_t last_used_client = INITIAL_CLIENT_COUNT - 1;
Client* clients;
int server;
pthread_t clean_thread;

pthread_mutex_t mutex;
pthread_cond_t server_wait;
pthread_cond_t senders_wait;
bool operation_pending = false;
uint64_t data_senders = 0;

void* cleaner();
void* client(void* data);
bool lock_server();
bool unlock_server();
void lock_send(ClientData* data);
void unlock_send(ClientData* data);
void stop_thread(ClientData* data);
void init_server(in_port_t port);
void soc_close(int soc);

int main(int argc, char* argv[])
{
    init_server(argc == 2 ? atoi(argv[1]) : PORT);
    if (pthread_mutex_init(&mutex, NULL))
    {
        perror("pthread_mutex_init");
        return -5;
    }
    if (pthread_cond_init(&server_wait, NULL))
    {
        perror("pthread_cond_init server_wait");
        return -6;
    }
    if (pthread_cond_init(&senders_wait, NULL))
    {
        perror("pthread_cond_init senders_wait");
        return -7;
    }
    int ret_val = pthread_create(&clean_thread, NULL, cleaner, NULL);
    if (ret_val != 0)
    {
        fprintf(stderr, "Cannot create cleaner thread. Error number: %i", ret_val);
        return -8;
    }
    while (true)
    {
        int socket = accept(server, NULL, NULL);
        if (socket < 0)
        {
            perror("accept");
            fprintf(stderr, "Error accepting incoming call!\n");
            continue;
        }
        uint64_t size = 0;
        if (read(socket, &size, sizeof(uint64_t)) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading connect command!\n");
            soc_close(socket);
            continue;
        }
        else if (size == 0)
        {
            soc_close(socket);
            break;
        }
        if (lock_server())
            break;
        ClientData* data = (ClientData*)malloc(sizeof(ClientData));
        uint64_t i = last_used_client;
        while (true)
        {
            i = (i + 1) % client_count;
            if (clients[i].thread == NULL)
            {
                last_used_client = i;
                break;
            }
            else if (i == last_used_client)
            {
                last_used_client = i = client_count;
                clients = (Client*)realloc(clients, ++client_count * sizeof(Client));
                break;
            }
        }
        clients[i].soc = socket;
        clients[i].thread = (pthread_t*)malloc(sizeof(pthread_t));
        data->id = i;
        data->size = size;
        data->buff = NULL;
        const int ret = pthread_create(clients[i].thread, NULL, client, data);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot create thread %lu. Error number: %i", i, ret);
            free(data);
            free(clients[i].thread);
            clients[i].soc = -1;
            clients[i].thread = NULL;
        }
        if (unlock_server())
            break;
    }
    if (shutdown(server, SHUT_RDWR))
        perror("shutdown server");
    ret_val = 0;
    if (close(server))
    {
        perror("close");
        fprintf(stderr, "Cannot close server!\n");
        ret_val = -9;
    }
    server = -1;
    const int ret = pthread_join(clean_thread, NULL);
    if (ret != 0)
    {
        fprintf(stderr, "Cannot join cleaner thread. Error number: %i", ret);
        ret_val = -10;
    }
    for (uint64_t i = 0; i < client_count; ++i)
    {
        if (pthread_mutex_destroy(&clients[i].mutex))
        {
            perror("pthread_mutex_destroy client");
            ret_val = -11;
        }
    }
    free(clients);
    if (pthread_mutex_destroy(&mutex))
    {
        perror("pthread_mutex_destroy");
        ret_val = -12;
    }
    if (pthread_cond_destroy(&server_wait))
    {
        perror("pthread_cond_destroy server_wait");
        ret_val = -13;
    }
    if (pthread_cond_destroy(&senders_wait))
    {
        perror("pthread_cond_destroy senders_wait");
        ret_val = -14;
    }
    return ret_val;
}

void soc_close(int soc)
{
    if (shutdown(soc, SHUT_WR))
        perror("shutdown");
    if (close(soc))
    {
        perror("close");
        fprintf(stderr, "Cannot close socket!\n");
    }
}

void init_server(in_port_t port)
{
    clients = (Client*)calloc(client_count, sizeof(Client));
    for (uint64_t i = 0; i < client_count; ++i)
    {
        clients[i].soc = -1;
        clients[i].thread = NULL;
        if (pthread_mutex_init(&clients[i].mutex, NULL))
        {
            perror("pthread_mutex_init client");
            exit(-1);
        }
    }
    server = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (server == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create socket!\n");
        exit(-2);
    }
    struct sockaddr_in addr;
    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = port;
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    if (bind(server, (struct sockaddr*)&addr, sizeof(addr)))
    {
        perror("bind");
        fprintf(stderr, "Cannot open server on port %i!\n", addr.sin_port);
        exit(-3);
    }
    if (listen(server, MAX_WAIT_QUEUE))
    {
        perror("listen");
        fprintf(stderr, "Cannot listen on socket!\n");
        exit(-4);
    }
}

void stop_thread(ClientData* data)
{
    if (!pthread_mutex_lock(&clients[data->id].mutex))
    {
        soc_close(clients[data->id].soc);
        clients[data->id].soc = -1;
        if (pthread_mutex_unlock(&clients[data->id].mutex))
            perror("pthread_mutex_unlock stop_thread");
    }
    else
        perror("pthread_mutex_lock stop_thread");
    free(data->buff);
    free(data);
    pthread_exit(NULL);
}

void unlock_send(ClientData* data)
{
    if (pthread_mutex_lock(&mutex))
    {
        perror("pthread_mutex_lock unlock_send");
        stop_thread(data);
    }
    if (--data_senders == 0)
    {
        if (pthread_cond_signal(&server_wait))
        {
            perror("pthread_cond_signal unlock_send");
            stop_thread(data);
        }
    }
    if (pthread_mutex_unlock(&mutex))
    {
        perror("pthread_mutex_unlock unlock_send");
        stop_thread(data);
    }
}

void lock_send(ClientData* data)
{
    if (pthread_mutex_lock(&mutex))
    {
        perror("pthread_mutex_lock lock_send");
        stop_thread(data);
    }
    while (operation_pending)
    {
        if (pthread_cond_wait(&senders_wait, &mutex))
        {
            perror("pthread_cond_wait lock_send");
            stop_thread(data);
        }
    }
    ++data_senders;
    if (pthread_mutex_unlock(&mutex))
    {
        perror("pthread_mutex_unlock lock_send");
        stop_thread(data);
    }
}

bool unlock_server()
{
    if (pthread_mutex_lock(&mutex))
    {
        perror("pthread_mutex_lock unlock_server");
        return true;
    }
    operation_pending = false;
    if (pthread_cond_broadcast(&senders_wait))
    {
        perror("pthread_cond_signal unlock_server");
        return true;
    }
    if (pthread_mutex_unlock(&mutex))
    {
        perror("pthread_mutex_unlock unlock_server");
        return true;
    }
    return false;
}

bool lock_server()
{
    if (pthread_mutex_lock(&mutex))
    {
        perror("pthread_mutex_lock unlock_server");
        return true;
    }
    while (operation_pending && data_senders != 0)
    {
        if (pthread_cond_wait(&server_wait, &mutex))
        {
            perror("pthread_cond_wait lock_send");
            return false;
        }
    }
    operation_pending = true;
    if (pthread_mutex_unlock(&mutex))
    {
        perror("pthread_mutex_unlock unlock_server");
        return true;
    }
    return false;
}

void* client(void* data_ptr)
{
    ClientData* data = (ClientData*)data_ptr;
    while (true)
    {
        if (read(clients[data->id].soc, &data->size, sizeof(data->size)) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading size of data!\n");
            break;
        }
        if (data->size == 0)
            break;
        data->buff = (char*)realloc(data->buff, data->size);
        if (read(clients[data->id].soc, data->buff, data->size) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading data!\n");
            break;
        }
        lock_send(data);
        for (uint64_t i = 0; i < client_count; ++i)
        {
            if (i != data->id)
            {
                if (pthread_mutex_lock(&clients[i].mutex))
                {
                    perror("pthread_mutex_lock client");
                    continue;
                }
                if (clients[i].soc != -1)
                {
                    if (write(clients[i].soc, &data->size, sizeof(data->size)) == -1)
                    {
                        perror("write");
                        fprintf(stderr, "Error sending size of data!\n");
                    }
                    else if (write(clients[i].soc, data->buff, data->size) == -1)
                    {
                        perror("write");
                        fprintf(stderr, "Error sending data!\n");
                    }
                }
                if (pthread_mutex_unlock(&clients[i].mutex))
                    perror("pthread_mutex_unlock client");
            }
        }
        unlock_send(data);
    }
    stop_thread(data);
    return NULL;
}

void* cleaner()
{
    bool run = true;
    do
    {
        sleep(1);
        if (server == -1)
            run = false;
        for (uint64_t i = 0; i < client_count; ++i)
        {
            if (clients[i].thread != NULL)
            {
                const int ret = pthread_join(*clients[i].thread, NULL);
                if (ret != 0)
                    fprintf(stderr, "Cannot join thread %lu. Error number: %i", i, ret);
                if (lock_server())
                    break;
                free(clients[i].thread);
                clients[i].thread = NULL;
                if (unlock_server())
                    break;
            }
        }
    } while (run);
    return NULL;
}