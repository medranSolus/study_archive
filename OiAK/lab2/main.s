.data
number1: .4byte 0x0F11FF22, 0xFF78FFF6, 0xEF7F1F9D
numberLength = (. - number1)/4
number2: .4byte 0x1FBBF33F, 0xF2F45F11, 0xCFFF0005
numberSize = . - number2

.text
.global _start
_start:
    call addLong
    call subLong
    call mulLong
    mov $1, %eax
    mov $0, %ebx
    int $0x80

addLong:
    pop %ebx
    mov $numberLength, %ecx
    xor %esi, %esi
    clc
    addLong_loop:
        mov number1(,%esi, 4), %eax
        adc number2(,%esi, 4), %eax
        push %eax
        inc %esi
        loop addLong_loop
    mov $0, %eax
    jnc addLong_quit
    inc %eax
    addLong_quit:
    push %eax
    addBreak:
    push %ebx
    ret

subLong:
    pop %ebx
    mov $numberLength, %ecx
    xor %esi, %esi
    clc
    subLong_loop:
        mov number1(,%esi, 4), %eax
        sbb number2(,%esi, 4), %eax
        push %eax
        inc %esi
        loop subLong_loop
    mov $0, %eax
    jnc subLong_quit
    dec %eax
    subLong_quit:
    push %eax
    subBreak:
    push %ebx
    ret

mulLong:
    pop return
    xor %esi, %esi
    xor %ebp, %ebp
    mov $numberLength, %ecx
    mulLong_loopSecondOP:
        push %ebp
        push %ecx
        mov $numberLength, %ecx
        xor %edi, %edi
        mulLong_loopFirstOP:
            mov number1(,%edi,4), %eax
            mull number2(,%esi,4)
            clc
            add %eax, product(,%ebp,4)
            inc %ebp
            adc %edx, product(,%ebp,4)
            jnc mulLong_loopFirstOPNext
            push %ebp
            mov $1, %eax
            mulLong_loopCarry:
                inc %ebp
                add %eax, product(,%ebp,4)
                jc mulLong_loopCarry
            pop %ebp
            mulLong_loopFirstOPNext:
            inc %edi
            loop mulLong_loopFirstOP
        pop %ecx
        pop %ebp
        inc %ebp
        inc %esi
        loop mulLong_loopSecondOP
    mov $numberLength, %ecx
    add $numberLength, %ecx
    xor %esi, %esi
    mulLong_storeLoop:
        push product(,%esi, 4)
        inc %esi
        loop mulLong_storeLoop
    mulBreak:
    push return
    ret

.bss
.lcomm return, 4
.lcomm product, numberSize * 2
