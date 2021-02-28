#ifndef VECTORS_H
#define VECTORS_H
#include <stdbool.h>
#include <stdint.h>
#define INTEGER 1
#define FLOAT 2
#define DOUBLE 3
#define INT8 8
#define INT16 16
#define INT32 32
#define INT64 64
#define LU 3000

extern bool checkSSE2();
extern bool checkSSE4_1();
extern uint32_t addSSE(void *operand1, void *operand2, void *destination, uint8_t numberType, bool isReal);
extern uint32_t subSSE(void *operand1, void *operand2, void *destination, uint8_t numberType, bool isReal);
extern uint32_t mulSSE(void *operand1, void *operand2, void *destination, uint8_t numberType);
extern uint32_t divSSE(void *operand1, void *operand2, void *destination, bool extended);
extern uint32_t addVec(void *operand1, void *operand2, void *destination, uint8_t numberType, bool isReal);
extern uint32_t subVec(void *operand1, void *operand2, void *destination, uint8_t numberType, bool isReal);
extern uint32_t mulVec(void *operand1, void *operand2, void *destination, uint8_t numberType);
extern uint32_t divVec(void *operand1, void *operand2, void *destination, bool extended);

#pragma pack(push, 16)
typedef struct
{
    int64_t data[2];
} Vector64 __attribute__((aligned(16)));

typedef struct
{
    int32_t data[4];
} Vector32 __attribute__((aligned(16)));

typedef struct
{
    int16_t data[8];
} Vector16 __attribute__((aligned(16)));

typedef struct
{
    int8_t data[16];
} Vector8 __attribute__((aligned(16)));

typedef struct
{
    double data[2];
} VectorD __attribute__((aligned(16)));

typedef struct
{
    float data[4];
} VectorF __attribute__((aligned(16)));
#pragma pack(pop)

#endif