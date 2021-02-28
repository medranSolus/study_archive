.text
.global addSSE
.extern getTimeDifference
.include "getTime.s"
# Requirements: SSE2
# destination = operand1 + operand2
# IN: operand1 [VectorX *], operand2 [VectorX *], destionation [VectorX *], numberType [1 byte], isReal [1 byte]
# OUT: nanoseconds for add operation or zero if error [4 byte]
addSSE:
    push %ebp
    mov %esp, %ebp
    sub $32, %esp
    pushfl
    push %ebx
    push %esi
    mov 8(%ebp), %esi
    mov 24(%ebp), %al
    test $1, %al
    mov 20(%ebp), %al
    jz addSSE_integer
    sub $2, %al
    jz addSSE_float
    sub $1, %al
    jz addSSE_double
    jmp addSSE_exit
    addSSE_double:
        getTime -16
        movapd (%esi), %xmm1
        mov 12(%ebp), %esi
        addpd (%esi), %xmm1
        mov 16(%ebp), %esi
        movapd %xmm1, (%esi)
        mfence
        lfence
        getTime -32
        jmp addSSE_exit
    addSSE_float:
        getTime -16
        mov %eax, -4(%ebp)
        movaps (%esi), %xmm1
        mov 12(%ebp), %esi
        addps (%esi), %xmm1
        mov 16(%ebp), %esi
        movaps %xmm1, (%esi)
        mfence
        lfence
        getTime -32
        jmp addSSE_exit
    addSSE_integer:
        sub $8, %al
        jz addSSE_integer8
        sub $8, %al
        jz addSSE_integer16
        sub $16, %al
        jz addSSE_integer32
        sub $32, %al
        jz addSSE_integer64
        jmp addSSE_exit
        addSSE_integer64:
            getTime -16
            movdqa (%esi), %xmm1
            mov 12(%ebp), %esi
            paddq (%esi), %xmm1
            mov 16(%ebp), %esi
            movdqa %xmm1, (%esi)
            mfence
            lfence
            getTime -32
            jmp addSSE_exit
        addSSE_integer32:
            getTime -16
            movdqa (%esi), %xmm1
            mov 12(%ebp), %esi
            paddd (%esi), %xmm1
            mov 16(%ebp), %esi
            movdqa %xmm1, (%esi)
            mfence
            lfence
            getTime -32
            jmp addSSE_exit
        addSSE_integer16:
            getTime -16
            movdqa (%esi), %xmm1
            mov 12(%ebp), %esi
            paddw (%esi), %xmm1
            mov 16(%ebp), %esi
            movdqa %xmm1, (%esi)
            mfence
            lfence
            getTime -32
            jmp addSSE_exit
        addSSE_integer8:
            getTime -16
            movdqa (%esi), %xmm1
            mov 12(%ebp), %esi
            paddb (%esi), %xmm1
            mov 16(%ebp), %esi
            movdqa %xmm1, (%esi)
            mfence
            lfence
            getTime -32
    addSSE_exit:
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
