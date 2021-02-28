#include "ftp.h"
#include<arpa/inet.h>

int main(int argc, char* argv[])
{
    bool run = true;
    ftp_msg msg = { 0 };
    int remote_fd = -1;
    int fd = -1;
    int soc = socket(AF_INET, SOCK_DGRAM, IPPROTO_UDP);
    if (soc == -1)
    {
        perror("socket");
        fprintf(stderr, "Cannot create socket!\n");
        return -1;
    }
    struct sockaddr_in addr;
    memset(&addr, 0, sizeof(addr));
    socklen_t soc_len = sizeof(addr);
    addr.sin_family = AF_INET;
    addr.sin_port = argc >= 2 ? atoi(argv[1]) : PORT;
    addr.sin_addr.s_addr = argc == 3 ? inet_addr(argv[2]) : htonl(INADDR_ANY);
    do
    {
        printf("\n[1] Start downloading file\n[2] Start upload file\n[3] Close file\n[4] Download file\n[5] Upload file\n[6] List server directory\n[7] Stop server and exit\n[8] Exit\n");
        int option = 0;
        scanf("%i", &option);
        switch (option)
        {
        case 1:
        {
            printf("Enter local file:\n");
            scanf("%s", msg.buf);
            msg.type = OPEN_FILE_R;
            if (fd != -1)
                close(fd);
            fd = open(msg.buf, O_WRONLY | O_CREAT | O_TRUNC, 0666);
            if (fd == -1)
            {
                perror("open");
                fprintf(stderr, "Cannot open file!\n");
                break;
            }
            printf("Enter server file:\n");
            scanf("%s", msg.buf);
            if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message OPEN_FILE_R!\n");
                break;
            }
            if (recvfrom(soc, &msg, sizeof(msg), MSG_WAITALL, (struct sockaddr*)&addr, &soc_len) != sizeof(msg))
            {
                perror("recvfrom");
                fprintf(stderr, "Error receiving message!\n");
            }
            if (msg.type == ERROR)
                printf("Response: error opening file!\n");
            remote_fd = msg.fd;
            break;
        }
        case 2:
        {
            printf("Enter local file:\n");
            scanf("%s", msg.buf);
            msg.type = OPEN_FILE_W;
            if (fd != -1)
                close(fd);
            fd = open(msg.buf, O_RDONLY);
            if (fd == -1)
            {
                perror("open");
                fprintf(stderr, "Cannot open file!\n");
                break;
            }
            printf("Enter server file:\n");
            scanf("%s", msg.buf);
            if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message OPEN_FILE_W!\n");
                break;
            }
            if (recvfrom(soc, &msg, sizeof(msg), MSG_WAITALL, (struct sockaddr*)&addr, &soc_len) != sizeof(msg))
            {
                perror("recvfrom");
                fprintf(stderr, "Error receiving message!\n");
            }
            if (msg.type == ERROR)
                printf("Response: error opening file!\n");
            remote_fd = msg.fd;
            break;
        }
        case 3:
        {
            msg.type = CLOSE_FILE;
            msg.fd = remote_fd;
            if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message CLOSE_FILE!\n");
            }
            break;
        }
        case 4:
        {
            msg.type = READ_FILE;
            msg.fd = remote_fd;
            if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message READ_FILE!\n");
            }
            do
            {
                if (recvfrom(soc, &msg, sizeof(msg), MSG_WAITALL, (struct sockaddr*)&addr, &soc_len) != sizeof(msg))
                {
                    perror("recvfrom");
                    fprintf(stderr, "Error receiving message!\n");
                }
                if (msg.type == UNKNOWN_HANDLE)
                {
                    printf("Response: file not open!\n");
                    break;
                }
                else if (msg.type == ERROR)
                {
                    printf("Response: error reading file!\n");
                    break;
                }
                if (write(fd, msg.buf, msg.size) != msg.size)
                {
                    perror("write");
                    fprintf(stderr, "Error writing to file!\n");
                }
            } while (msg.size == BUF_SIZE);
            break;
        }
        case 5:
        {
            msg.type = WRITE_FILE;
            msg.fd = remote_fd;
            do
            {
                msg.size = read(fd, msg.buf, BUF_SIZE);
                if (msg.size == -1)
                {
                    perror("read");
                    fprintf(stderr, "Error reading file!\n");
                    break;
                }
                if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
                {
                    perror("send");
                    fprintf(stderr, "Error sending message WRITE_FILE!\n");
                    break;
                }
            } while (msg.size == BUF_SIZE);
            break;
        }
        case 6:
        {
            printf("Enter server dir:\n");
            scanf("%s", msg.buf);
            msg.type = LIST_DIR;
            if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message LIST_DIR!\n");
            }
            printf("\n");
            do
            {
                if (recvfrom(soc, &msg, sizeof(msg), MSG_WAITALL, (struct sockaddr*)&addr, &soc_len) != sizeof(msg))
                {
                    perror("recvfrom");
                    fprintf(stderr, "Error receiving message!\n");
                }
                if (msg.type == ERROR)
                {
                    printf("Response: error reading directory!\n");
                    break;
                }
                if (msg.size)
                    printf("%s\n", msg.buf);
            } while (msg.size);
            break;
        }
        case 7:
        {
            msg.type = STOP;
            if (sendto(soc, &msg, sizeof(msg), MSG_CONFIRM, (struct sockaddr*)&addr, sizeof(addr)) != sizeof(msg))
            {
                perror("send");
                fprintf(stderr, "Error sending message STOP!\n");
            }
        }
        case 8:
        {
            run = false;
            break;
        }
        default:
        {
            printf("Unknown option!\n");
            break;
        }
        }
    } while (run);
    close(soc);
    return 0;
}