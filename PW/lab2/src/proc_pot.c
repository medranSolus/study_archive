#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>

int main(int argc, char* argv[])
{
    const int pid = atoi(argv[1]);
    const int steps = atoi(argv[2]);
    for (int i = 1; i <= steps; ++i)
    {
        printf("Proces %d krok %d\n", pid, i);
        sleep(1);
    }
    printf("Proces %d zakonczony\n", pid);
    return pid;
}