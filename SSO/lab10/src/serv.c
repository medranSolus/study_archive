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
    in_addr_t addr;
    in_port_t port;
    uint64_t size;
    char* buff;
} ClientData;

typedef struct
{
    int soc;
    pthread_t* thread;
    pthread_mutex_t mutex;
} ClientTCP;

typedef struct
{
    struct sockaddr_in* addr;
    pthread_mutex_t mutex;
} ClientUDP;

uint64_t client_tcp_count = INITIAL_CLIENT_COUNT;
uint64_t last_used_tcp_client = INITIAL_CLIENT_COUNT - 1;
ClientTCP* clients_tcp;
int server_tcp;
uint64_t client_udp_count = INITIAL_CLIENT_COUNT;
uint64_t last_used_udp_client = INITIAL_CLIENT_COUNT - 1;
ClientUDP* clients_udp;
int server_udp;
bool run_server = true;
pthread_t udp_thread;
pthread_t clean_thread;

pthread_mutex_t mutex;
pthread_cond_t server_wait;
pthread_cond_t senders_wait;
bool operation_pending_tcp = false;
bool operation_pending_udp = false;
uint64_t data_senders = 0;

void* cleaner();
void send_all();
void* client(void* data);
void* server();
bool lock_server(bool tcp);
bool unlock_server(bool tcp);
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
        return -8;
    }
    if (pthread_cond_init(&server_wait, NULL))
    {
        perror("pthread_cond_init server_wait");
        return -9;
    }
    if (pthread_cond_init(&senders_wait, NULL))
    {
        perror("pthread_cond_init senders_wait");
        return -10;
    }
    int ret_val = pthread_create(&clean_thread, NULL, cleaner, NULL);
    if (ret_val != 0)
    {
        fprintf(stderr, "Cannot create cleaner thread. Error number: %i", ret_val);
        return -11;
    }
    ret_val = pthread_create(&udp_thread, NULL, server, NULL);
    if (ret_val != 0)
    {
        fprintf(stderr, "Cannot create UDP thread. Error number: %i", ret_val);
        return -12;
    }
    while (true)
    {
        int socket = accept(server_tcp, NULL, NULL);
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
            run_server = false;
            break;
        }
        if (lock_server(true))
            break;
        ClientData* data = (ClientData*)malloc(sizeof(ClientData));
        uint64_t i = last_used_tcp_client;
        while (true)
        {
            i = (i + 1) % client_tcp_count;
            if (clients_tcp[i].thread == NULL)
            {
                last_used_tcp_client = i;
                break;
            }
            else if (i == last_used_tcp_client)
            {
                last_used_tcp_client = i = client_tcp_count;
                clients_tcp = (ClientTCP*)realloc(clients_tcp, ++client_tcp_count * sizeof(ClientTCP));
                pthread_mutex_init(&clients_tcp[i].mutex, NULL);
                break;
            }
        }
        clients_tcp[i].soc = socket;
        clients_tcp[i].thread = (pthread_t*)malloc(sizeof(pthread_t));
        data->id = i;
        data->size = size;
        data->addr = data->port = 0;
        data->buff = NULL;
        const int ret = pthread_create(clients_tcp[i].thread, NULL, client, data);
        if (ret != 0)
        {
            fprintf(stderr, "Cannot create thread %lu. Error number: %i", i, ret);
            free(data);
            free(clients_tcp[i].thread);
            clients_tcp[i].soc = -1;
            clients_tcp[i].thread = NULL;
        }
        if (unlock_server(true))
            break;
    }
    if (shutdown(server_tcp, SHUT_RDWR))
        perror("shutdown server_tcp");
    ret_val = 0;
    if (close(server_tcp))
    {
        perror("close");
        fprintf(stderr, "Cannot close server_tcp!\n");
        ret_val = -13;
    }
    server_tcp = -1;
    shutdown(server_udp, SHUT_RDWR);
    if (close(server_udp))
    {
        perror("close");
        fprintf(stderr, "Cannot close server_udp!\n");
        ret_val = -14;
    }
    int ret = pthread_join(udp_thread, NULL);
    if (ret != 0)
    {
        fprintf(stderr, "Cannot join UDP thread. Error number: %i", ret);
        ret_val = -15;
    }
    ret = pthread_join(clean_thread, NULL);
    if (ret != 0)
    {
        fprintf(stderr, "Cannot join cleaner thread. Error number: %i", ret);
        ret_val = -16;
    }
    for (uint64_t i = 0; i < client_tcp_count; ++i)
    {
        if (pthread_mutex_destroy(&clients_tcp[i].mutex))
        {
            perror("pthread_mutex_destroy client_tcp");
            ret_val = -17;
        }
    }
    free(clients_tcp);
    for (uint64_t i = 0; i < client_udp_count; ++i)
    {
        if (clients_udp[i].addr != NULL)
            free(clients_udp[i].addr);
        if (pthread_mutex_destroy(&clients_tcp[i].mutex))
        {
            perror("pthread_mutex_destroy client_udp");
            ret_val = -18;
        }
    }
    free(clients_udp);
    if (pthread_mutex_destroy(&mutex))
    {
        perror("pthread_mutex_destroy");
        ret_val = -19;
    }
    if (pthread_cond_destroy(&server_wait))
    {
        perror("pthread_cond_destroy server_wait");
        ret_val = -20;
    }
    if (pthread_cond_destroy(&senders_wait))
    {
        perror("pthread_cond_destroy senders_wait");
        ret_val = -21;
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
    clients_tcp = (ClientTCP*)calloc(client_tcp_count, sizeof(ClientTCP));
    for (uint64_t i = 0; i < client_tcp_count; ++i)
    {
        clients_tcp[i].soc = -1;
        clients_tcp[i].thread = NULL;
        if (pthread_mutex_init(&clients_tcp[i].mutex, NULL))
        {
            perror("pthread_mutex_init client_tcp");
            exit(-1);
        }
    }
    clients_udp = (ClientUDP*)calloc(client_udp_count, sizeof(ClientUDP));
    for (uint64_t i = 0; i < client_udp_count; ++i)
    {
        clients_udp[i].addr = NULL;
        if (pthread_mutex_init(&clients_udp[i].mutex, NULL))
        {
            perror("pthread_mutex_init client_udp");
            exit(-2);
        }
    }
    server_tcp = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);
    if (server_tcp == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create TCP socket!\n");
        exit(-3);
    }
    server_udp = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (server_udp == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create UDP socket!\n");
        exit(-4);
    }
    struct sockaddr_in addr;
    memset(&addr, 0, sizeof(addr));
    addr.sin_family = AF_INET;
    addr.sin_port = htons(port);
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    if (bind(server_tcp, (struct sockaddr*)&addr, sizeof(addr)))
    {
        perror("bind");
        fprintf(stderr, "Cannot open server_tcp on port %i!\n", port);
        exit(-5);
    }
    if (bind(server_udp, (struct sockaddr*)&addr, sizeof(addr)))
    {
        perror("bind");
        fprintf(stderr, "Cannot open server_udp on port %i!\n", port);
        exit(-6);
    }
    if (listen(server_tcp, MAX_WAIT_QUEUE))
    {
        perror("listen");
        fprintf(stderr, "Cannot listen on socket!\n");
        exit(-7);
    }
}

