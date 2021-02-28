#include <mqueue.h>

typedef struct
{
    pid_t id;
    char text[512];
} mq_msg;

const char mq_name[] = "/prodkons.mq";