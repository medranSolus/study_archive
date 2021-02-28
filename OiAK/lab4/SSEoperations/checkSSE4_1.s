.text
.global checkSSE4_1

checkSSE4_1:
    push %ebx
    mov $1, %eax
    cpuid
    test $1<<19, %ecx
    setnz %al
    pop %ebx
    ret
