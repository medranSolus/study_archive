.text
.global mulLong
#IN: 1. first operand address 2. second operand address 3. number size in bytes (first * second = second:first)
mulLong:
    push %ebp
    mov %esp, %ebp
    push %eax
    push %ebx
    push %ecx
    push %edx
    push %esi
    push %edi
    pushfl
    xor %edx, %edx
    mov 12(%ebp), %esi
    mov 16(%ebp), %ecx
    mulLong_loopSecondOP:
        mov (%esi), %bl
        mov 8(%ebp), %edi
        push %ecx
        push %edx
        mov 16(%ebp), %ecx
        mulLong_loopFirstOP:
            mov (%edi), %al
            mulb %bl
            addw %ax, product(%edx)
            inc %edx
            jnc mulLong_loopFirstOPNext
            inc %edx
            incb product(%edx)
            dec %edx
            mulLong_loopFirstOPNext:
            inc %edi
            xor %ax, %ax
            loop mulLong_loopFirstOP
        pop %edx
        pop %ecx
        inc %edx
        inc %esi
        loop mulLong_loopSecondOP
    mov 16(%ebp), %ecx
    mov $product, %esi
    mov 8(%ebp), %edi
    call mulLong_store
    mov 16(%ebp), %ecx
    mov 12(%ebp), %edi
    call mulLong_store
    popfl
    pop %edi
    pop %esi
    pop %edx
    pop %ecx
    pop %ebx
    pop %eax
    pop %ebp
    ret

#ECX = size, ESI = source, EDI = destination
mulLong_store:
    mov (%esi), %al
    mov %al, (%edi)
    inc %esi
    inc %edi
    loop mulLong_store
    ret

.bss
.lcomm product, 200
