#include "prodkons.h"
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>

int main(int argc, char* argv[])
{
    if (argc == 3)
    {
        mq_msg msg;
        msg.id = atoi(argv[1]);
        const size_t count = atoll(argv[2]);
        mqd_t mq = mq_open(mq_name, O_WRONLY, 0660);
        for (size_t i = 0; i < count; ++i)
        {
            sprintf(msg.text, "Prod id=%i, i=%i", msg.id, i);
            if (mq_send(mq, &msg, sizeof(msg), 1) == -1)
            {
                perror("mq_send");
                fprintf(stderr, "Cannot send msg!\n");
                mq_close(mq);
                return -2;
            }
            sleep(1);
        }
        mq_close(mq);
        return 0;
    }
    else
    {
        fprintf(stderr, "Not enought params!\n");
        return -1;
    }
}