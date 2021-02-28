.text
.global mulVec
.extern fpuPrecision
.extern getTimeDifference
.include "getTime.s"
# destination = <operand1[0] * operand2[0]; operand1[2] * operand2[2]>
# IN: operand1 [VectorX *], operand2 [VectorX *], destionation [VectorX *], numberType [1 byte]
# OUT: nanoseconds for add operation or zero if error [4 byte]
mulVec:
    push %ebp
    mov %esp, %ebp
    sub $32, %esp
    pushfl
    push %ebx
    push %esi
    push %edi
    mov 8(%ebp), %esi
    mov 20(%ebp), %al
    sub $1, %al
    jz mulVec_integer
    finit
    sub $1, %al
    jz mulVec_float
    sub $1, %al
    jz mulVec_double
    jmp mulVec_exit
    mulVec_double:
        push $2
        call fpuPrecision
        pop %eax
        getTime -16
        fldl (%esi)
        fldl 8(%esi)
        mov 12(%ebp), %esi
        fmull 8(%esi)
        fldl (%esi)
        fmulp %st, %st(2)
        mov 16(%ebp), %esi
        fwait
        fstpl 8(%esi)
        fstpl (%esi)
        mfence
        lfence
        getTime -32
        jmp mulVec_exit
    mulVec_float:
        push $1
        call fpuPrecision
        pop %eax
        getTime -16
        fld (%esi)
        fld 4(%esi)
        fld 8(%esi)
        fld 12(%esi)
        mov 12(%ebp), %esi
        fmul 12(%esi)
        fld 8(%esi)
        fmulp %st, %st(2)
        fld 4(%esi)
        fmulp %st, %st(3)
        fld (%esi)
        fmulp %st, %st(4)
        mov 16(%ebp), %esi
        fwait
        fstp 12(%esi)
        fstp 8(%esi)
        fstp 4(%esi)
        fstp (%esi)        
        mfence
        lfence
        getTime -32
        jmp mulVec_exit
    mulVec_integer:
        getTime -16
        mov 12(%ebp), %ebx
        mov 16(%ebp), %edi
        mov $2, %ecx
        mulVec_integer_loop:
            dec %ecx
            mov (%esi, %ecx, 8), %eax
            imull (%ebx, %ecx, 8)
            mov %eax, (%edi, %ecx, 8)
            mov %edx, 4(%edi, %ecx, 8)
            inc %ecx
            loop mulVec_integer_loop
        mfence
        lfence
        getTime -32
    mulVec_exit:
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
