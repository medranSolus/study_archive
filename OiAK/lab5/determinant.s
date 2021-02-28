.text
.global determinant
.include "calcVec.s"
# IN: pointer to matrix1 4x4 with float values [4 bytes], pointer to matrix2 4x4 with float values [4 bytes], pointer to table with 2 float values as determinant [4 bytes]
determinant:
    push %ebp
    mov %esp, %ebp
    push %esi
    push %edi
    mov 8(%ebp), %esi
    mov 12(%ebp), %edi
    calcVecLong 0, 16, %esi, %edi, $0xEE, $0x77, %xmm2, %xmm3, %xmm4, %xmm5, %xmm6
    calcVecLong 32, 48, %esi, %edi, $0, $0x99, %xmm3, %xmm4, %xmm5, %xmm6, %xmm7
    movaps 32(%esi), %xmm5
    movaps 48(%esi), %xmm6
    calcVec $0x16, $0xEF, %xmm5, %xmm6, %xmm7, %xmm1
    movaps 32(%edi), %xmm4
    movaps 48(%edi), %xmm6
    calcVec $0x16, $0xEF, %xmm4, %xmm6, %xmm7, %xmm5
    movaps (%esi), %xmm6
    movaps 16(%esi), %xmm7
    calcVecQuick $0x48, $0xB1, $0x1D, %xmm6, %xmm7, %xmm0
    movaps (%edi), %xmm6
    movaps 16(%edi), %xmm7
    calcVecQuick $0x48, $0xB1, $0x1D, %xmm6, %xmm7, %xmm4
    mulps %xmm1, %xmm0
    mulps %xmm3, %xmm2
    mulps %xmm5, %xmm4
    movaps %xmm4, %xmm5
    shufps $0x4E, %xmm0, %xmm5
    shufps $0xE4, %xmm0, %xmm4
    addps %xmm5, %xmm4
    addps %xmm4, %xmm2
    movaps %xmm2, %xmm0
    shufps $0xB1, %xmm2, %xmm2
    addps %xmm2, %xmm0
    shufps $0xC6, %xmm0, %xmm0
    mov 16(%ebp), %edi
    movlps %xmm0, (%edi)
    pop %edi
    pop %esi
    pop %ebp
    ret
# 4 8 1 5 7 1 2 6 2 5 6 0 0  2
# 21 54 0 1 55 1 6 9 1 3 4 6 8 2 3 7
