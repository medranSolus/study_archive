.text
.global checkSSE2

checkSSE2:
    push %ebx
    mov $1, %eax
    cpuid
    test $1<<26, %edx
    setnz %al
    pop %ebx
    ret
