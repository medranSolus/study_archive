#include "vectors.h"
#include <stdio.h>
#include <stdlib.h>
#include <time.h>
#include <float.h>
#include <string.h>
#include <unistd.h>
#include <sys/time.h>

void saveTests(const char *file, long long *timeAddSSE, long long *timeAddVec, long long *timeSubSSE, long long *timeSubVec, long long *timeMulSSE, long long *timeMulVec, long long *timeDivSSE, long long *timeDivVec);
void randomVector(void *vector, uint8_t numberType);
uint32_t getTimeDifference(struct timespec *start, struct timespec *end);

int main()
{
    srand(time(NULL));
    char *fileName[] = {"./test_2048.txt", "./test_4096.txt", "./test_8192.txt"};
    uint64_t timeAddSSE[6] = {0.0}, timeAddVec[6] = {0.0};
    uint64_t timeSubSSE[6] = {0.0}, timeSubVec[6] = {0.0};
    uint64_t timeMulSSE[3] = {0.0}, timeMulVec[3] = {0.0};
    uint64_t timeDivSSE[2] = {0.0}, timeDivVec[2] = {0.0};
    Vector8 op1i8, op2i8, desi8;
    Vector16 op1i16, op2i16, desi16;
    Vector32 op1i32, op2i32, desi32;
    Vector64 op1i64, op2i64, desi64;
    VectorF op1F, op2F, desF;
    VectorD op1D, op2D, desD;
    randomVector(&op1i8, INT8);
    randomVector(&op2i8, INT8);
    randomVector(&op1i16, INT16);
    randomVector(&op2i16, INT16);
    randomVector(&op1i32, INT32);
    randomVector(&op2i32, INT32);
    randomVector(&op1i64, INT64);
    randomVector(&op2i64, INT64);
    randomVector(&op1F, FLOAT);
    randomVector(&op2F, FLOAT);
    randomVector(&op1D, DOUBLE);
    randomVector(&op2D, DOUBLE);
    for (uint32_t i = 2048, name = 0; i < 8193; i *= 2, ++name)
    {
        for (uint16_t j = 0; j < 1000; ++j)
        {
            for (uint32_t k = 0; k < i; ++k)
            {
                timeAddSSE[0] += addSSE(&op1i8, &op2i8, &desi8, INT8, false);
                timeAddSSE[1] += addSSE(&op1i16, &op2i16, &desi16, INT16, false);
                timeAddSSE[2] += addSSE(&op1i32, &op2i32, &desi32, INT32, false);
                timeAddSSE[3] += addSSE(&op1i64, &op2i64, &desi64, INT64, false);
                timeAddSSE[4] += addSSE(&op1F, &op2F, &desF, FLOAT, true);
                timeAddSSE[5] += addSSE(&op1D, &op2D, &desD, DOUBLE, true);
                timeAddVec[0] += addVec(&op1i8, &op2i8, &desi8, INT8, false);
                timeAddVec[1] += addVec(&op1i16, &op2i16, &desi16, INT16, false);
                timeAddVec[2] += addVec(&op1i32, &op2i32, &desi32, INT32, false);
                timeAddVec[3] += addVec(&op1i64, &op2i64, &desi64, INT64, false);
                timeAddVec[4] += addVec(&op1F, &op2F, &desF, FLOAT, true);
                timeAddVec[5] += addVec(&op1D, &op2D, &desD, DOUBLE, true);

                timeSubSSE[0] += subSSE(&op1i8, &op2i8, &desi8, INT8, false);
                timeSubSSE[1] += subSSE(&op1i16, &op2i16, &desi16, INT16, false);
                timeSubSSE[2] += subSSE(&op1i32, &op2i32, &desi32, INT32, false);
                timeSubSSE[3] += subSSE(&op1i64, &op2i64, &desi64, INT64, false);
                timeSubSSE[4] += subSSE(&op1F, &op2F, &desF, FLOAT, true);
                timeSubSSE[5] += subSSE(&op1D, &op2D, &desD, DOUBLE, true);
                timeSubVec[0] += subVec(&op1i8, &op2i8, &desi8, INT8, false);
                timeSubVec[1] += subVec(&op1i16, &op2i16, &desi16, INT16, false);
                timeSubVec[2] += subVec(&op1i32, &op2i32, &desi32, INT32, false);
                timeSubVec[3] += subVec(&op1i64, &op2i64, &desi64, INT64, false);
                timeSubVec[4] += subVec(&op1F, &op2F, &desF, FLOAT, true);
                timeSubVec[5] += subVec(&op1D, &op2D, &desD, DOUBLE, true);

                timeMulSSE[0] += mulSSE(&op1i32, &op2i32, &desi64, INTEGER);
                timeMulSSE[1] += mulSSE(&op1F, &op2F, &desF, FLOAT);
                timeMulSSE[2] += mulSSE(&op1D, &op2D, &desD, DOUBLE);
                timeMulVec[0] += mulVec(&op1i32, &op2i32, &desi64, INTEGER);
                timeMulVec[1] += mulVec(&op1F, &op2F, &desF, FLOAT);
                timeMulVec[2] += mulVec(&op1D, &op2D, &desD, DOUBLE);

                timeDivSSE[0] += divSSE(&op1F, &op2F, &desF, false);
                timeDivSSE[1] += divSSE(&op1D, &op2D, &desD, true);
                timeDivVec[0] += divVec(&op1F, &op2F, &desF, false);
                timeDivVec[1] += divVec(&op1D, &op2D, &desD, true);
            }
        }
        for (uint8_t j = 0; j < 6; ++j)
        {
            timeAddSSE[j] /= 1000;
            timeAddVec[j] /= 1000;
            timeSubSSE[j] /= 1000;
            timeSubVec[j] /= 1000;
        }
        for (uint8_t j = 0; j < 3; ++j)
        {
            timeMulSSE[j] /= 1000;
            timeMulVec[j] /= 1000;
        }
        for (uint8_t j = 0; j < 2; ++j)
        {
            timeDivSSE[j] /= 1000;
            timeDivVec[j] /= 1000;
        }
        saveTests(fileName[name], timeAddSSE, timeAddVec, timeSubSSE, timeSubVec, timeMulSSE, timeMulVec, timeDivSSE, timeDivVec);
    }
    return 0;
}

