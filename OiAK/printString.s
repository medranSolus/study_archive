.text
.global printString
#IN: 1. pointer to null terminated string
printString:
    push %ebp
    mov %esp, %ebp
    push %eax
    push %ebx
    push %ecx
    push %edx
    push %edi
    pushfl
    mov 8(%ebp), %edi
    mov (%edi), %al
    xor	%ecx, %ecx
	xor	%al, %al
	not	%ecx
	cld
    repnz scasb
	not	%ecx
	dec	%ecx
    mov %ecx, %edx
    mov 8(%ebp), %ecx
    mov $4, %eax
    mov $1, %ebx
    int $0x80
    popfl
    pop %edi
    pop %edx
    pop %ecx
    pop %ebx
    pop %eax
    pop %ebp
    ret