void stop_thread(ClientData* data)
{
    if (!pthread_mutex_lock(&clients_tcp[data->id].mutex))
    {
        soc_close(clients_tcp[data->id].soc);
        clients_tcp[data->id].soc = -1;
        if (pthread_mutex_unlock(&clients_tcp[data->id].mutex))
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
    while (operation_pending_tcp || operation_pending_udp)
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

bool unlock_server(bool tcp)
{
    if (pthread_mutex_lock(&mutex))
    {
        perror("pthread_mutex_lock unlock_server");
        return true;
    }
    if (tcp)
        operation_pending_tcp = false;
    else
        operation_pending_udp = false;
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

bool lock_server(bool tcp)
{
    if (pthread_mutex_lock(&mutex))
    {
        perror("pthread_mutex_lock unlock_server");
        return true;
    }
    if (tcp)
    {
        while (operation_pending_tcp && data_senders != 0)
        {
            if (pthread_cond_wait(&server_wait, &mutex))
            {
                perror("pthread_cond_wait lock_send");
                return false;
            }
        }
        operation_pending_tcp = true;
    }
    else
    {
        while (operation_pending_udp && data_senders != 0)
        {
            if (pthread_cond_wait(&server_wait, &mutex))
            {
                perror("pthread_cond_wait lock_send");
                return false;
            }
        }
        operation_pending_udp = true;
    }
    if (pthread_mutex_unlock(&mutex))
    {
        perror("pthread_mutex_unlock unlock_server");
        return true;
    }
    return false;
}

void* server()
{
    struct sockaddr_in addr;
    socklen_t len = sizeof(struct sockaddr_in);
    ClientData data;
    data.id = -1;
    data.buff = NULL;
    uint8_t* received_data = NULL;
    while (run_server)
    {
        if (recvfrom(server_udp, &data.size, sizeof(data.size), MSG_PEEK, (struct sockaddr*)&addr, &len) != sizeof(data.size))
        {
            if (run_server)
            {
                perror("recvfrom");
                fprintf(stderr, "Error reading header of message!\n");
            }
            break;
        }
        switch (data.size)
        {
        case -1:
        {
            recvfrom(server_udp, &data.size, sizeof(data.size), MSG_WAITALL, (struct sockaddr*)&addr, &len);
            if (lock_server(false))
            {
                if (received_data)
                    free(received_data);
                return NULL;
            }
            uint64_t i = last_used_udp_client;
            while (true)
            {
                i = (i + 1) % client_udp_count;
                if (clients_udp[i].addr == NULL)
                {
                    last_used_udp_client = i;
                    break;
                }
                else if (i == last_used_udp_client)
                {
                    last_used_udp_client = i = client_udp_count;
                    clients_udp = (ClientUDP*)realloc(clients_udp, ++client_udp_count * sizeof(ClientUDP));
                    pthread_mutex_init(&clients_udp[i].mutex, NULL);
                    break;
                }
            }
            clients_udp[i].addr = (struct sockaddr_in*)malloc(sizeof(struct sockaddr_in));
            *clients_udp[i].addr = addr;
            if (unlock_server(false))
            {
                if (received_data)
                    free(received_data);
                return NULL;
            }
            break;
        }
        case 0:
        {
            recvfrom(server_udp, &data.size, sizeof(data.size), MSG_WAITALL, NULL, NULL);
            for (uint64_t i = 0; i < client_udp_count; ++i)
            {
                bool found = false;
                if (pthread_mutex_lock(&clients_tcp[i].mutex))
                {
                    perror("pthread_mutex_lock client_udp");
                    continue;
                }
                if (clients_udp[i].addr != NULL)
                {
                    if (clients_udp[i].addr->sin_addr.s_addr == addr.sin_addr.s_addr)
                    {
                        free(clients_udp[i].addr);
                        clients_udp[i].addr = NULL;
                        found = true;
                    }
                }
                if (pthread_mutex_unlock(&clients_udp[i].mutex))
                    perror("pthread_mutex_unlock client_udp");
                if (found)
                    break;
            }
            break;
        }
        default:
        {
            data.addr = addr.sin_addr.s_addr;
            data.port = addr.sin_port;
            data.size += sizeof(data.size);
            received_data = (uint8_t*)realloc(received_data, data.size);
            data.buff = (char*)received_data + sizeof(data.size);
            uint64_t size_read = recvfrom(server_udp, received_data, data.size, MSG_WAITALL, (struct sockaddr*)&addr, &len);
            if (size_read != data.size)
            {
                perror("recvfrom");
                fprintf(stderr, "Error reading message data, expected %lu, read %lu!\n", data.size, size_read);
                free(received_data);
                return NULL;
            }
            data.size -= sizeof(data.size);
            send_all(&data);
            break;
        }
        }
    }
    if (received_data)
        free(received_data);
    return NULL;
}

void* client(void* data_ptr)
{
    ClientData* data = (ClientData*)data_ptr;
    while (true)
    {
        if (read(clients_tcp[data->id].soc, &data->size, sizeof(data->size)) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading size of data!\n");
            break;
        }
        if (data->size == 0)
            break;
        data->buff = (char*)realloc(data->buff, data->size + sizeof(data->size));
        *(u_int64_t*)data->buff = data->size;
        data->buff += sizeof(data->size);
        if (read(clients_tcp[data->id].soc, data->buff, data->size) == -1)
        {
            perror("read");
            fprintf(stderr, "Error reading data!\n");
            break;
        }
        send_all(data);
        data->buff -= sizeof(data->size);
    }
    stop_thread(data);
    return NULL;
}

void send_all(ClientData* data)
{
    lock_send(data);
    for (uint64_t i = 0; i < client_tcp_count; ++i)
    {
        if (i != data->id)
        {
            if (pthread_mutex_lock(&clients_tcp[i].mutex))
            {
                perror("pthread_mutex_lock client_tcp");
                continue;
            }
            if (clients_tcp[i].soc != -1)
            {
                if (write(clients_tcp[i].soc, &data->size, sizeof(data->size)) == -1)
                {
                    perror("write");
                    fprintf(stderr, "Error sending size of data!\n");
                }
                else if (write(clients_tcp[i].soc, data->buff, data->size) == -1)
                {
                    perror("write");
                    fprintf(stderr, "Error sending data!\n");
                }
            }
            if (pthread_mutex_unlock(&clients_tcp[i].mutex))
                perror("pthread_mutex_unlock client_tcp");
        }
    }
    for (uint64_t i = 0; i < client_udp_count; ++i)
    {
        if (pthread_mutex_lock(&clients_udp[i].mutex))
        {
            perror("pthread_mutex_lock client_udp");
            continue;
        }
        if (clients_udp[i].addr != NULL)
        {
            if (clients_udp[i].addr->sin_addr.s_addr != data->addr || clients_udp[i].addr->sin_port != data->port)
            {
                if (sendto(server_udp, data->buff - sizeof(data->size), data->size + sizeof(data->size), 0,
                    (struct sockaddr*)clients_udp[i].addr, sizeof(struct sockaddr_in)) == -1)
                {
                    perror("sendto");
                    fprintf(stderr, "Error sending data packet!\n");
                }
            }
        }
        if (pthread_mutex_unlock(&clients_udp[i].mutex))
            perror("pthread_mutex_unlock client_udp");
    }
    unlock_send(data);
}

void* cleaner()
{
    bool run = true;
    do
    {
        sleep(1);
        if (server_tcp == -1)
            run = false;
        for (uint64_t i = 0; i < client_tcp_count; ++i)
        {
            if (clients_tcp[i].thread != NULL)
            {
                const int ret = pthread_join(*clients_tcp[i].thread, NULL);
                if (ret != 0)
                    fprintf(stderr, "Cannot join thread %lu. Error number: %i", i, ret);
                if (lock_server(true))
                    break;
                free(clients_tcp[i].thread);
                clients_tcp[i].thread = NULL;
                if (unlock_server(true))
                    break;
            }
        }
    } while (run);
    return NULL;
}