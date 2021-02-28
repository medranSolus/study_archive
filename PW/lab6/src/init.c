#include "prodkons.h"
#include <stdio.h>

int main()
{
    struct mq_attr attr;
    attr.mq_msgsize = sizeof(mq_msg);
    attr.mq_maxmsg = 4;
    attr.mq_flags = 0;
    mqd_t mq = mq_open(mq_name, O_RDWR | O_CREAT, 0660, &attr);
    if (mq == -1)
    {
        perror("mq_open");
        fprintf(stderr, "Cannot create mq!\n");
        return -1;
    }
    mq_close(mq);
    return 0;
}