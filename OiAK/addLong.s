.text
.global addLong
#IN: 1. destination operand address 2. source operand address 3. number size in bytes (des+=src)
addLong:
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
    addLong_loop:
        mov (%esi), %al
        adc %al, (%edi)
        inc %esi
        inc %edi
        loop addLong_loop
    popfl
    pop %edi
    pop %esi
    pop %ecx
    pop %eax
    pop %ebp
    ret
