SYSEXIT = 1
EXIT_SUCCESS = 0
SYSWRITE = 4
STDOUT = 1
SYSREAD = 3
STDIN = 0

.data
msg: .ascii "Write text (5): \n"
msg_len = . - msg
msg2: .ascii "Written text: "
msg2_len = . - msg2
newline: .ascii "\n"
newline_len = . - newline

buf: .ascii "     "
buf_len = . - buf
isNewLine: .byte 0
flushByte: .byte 0
inputLength: .4byte 0

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

    mov %eax, (inputLength)
    mov $buf_len, %ecx
    mov $10, %ah
    mov $buf, %esi
    checkNewLineLoop:
        lodsb
        cmp %al, %ah
        je newLineFound
        loop checkNewLineLoop
    flushLoop:
        mov $SYSREAD, %eax
        mov $STDIN, %ebx
        mov $flushByte, %ecx
        mov $1, %edx
        int $0x80
        mov $10, %ah
        cmp %ah, (flushByte)
        je writeMsg
        jmp flushLoop
    jmp writeMsg
    newLineFound:
    mov %al, (isNewLine)
    writeMsg:
    mov $SYSWRITE, %eax
    mov $STDOUT, %ebx
    mov $msg2, %ecx
    mov $msg2_len, %edx
    int $0x80

    mov $SYSWRITE, %eax
    mov $STDOUT, %ebx
    mov $buf, %ecx
    mov (inputLength), %edx
    int $0x80

    mov $0, %al
    cmp %al, (isNewLine)
    jne quit
    mov $SYSWRITE, %eax
    mov $STDOUT, %ebx
    mov $newline, %ecx
    mov $newline_len, %edx
    int $0x80
    quit:
    mov $SYSEXIT, %eax
    mov $EXIT_SUCCESS, %ebx
    int $0x80
