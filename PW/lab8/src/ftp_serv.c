#include "ftp.h"
#include <dirent.h>

int main(int argc, char* argv[])
{
    int serv_soc = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (serv_soc == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create socket!\n");
        return -1;
    }
    struct sockaddr_in addr;
    memset(&addr, 0, sizeof(addr));
    socklen_t soc_len = sizeof(addr);
    addr.sin_family = AF_INET;
    addr.sin_port = argc == 2 ? atoi(argv[1]) : PORT;
    addr.sin_addr.s_addr = htonl(INADDR_ANY);
    if (bind(serv_soc, (struct sockaddr*)&addr, soc_len))
    {
        perror("bind");
        fprintf(stderr, "Cannot open server on port %i!\n", addr.sin_port);
        return -2;
    }
    ftp_msg msg;
    int fd = -1;
    bool run = true;
    do
    {
        if (recvfrom(serv_soc, &msg, sizeof(msg), MSG_WAITALL, (struct sockaddr*)&addr, &soc_len) != sizeof(msg))
        {
            perror("recvfrom");
            fprintf(stderr, "Error receiving message!\n");
            continue;
        }
        switch (msg.type)
        {
        case OPEN_FILE_R:
        {
            if (fd != -1)
                close(fd);
            fd = open(msg.buf, O_RDONLY);
            if (fd == -1)
            {
                perror("open");
                fprintf(stderr, "Cannot open file to read: &s!\n", msg.buf);
                msg.type = ERROR;
            }
            msg.fd = fd;
            if (sendto(serv_soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, soc_len) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message OPEN_FILE_R!\n");
            }
            break;
        }
        case OPEN_FILE_W:
        {
            if (fd != -1)
                close(fd);
            fd = open(msg.buf, O_WRONLY | O_CREAT | O_TRUNC, 0666);
            if (fd == -1)
            {
                perror("open");
                fprintf(stderr, "Cannot open file to write: &s!\n", msg.buf);
                msg.type = ERROR;
            }
            msg.fd = fd;
            if (sendto(serv_soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, soc_len) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message OPEN_FILE_W!\n");
            }
            break;
        }
        case CLOSE_FILE:
        {
            if (fd == msg.fd && fd != -1)
            {
                close(msg.fd);
                fd = -1;
            }
            break;
        }
        case READ_FILE:
        {
            if (fd != msg.fd)
            {
                msg.type = UNKNOWN_HANDLE;
                if (sendto(serv_soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, soc_len) != sizeof(msg))
                {
                    perror("send");
                    fprintf(stderr, "Error sending message READ_FILE!\n");
                }
            }
            else
            {
                do
                {
                    msg.size = read(msg.fd, msg.buf, BUF_SIZE);
                    if (msg.size == -1)
                    {
                        perror("read");
                        fprintf(stderr, "Error reading file!\n");
                        msg.type = ERROR;
                    }
                    if (sendto(serv_soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, soc_len) != sizeof(msg))
                    {
                        perror("send");
                        fprintf(stderr, "Error sending message READ_FILE!\n");
                        break;
                    }
                } while (msg.size == BUF_SIZE);
            }
            break;
        }
        case WRITE_FILE:
        {
            if (fd == msg.fd && fd != -1)
            {
                if (msg.size <= BUF_SIZE)
                {
                    if (write(msg.fd, msg.buf, msg.size) != msg.size)
                    {
                        perror("write");
                        fprintf(stderr, "Error writing to file!\n");
                    }
                }
                else
                    fprintf(stderr, "Requested size to write exceeding BUF_SIZE: %i!\n", BUF_SIZE);
            }
            break;
        }
        case LIST_DIR:
        {
            DIR* dir = opendir(msg.buf);
            if (dir != NULL)
            {
                struct dirent* entry = NULL;
                msg.size = 1;
                do
                {
                    entry = readdir(dir);
                    if (entry == NULL)
                        msg.size = 0;
                    else if (strcmp(entry->d_name, ".") && strcmp(entry->d_name, ".."))
                        memcpy(msg.buf, entry->d_name, sizeof(entry->d_name));
                    else
                        continue;
                    if (sendto(serv_soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, soc_len) != sizeof(msg))
                    {
                        perror("send");
                        fprintf(stderr, "Error sending message LIST_DIR!\n");
                    }
                } while (entry != NULL);
                closedir(dir);
            }
            else
            {
                msg.type = ERROR;
                if (sendto(serv_soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, soc_len) != sizeof(msg))
                {
                    perror("send");
                    fprintf(stderr, "Error sending message LIST_DIR!\n");
                }
            }
            break;
        }
        case STOP:
        {
            run = false;
            printf("Server closing.\n");
            break;
        }
        default:
        {
            fprintf(stderr, "Unknow message type: %i!\n", msg.type);
            break;
        }
        }
    } while (run);
    close(serv_soc);
    if (fd != -1)
        close(fd);
    return 0;
}