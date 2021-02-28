#include <stdbool.h>
#include <stdlib.h>
#include <stdio.h>

unsigned int get_first_count(unsigned int start, unsigned int end);
bool is_first(int n);

int main(int argc, char* argv[])
{
    unsigned int count = get_first_count(abs(atoi(argv[1])), abs(atoi(argv[2])));
    if (count > 255)
        fprintf(stderr, "Warning, count overflow! [%u]\n", count);
    return count;
}

unsigned int get_first_count(unsigned int start, unsigned int end)
{
    unsigned int count = 0;
    for (; start < end; ++start)
        if(is_first(start))
            ++count;
    return count;
}

bool is_first(int n)
{
    for (int i = 2; i*i <= n; ++i)
        if (n%i == 0)
            return false;
    return true;
} 