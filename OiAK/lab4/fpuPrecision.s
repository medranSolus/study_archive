.text
.global fpuPrecision
# IN:    (PC bits 9-8)
# 1: 00 = 24 bits (float) 
# 2: 10 = 53 bits (double) 
# 3: 11 = 64 bits (REAL10) (default)
fpuPrecision:
    push $0
    fstcw (%esp)
    movzx (%esp), %ecx
    mov 8(%esp), %eax
    sub $1, %eax
    jz fpuPrecision_float
    sub $1, %eax
    jz fpuPrecision_double
    sub $1, %eax
    jz fpuPrecision_real10
    jmp fpuPrecision_exit
    fpuPrecision_float:
        and $0b1111110011111111, %ecx
        jmp fpuPrecision_exit
    fpuPrecision_double:
        or $0b0000001000000000, %ecx
        and $0b1111111011111111, %ecx
        jmp fpuPrecision_exit
    fpuPrecision_real10:
        or $0b0000001100000000, %ecx
        jmp fpuPrecision_exit
    fpuPrecision_exit:
    mov %cx, (%esp)
    fldcw (%esp)
    pop %eax
    ret
