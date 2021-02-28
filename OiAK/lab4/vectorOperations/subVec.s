.text
.global subVec
.extern fpuPrecision
.extern getTimeDifference
.include "getTime.s"
# destination = operand1 - operand2
# IN: operand1 [VectorX *], operand2 [VectorX *], destionation [VectorX *], numberType [1 byte], isReal [1 byte]
# OUT: nanoseconds for add operation or zero if error [4 byte]
subVec:
    push %ebp
    mov %esp, %ebp
    sub $32, %esp
    pushfl
    push %ebx
    push %esi
    push %edi
    mov 8(%ebp), %esi
    mov 24(%ebp), %al
    test $1, %al
    mov 20(%ebp), %al
    jz subVec_integer
    finit
    sub $2, %al
    jz subVec_float
    sub $1, %al
    jz subVec_double
    jmp subVec_exit
    subVec_double:
        push $2
        call fpuPrecision
        pop %eax
        getTime -16
        fldl (%esi)
        fldl 8(%esi)
        mov 12(%ebp), %esi
        fsubl 8(%esi)
        fldl (%esi)
        fsubrp %st, %st(2)
        mov 16(%ebp), %esi
        fwait
        fstpl 8(%esi)
        fstpl (%esi)
        mfence
        lfence
        getTime -32
        jmp subVec_exit
    subVec_float:
        push $1
        call fpuPrecision
        pop %eax
        getTime -16
        fld (%esi)
        fld 4(%esi)
        fld 8(%esi)
        fld 12(%esi)
        mov 12(%ebp), %esi
        fsub 12(%esi)
        fld 8(%esi)
        fsubrp %st, %st(2)
        fld 4(%esi)
        fsubrp %st, %st(3)
        fld (%esi)
        fsubrp %st, %st(4)
        mov 16(%ebp), %esi
        fwait
        fstp 12(%esi)
        fstp 8(%esi)
        fstp 4(%esi)
        fstp (%esi)
        mfence
        lfence
        getTime -32
        jmp subVec_exit
    subVec_integer:
        sub $8, %al
        jz subVec_integer8
        sub $8, %al
        jz subVec_integer16
        sub $16, %al
        jz subVec_integer32
        sub $32, %al
        jz subVec_integer64
        jmp subVec_exit
        subVec_integer64:
            getTime -16
            mov 12(%ebp), %edx
            mov 16(%ebp), %edi
            mov $2, %ecx
            subVec_integer64_loop:
                dec %ecx
                mov (%esi, %ecx, 8), %eax
                sub (%edx, %ecx, 8), %eax
                mov %eax, (%edi, %ecx, 8)
                mov 4(%esi, %ecx, 8), %eax
                sbb 4(%edx, %ecx, 8), %eax
                mov %eax, 4(%edi, %ecx, 8)
                inc %ecx
                loop subVec_integer64_loop
            mfence
            lfence
            getTime -32
            jmp subVec_exit
        subVec_integer32:
            getTime -16
            mov 12(%ebp), %edx
            mov 16(%ebp), %edi
            mov $4, %ecx
            subVec_integer32_loop:
                dec %ecx
                mov (%esi, %ecx, 4), %eax
                sub (%edx, %ecx, 4), %eax
                mov %eax, (%edi, %ecx, 4)
                inc %ecx
                loop subVec_integer32_loop
            mfence
            lfence
            getTime -32
            jmp subVec_exit
        subVec_integer16:
            getTime -16
            mov 12(%ebp), %edx
            mov 16(%ebp), %edi
            mov $8, %ecx
            subVec_integer16_loop:
                dec %ecx
                mov (%esi, %ecx, 2), %ax
                sub (%edx, %ecx, 2), %ax
                mov %ax, (%edi, %ecx, 2)
                inc %ecx
                loop subVec_integer16_loop
            mfence
            lfence
            getTime -32
            jmp subVec_exit
        subVec_integer8:
            getTime -16
            mov 12(%ebp), %edx
            mov 16(%ebp), %edi
            mov $16, %ecx
            subVec_integer8_loop:
                dec %ecx
                mov (%esi, %ecx), %al
                sub (%edx, %ecx), %al
                mov %al, (%edi, %ecx)
                inc %ecx
                loop subVec_integer8_loop
            mfence
            lfence
            getTime -32
    subVec_exit:
    lea -32(%ebp), %eax
    push %eax
    lea -16(%ebp), %eax
    push %eax
    call getTimeDifference
    pop %esi
    pop %esi
    pop %edi
    pop %esi
    pop %ebx
    popfl
    add $32, %esp
    pop %ebp
    ret
