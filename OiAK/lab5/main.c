#include "matrix.h"
#include <stdio.h>
#include <stdint.h>

extern void determinant(Matrix *matrix1, Matrix *matrix2, float dets[2]);

void getMatrix(Matrix *matrix);

int main()
{
    float dets[2] = {0.0f};
    Matrix m1, m2;
    getMatrix(&m1);
    getMatrix(&m2);
    determinant(&m1, &m2, dets);
    printf("Det1: %f\n", dets[0]);
    printf("Det2: %f\n", dets[1]);
    return 0;
}

void getMatrix(Matrix *matrix)
{
    printf("Podaj kolejne wiersze macierzy 4x4 (16 liczb):\n");
    for (uint8_t i = 0; i < 4; ++i)
    {
        printf("Wiersz %i: ", i + 1);
        for (uint8_t j = 0; j < 4; ++j)
        {
            scanf("%f", matrix->data[i] + j);
        }
    }
}