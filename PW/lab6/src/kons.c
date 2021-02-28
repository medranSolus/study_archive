#include "prodkons.h"
#include <stdlib.h>
#include <stdio.h>
#include <unistd.h>

int main(int argc, char* argv[])
{
    if (argc == 2)
    {
        mq_msg msg;
        const size_t count = atoll(argv[1]);
        mqd_t mq = mq_open(mq_name, O_RDONLY, 0660);
        unsigned int priority = 1;
        for (size_t i = 0; i < count; ++i)
        {
            if (mq_receive(mq, &msg, sizeof(msg), &priority) == -1)
            {
                perror("mq_receive");
                fprintf(stderr, "Cannot send msg!\n");
                mq_close(mq);
                return -2;
            }
            printf("Received: %s", msg.text);
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