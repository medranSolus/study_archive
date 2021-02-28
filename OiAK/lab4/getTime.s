# IN: offset(%ebp) = begining of struct timespec
.macro getTime offset
    lea \offset(%ebp), %ecx
    mov $2, %ebx
    mov $265, %eax
    int $0x80
.endm
