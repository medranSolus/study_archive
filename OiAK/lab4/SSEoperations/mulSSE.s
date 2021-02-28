.text
.global mulSSE
.extern getTimeDifference
.include "getTime.s"
# Requirements: SSE4_1
# numberType = INTEGER: destination = <operand1[0] * operand2[0]; operand1[2] * operand2[2]>
# IN: operand1 [VectorX *], operand2 [VectorX *], destionation [VectorX *], numberType [1 byte]
# OUT: nanoseconds for add operation or zero if error [4 byte]
mulSSE:
    push %ebp
    mov %esp, %ebp
    sub $32, %esp
    pushfl
    push %ebx
    push %esi
    mov 8(%ebp), %esi
    mov 20(%ebp), %al
    sub $1, %al
    jz mulSSE_integer
    sub $1, %al
    jz mulSSE_float
    sub $1, %al
    jz mulSSE_double
    jmp mulSSE_exit
    mulSSE_double:
        getTime -16
        movapd (%esi), %xmm1
        mov 12(%ebp), %esi
        mulpd (%esi), %xmm1
        mov 16(%ebp), %esi
        movapd %xmm1, (%esi)
        mfence
        lfence
        getTime -32
        jmp mulSSE_exit
    mulSSE_float:
        getTime -16
        movaps (%esi), %xmm1
        mov 12(%ebp), %esi
        mulps (%esi), %xmm1
        mov 16(%ebp), %esi
        movaps %xmm1, (%esi)
        mfence
        lfence
        getTime -32
        jmp mulSSE_exit
    mulSSE_integer:
        getTime -16
        movdqa (%esi), %xmm1
        mov 12(%ebp), %esi
        pmuldq (%esi), %xmm1
        mov 16(%ebp), %esi
        movdqa %xmm1, (%esi)
        mfence
        lfence
        getTime -32
    mulSSE_exit:
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