void randomVector(void *vector, uint8_t numberType)
{
    switch (numberType)
    {
    case INT8:
    {
        for (uint8_t i = 0; i < 16; ++i)
            ((Vector8 *)vector)->data[i] = rand() % 255;
        break;
    }
    case INT16:
    {
        for (uint8_t i = 0; i < 8; ++i)
            ((Vector16 *)vector)->data[i] = rand() % LU;
        break;
    }
    case INT32:
    {
        for (uint8_t i = 0; i < 4; ++i)
            ((Vector32 *)vector)->data[i] = rand() % LU;
        break;
    }
    case INT64:
    {
        for (uint8_t i = 0; i < 2; ++i)
            ((Vector64 *)vector)->data[i] = rand() % LU;
        break;
    }
    case FLOAT:
    {
        for (uint8_t i = 0; i < 4; ++i)
            ((VectorF *)vector)->data[i] = ((float)rand() / (float)(RAND_MAX)) * LU;
        break;
    }
    case DOUBLE:
    {
        for (uint8_t i = 0; i < 2; ++i)
            ((VectorD *)vector)->data[i] = ((double)rand() / (double)(RAND_MAX)) * LU;
        break;
    }
    }
}

uint32_t getTimeDifference(struct timespec *start, struct timespec *end)
{
    return (end->tv_nsec - start->tv_nsec) + (end->tv_sec - start->tv_sec) * 1000000000;
}

void saveTests(const char *file, long long *timeAddSSE, long long *timeAddVec, long long *timeSubSSE, long long *timeSubVec, long long *timeMulSSE, long long *timeMulVec, long long *timeDivSSE, long long *timeDivVec)
{
    FILE *handle = fopen(file, "w");
    if (handle == NULL)
    {
        printf("Unable to create file.\n");
        exit(EXIT_FAILURE);
    }
    fprintf(handle, "---SSE---\nADD\tSUB\n");
    for (uint8_t i = 0; i < 6; ++i)
    {
        fprintf(handle, "%i\t", timeAddSSE[i]);
        fprintf(handle, "%i\n", timeSubSSE[i]);
    }
    fprintf(handle, "MUL\n");
    for (uint8_t i = 0; i < 3; ++i)
    {
        fprintf(handle, "%i\n", timeMulSSE[i]);
    }
    fprintf(handle, "DIV\n");
    for (uint8_t i = 0; i < 2; ++i)
    {
        fprintf(handle, "%i\n", timeDivSSE[i]);
    }
    fprintf(handle, "\n---VEC---\nADD\tSUB\n");
    for (uint8_t i = 0; i < 6; ++i)
    {
        fprintf(handle, "%i\t", timeAddVec[i]);
        fprintf(handle, "%i\n", timeSubVec[i]);
    }
    fprintf(handle, "MUL\n");
    for (uint8_t i = 0; i < 3; ++i)
    {
        fprintf(handle, "%i\n", timeMulVec[i]);
    }
    fprintf(handle, "DIV\n");
    for (uint8_t i = 0; i < 2; ++i)
    {
        fprintf(handle, "%i\n", timeDivVec[i]);
    }
    fclose(handle);
}

/*
    sprawko pełne i porządne
    wykresy dobre tabele i jednostki
    sensowne wykresy
    opisać proces badawczy
    opisać metodę pomiaru czasu
    opisać projekt programu
    co to simd i sisd i czemu tak zrobione
    co ma jakie wady i co jakie zalety
    WNIOSKI
    czemu co i jak jeśli takie rzeczy się dzieją
    myśli, a nie stwierdzenia
*/
