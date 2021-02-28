.text
.global checkSSE2

checkSSE2:
    push %rbx
    mov $1, %rax
    cpuid
    test $1<<26, %edx
    setnz %al
    pop %rbx
    ret
