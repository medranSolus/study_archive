.text
.global divSSE
.extern getTimeDifference
.include "getTime.s"
# Requirements: SSE4_1
# destination = operand1 / operand2
# IN: operand1 [VectorX *], operand2 [VectorX *], destionation [VectorX *], extended [1 byte]
# OUT: nanoseconds for add operation or zero if error [4 byte]
divSSE:
    push %ebp
    mov %esp, %ebp
    sub $32, %esp
    pushfl
    push %ebx
    push %esi
    mov 8(%ebp), %esi
    mov 20(%ebp), %al
    test $1, %al
    jz divSSE_float
    jmp divSSE_double
    divSSE_double:
        getTime -16
        movapd (%esi), %xmm1
        mov 12(%ebp), %esi
        divpd (%esi), %xmm1
        mov 16(%ebp), %esi
        movapd %xmm1, (%esi)
        mfence
        lfence
        getTime -32
        jmp divSSE_exit
    divSSE_float:
        getTime -16
        movaps (%esi), %xmm1
        mov 12(%ebp), %esi
        divps (%esi), %xmm1
        mov 16(%ebp), %esi
        movaps %xmm1, (%esi)
        mfence
        lfence
        getTime -32
    divSSE_exit:
    lea -32(%ebp), %eax
    push %eax
    lea -16(%ebp), %eax
    push %eax
    call getTimeDifference
    pop %esi
    pop %esi
    pop %esi
    pop %ebx
    popfl
    add $32, %esp
    pop %ebp
    ret
