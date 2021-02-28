LJMP main
LCDstatus EQU 0FF2EH
LCDcontrol EQU 0FF2CH
LCDdataWR EQU 0FF2DH
LCDdataRD EQU 0FF2FH

#define HOME 0x80
#define INITDISP 0x38
#define HOM2 0xc0
#define LCDON 0x0e
#define CLEAR 0x01

ORG 0100H
LCDctrlWR MACRO x
	LOCAL loop
	loop:
		MOV DPTR, #LCDstatus
		MOVX A, @DPTR
		JB ACC.7, loop
	MOV DPTR, #LCDcontrol
	MOV A, x
	MOVX @DPTR, A
ENDM
LCDcharWR MACRO
	LOCAL loop1
	PUSH ACC
	loop1:
		MOV DPTR, #LCDstatus
		MOVX A, @DPTR
		JB ACC.7, loop1	
	MOV DPTR, #LCDdataWR
	POP ACC
	MOVX @DPTR, A
ENDM
init_LCD MACRO
	LCDctrlWR #INITDISP
	LCDctrlWR #CLEAR
	LCDctrlWR #LCDON
ENDM

text1: DB "Numa numa jej", 00
text2: DB "Lec Adam lec", 00
text3: DB "Hello World!", 00
text4: DB "Piateczek", 00
textD: DB "Ni ma nic", 00

writeString:
	CLR A
	MOVC A, @A+DPTR
	JZ exit
	PUSH DPH
	PUSH DPL
	CALL writeChar
	POP	DPL
	POP	DPH
	INC DPTR
	SJMP writeString
	exit:
	RET

writeChar: 
	LCDcharWR
	RET
			
main:
	init_LCD
	PUSH 00
	loop:
		CLR A
		MOV A, P3
		JNB ACC.5, case1
		JNB ACC.4, case2
		JNB ACC.3, case3
		JNB ACC.2, case4
		JMP default
		case1:
			MOV DPTR, #text1
			MOV A, #01H
			JMP next
		case2:
			MOV DPTR, #text2
			MOV A, #02H
			JMP next
		case3:
			MOV DPTR, #text3
			MOV A, #03H
			JMP next
		case4:
			MOV DPTR, #text4
			MOV A, #04H
			JMP next
		default:
			MOV DPTR, #textD
			MOV A, #05H
			JMP next
		next:
		CLR C
		POP B
		MOV R0, A
		SUBB A, B
		JZ next_loop
		MOV B, R0
		PUSH B
		PUSH DPH
		PUSH DPL
		LCDctrlWR #CLEAR
		POP DPL
		POP DPH
		CALL writeString
		next_loop:
		JMP loop
END main