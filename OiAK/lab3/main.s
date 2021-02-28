.data
num1f: .float -10
num2f: .float 0
num3f: .float 1
num4f: .float -1
num: .4byte 0x7FFF
controlWord: .2byte 0
.text
.global _start
_start:
    finit
    push $3
    call fpuRound
    pop %eax

    push $1
    call fpuPrecision
    pop %eax
    fld num1f
    minusInf:
    fld num1f
    fdiv num2f
    plusZero:
    fld num3f
    fld num3f
    fsub %st, %st(1)
    minusZero:
    fld num4f
    fmul %st(2), %st

    push $4
    call fpuRound
    pop %eax
    push $2
    call fpuPrecision
    pop %eax
    plusInf:
    fldz
    fild num
    fdiv %st, %st(1)
    nan:
    fld num2f
    fldpi

    mov $1, %eax
    mov $0, %ebx
    int $0x80

# IN:   7.-.5.4.3.2.1.0 [byte]
# (1 for mask INT 0 to provide own handling)
# 0: Invalid operation (IM)
# 1: Denormalized operand (DM)
# 2: Zero divide (ZM)
# 3: Overflow (OM)
# 4: Underflow (UM)
# 5: Precision (PM)
# 7: Interrupt enable (IEM, not used anymore only for compability)
fpuInterrupts:
    fstcw controlWord
    movzx controlWord, %ebx
    mov 4(%esp), %eax
    and $0b10111111, %al
    or %al, %bl
    or $0b01000000, %al
    and %al, %bl
    mov %bx, controlWord
    fldcw controlWord
    ret

# IN:    (RC bits 11-10)
# 1: 00 = Round to even (default)
# 2: 01 = Round toward -infinity
# 3: 10 = Round toward +infinity
# 4: 11 = Round to zero
fpuRound:
    fstcw controlWord
    movzx controlWord, %ebx
    mov 4(%esp), %eax
    sub $1, %eax
    jz fpuRound_even
    sub $1, %eax
    jz fpuRound_minusInf
    sub $1, %eax
    jz fpuRound_plusInf
    sub $1, %eax
    jz fpuRound_trunc
    jmp fpuRound_exit
    fpuRound_even:
        and $0b1111001111111111, %ebx
        jmp fpuRound_exit
    fpuRound_minusInf:
        or $0b0000010000000000, %ebx
        and $0b1111011111111111, %ebx
        jmp fpuRound_exit
    fpuRound_plusInf:
        or $0b0000100000000000, %ebx
        and $0b1111101111111111, %ebx
        jmp fpuRound_exit
    fpuRound_trunc:
        or $0b0000110000000000, %ebx
        jmp fpuRound_exit
    fpuRound_exit:
    mov %bx, controlWord
    fldcw controlWord
    ret

# IN:    (PC bits 9-8)
# 1: 00 = 24 bits (float) 
# 2: 10 = 53 bits (double) 
# 3: 11 = 64 bits (REAL10) (default)
fpuPrecision:
    fstcw controlWord
    movzx controlWord, %ebx
    mov 4(%esp), %eax
    sub $1, %eax
    jz fpuPrecision_float
    sub $1, %eax
    jz fpuPrecision_double
    sub $1, %eax
    jz fpuPrecision_real10
    jmp fpuPrecision_exit
    fpuPrecision_float:
        and $0b1111110011111111, %ebx
        jmp fpuPrecision_exit
    fpuPrecision_double:
        or $0b0000001000000000, %ebx
        and $0b1111111011111111, %ebx
        jmp fpuPrecision_exit
    fpuPrecision_real10:
        or $0b0000001100000000, %ebx
        jmp fpuPrecision_exit
    fpuPrecision_exit:
    mov %bx, controlWord
    fldcw controlWord
    ret
