.text
.global subLong
#IN: 1. destination operand address 2. source operand address 3. number size in bytes (des-=src)
subLong:
    push %ebp
    mov %esp, %ebp
    push %eax
    push %ecx
    push %esi
    push %edi
    pushfl
    mov 8(%ebp), %edi
    mov 12(%ebp), %esi
    mov 16(%ebp), %ecx
    clc
    subLong_loop:
        mov (%esi), %al
        sbb %al, (%edi)
        inc %esi
        inc %edi
        loop subLong_loop
    popfl
    pop %edi
    pop %esi
    pop %ecx
    pop %eax
    pop %ebp
    ret
