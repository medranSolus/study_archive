SYSEXIT = 1
EXIT_SUCCESS = 0
SYSWRITE = 4
STDOUT = 1
SYSREAD = 3
STDIN = 0

.data
msg: .ascii "Write text (5): \n"
msg_len = . - msg
msgEqual: .ascii "Input is equal to <text1>\n"
msgEqualLength= . - msgEqual
msgNotEqual: .ascii "Input is not equal to <text1>\n"
msgNotEqualLength= . - msgNotEqual
compareText: .ascii "text1"
compareTextLength = . - compareText

buf: .ascii "     "
buf_len = . - buf
flushByte: .byte 0

.text
.global _start

_start:
    mov $SYSWRITE, %eax
    mov $STDOUT, %ebx
    mov $msg, %ecx
    mov $msg_len, %edx
    int $0x80

    mov $SYSREAD, %eax
    mov $STDIN, %ebx
    mov $buf, %ecx
    mov $buf_len, %edx
    int $0x80

    mov $buf_len, %ecx
    mov $10, %ah
    mov $buf, %esi
    checkNewLineLoop:
        lodsb
        cmp %al, %ah
        je compare
        loop checkNewLineLoop
    flushLoop:
        mov $SYSREAD, %eax
        mov $STDIN, %ebx
        mov $flushByte, %ecx
        mov $1, %edx
        int $0x80
        mov $10, %ah
        cmp %ah, (flushByte)
        je compare
        jmp flushLoop
    compare:
    mov $buf, %esi
    mov $0, %edi
    mov $buf_len, %ecx
    compareLoop:
        lodsb
        cmp %al, compareText(%edi)
        jne notEqal
        inc %edi
        loop compareLoop
    mov $msgEqual, %ecx
    mov $msgEqualLength, %edx
    jmp quit
    notEqal:
    mov $msgNotEqual, %ecx
    mov $msgNotEqualLength, %edx
    quit:
    mov $SYSWRITE, %eax
    mov $STDOUT, %ebx
    int $0x80

    mov $SYSEXIT, %eax
    mov $EXIT_SUCCESS, %ebx
    int $0x80
