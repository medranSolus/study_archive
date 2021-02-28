.text
.global writeChar
#IN: 1. character in ascii format to write
writeChar:
    push %ebp
    mov %esp, %ebp
    push %eax
    push %ebx
    push %ecx
    push %edx
    pushfl
    mov $4, %eax
    mov $1, %ebx
    lea 8(%ebp), %ecx
    mov $1, %edx
    int $0x80
    popfl
    pop %edx
    pop %ecx
    pop %ebx
    pop %eax
    pop %ebp
    ret
