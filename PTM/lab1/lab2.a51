LJMP main
ORG 0100H
main:
 MOV R0, #010H
 MOV @R0, #012H
 MOV A, @R0
 NOP
 MOV DPTR, #08000H
 MOV A, #012H
 MOVX @DPTR, A
 INC DPTR
 MOVX @DPTR, A 
 MOV A, #0H
 MOVX A, @DPTR 
 NOP
 MOV R0, #07FH
 MOV A, #00H
 ORL A, R0
 MOV R0, #01H
 XRL A, R0
 MOV R0, #02H
 ANL A, R0
 NOP
 MOV R0, #011H ;01312H
 MOV R1, #011H
 MOV R2, #0F2H ;01514H
 MOV R3, #022H
 MOV A, R0
 ADD A, R2
 MOV R4, A
 MOV A, R1
 ADDC A, R3
 MOV R5, A
 NOP
 ADD A, #0FFH
 ADD A, #01H
 ADDC A, #01H
 MOV B, #04H
 MUL AB
 MOV B, #03H
 DIV AB
 SUBB A, B
END main