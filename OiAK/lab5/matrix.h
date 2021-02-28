#ifndef MATRIX_H
#define MATRIX_H

#pragma pack(push, 16)
typedef struct
{
    float data[4][4];
} Matrix __attribute__((aligned(16)));
#pragma pack(pop)

#endif