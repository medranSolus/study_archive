#define _POSIX_C_SOURCE 200809L
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>

#define PORT 4200
#define BUF_SIZE 256

typedef enum
{
    OPEN_FILE_R,
    OPEN_FILE_W,
    CLOSE_FILE,
    READ_FILE,
    WRITE_FILE,
    OPEN_DIR,
    LIST_DIR,
    STOP,
    UNKNOWN_HANDLE,
    ERROR
} msg_type;

typedef struct
{
    msg_type type;
    int size;
    int fd;
    char buf[BUF_SIZE];
} ftp_msg;