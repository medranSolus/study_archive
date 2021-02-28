.text
.global divVec
.extern getTimeDifference
.include "getTime.s"
# destination = operand1 / operand2
# IN: operand1 [VectorX *], operand2 [VectorX *], destionation [VectorX *], extended [1 byte]
# OUT: nanoseconds for add operation or zero if error [4 byte]
divVec:
    push %ebp
    mov %esp, %ebp
    sub $32, %esp
    pushfl
    push %ebx
    push %esi
    finit
    mov 8(%ebp), %esi
    mov 20(%ebp), %al
    test $1, %al
    jz divVec_float
    jmp divVec_double
    divVec_double:
        push $2
        call fpuPrecision
        pop %eax
        getTime -16
        fldl (%esi)
        fldl 8(%esi)
        mov 12(%ebp), %esi
        fdivl 8(%esi)
        fldl (%esi)
        fdivrp %st, %st(2)
        mov 16(%ebp), %esi
        fwait
        fstpl 8(%esi)
        fstpl (%esi)   
        mfence
        lfence
        getTime -32
        jmp divVec_exit
    divVec_float:
        push $1
        call fpuPrecision
        pop %eax
        getTime -16
        fld (%esi)
        fld 4(%esi)
        fld 8(%esi)
        fld 12(%esi)
        mov 12(%ebp), %esi
        fdiv 12(%esi)
        fld 8(%esi)
        fdivrp %st, %st(2)
        fld 4(%esi)
        fdivrp %st, %st(3)
        fld (%esi)
        fdivrp %st, %st(4)
        mov 16(%ebp), %esi
        fwait
        fstp 12(%esi)
        fstp 8(%esi)
        fstp 4(%esi)
        fstp (%esi)
        mfence
        lfence
        getTime -32
    divVec_exit:
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
