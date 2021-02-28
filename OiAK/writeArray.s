.text
.global writeArray
#IN: 1. pointer to array 2. array size in bytes
writeArray:
    push %ebp
    mov %esp, %ebp
    push %eax
    push %ebx
    push %ecx
    push %edx
    push %esi
    pushfl
    mov 12(%ebp), %ecx
    cmp $0, %ecx
    jz writeArray_quit
    mov 8(%ebp), %esi
    dec %esi
    xor %ebx, %ebx
    writeArray_loop:
        mov (%esi,%ecx), %al
        mov %al, %ah
        and $0xf0, %ah
        and $0x0f, %al
        shr $4, %ah
        cmp $9, %ah
        ja writeArray_char1
        add $0x30, %ah
        jmp writeArray_print1
        writeArray_char1:
        add $0x37, %ah
        writeArray_print1:
        movzx %ah, %ebx
        push %ebx
        call writeChar
        pop %ebx
        cmp $9, %al
        ja writeArray_char2
        add $0x30, %al
        jmp writeArray_print2
        writeArray_char2:
        add $0x37, %al
        writeArray_print2:
        movzx %al, %ebx
        push %ebx
        call writeChar
        pop %ebx
        loop writeArray_loop
    writeArray_quit:
    popfl
    pop %esi
    pop %edx
    pop %ecx
    pop %ebx
    pop %eax
    pop %ebp
    ret
