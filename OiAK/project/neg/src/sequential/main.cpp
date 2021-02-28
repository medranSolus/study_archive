#include "BMP.h"
#include <sys/time.h>
#include <fstream>
#include <iostream>

int main(int argc, char **argv)
{
    timespec start, end;
    clock_gettime(CLOCK_MONOTONIC, &start);
    BMP image;
    if (argc == 2)
    {
        image.read(argv[1]);
        image.negative();
        image.write(argv[1]);
    }
    clock_gettime(CLOCK_MONOTONIC, &end);
    unsigned long diff = (end.tv_nsec - start.tv_nsec) / 1000 + (end.tv_sec - start.tv_sec) * 1000000;
    std::ofstream fout("_imageTime.txt");
    fout << diff << ' ' << image.columns() << ' ' << image.rows() << std::endl;
    fout.close();
    return SUCCESS;
}