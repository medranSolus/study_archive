LJMP start
P5 EQU 0F8H
P7 EQU 0DBH
LCDstatus EQU 0FF2EH
LCDcontrol EQU 0FF2CH
LCDdataWR EQU 0FF2DH
LCDdataRD EQU 0FF2FH

#define HOME 0x80
#define INITDISP 0x38
#define HOM2 0xc0
#define LCDON 0x0e
#define CLEAR 0x01

ORG 0BH
LJMP intT0

ORG 050H
textEnterValue: DB "Enter value: ", 00
textReset: DB "RESET", 00
textStop: DB "STOP", 00
textPause: DB "PAUSE", 00

ORG 0100H
; GLOBAL: R7 = counter for one second
intT0:
	MOV TH0, #0EBH
	MOV TL0, #0C0H
	DEC R7
	RETI

; LOCAL: DPTR, A	
init:
	MOV DPTR, #08000H
	MOV A, #031H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #032H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #033H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #041H
	MOVX @DPTR, A
	INC DPTR	  
	MOV A, #034H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #035H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #036H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #042H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #037H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #038H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #039H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #043H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #02AH
	MOVX @DPTR, A
	INC DPTR
	MOV A, #030H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #023H
	MOVX @DPTR, A
	INC DPTR
	MOV A, #044H
	MOVX @DPTR, A
	MOV A, #INITDISP
	CALL writeControlWordLCD
	MOV A, #CLEAR
	CALL writeControlWordLCD
	MOV A, #LCDON
	CALL writeControlWordLCD
	RET

; LOCAL: R1, R0
wait:
	MOV R1, #0FFH
	wait_loop: 
		MOV R0, #0A0H
		DJNZ R0, $
		DJNZ R1, wait_loop
	RET

; IN: A = control word
writeControlWordLCD:
	PUSH DPH
	PUSH DPL
	PUSH ACC
	MOV DPTR, #LCDstatus
	writeControlWordLCD_loop:
		MOVX A, @DPTR
		JB ACC.7, writeControlWordLCD_loop
	MOV DPTR, #LCDcontrol
	POP ACC
	MOVX @DPTR, A
	POP DPL
	POP DPH
	RET

; IN: A = Asci code
writeChar:
	PUSH DPH
	PUSH DPL
	PUSH ACC
	MOV DPTR, #LCDstatus
	writeChar_loop:
		MOVX A, @DPTR
		JB ACC.7, writeChar_loop	
	MOV DPTR, #LCDdataWR
	POP ACC
	MOVX @DPTR, A
	POP DPL
	POP DPH
	RET

; IN: DPTR = string adress in code memory
printString:
	PUSH ACC
	printString_loop:
		CLR A
		MOVC A, @A+DPTR
		JZ exit
		CALL writeChar
		INC DPTR
		SJMP printString_loop
	exit:
	POP ACC
	RET

; IN: R0 = Row
; OUT: A = Ascii code (0 == no input)
; LOCAL: R2
readRow:
	PUSH DPH
	PUSH DPL
	MOV DPH, #080H
	MOV A, R0 
	MOV P5, A
	MOV A, P7
	ANL A, R0
	MOV R2, A
	CLR C
	SUBB A, R0
	JZ decode_exit
	MOV A, R0
	ANL A, #80H
	JZ decode_first
	MOV A, R0
	ANL A, #40H
	JZ decode_second
	MOV A, R0
	ANL A, #20H
	JZ decode_third
	MOV A, R0
	ANL A, #10H
	JZ decode_fourth
	decode_first:
		MOV DPL, #0H
		JMP decode_next
	decode_second:
		MOV DPL, #04H
		JMP decode_next
	decode_third:
		MOV DPL, #08H
		JMP decode_next
	decode_fourth:
		MOV DPL, #0CH
	decode_next:
	MOV A, R2
	ANL A, #0FH
	XRL A, #0FH
	JZ decode_exit
	MOV A, R2
	ANL A, #08H
	JZ decode_get
	MOV A, R2
	ANL A, #04H
	JZ decode_secondCode
	MOV A, R2
	ANL A, #02H
	JZ decode_thirdCode
	MOV A, R2
	ANL A, #01H
	JZ decode_fourthCode
	decode_secondCode:
		MOV A, DPL
		ADD A, #01H
		MOV DPL, A
		JMP decode_get
	decode_thirdCode:
		MOV A, DPL
		ADD A, #02H
		MOV DPL, A
		JMP decode_get
	decode_fourthCode:
		MOV A, DPL
		ADD A, #03H
		MOV DPL, A
	decode_get:
		MOVX A, @DPTR
	decode_exit:
	POP DPL
	POP DPH
	RET

; OUT: A = Ascii code (0 == no input)
; LOCAL: R0
getCode:
	MOV R0, #07FH
	CALL readRow
	CLR C
	SUBB A, #0H
	JNZ getCode_exit
	MOV R0, #0BFH 
	CALL readRow
	CLR C
	SUBB A, #0H
	JNZ getCode_exit
	MOV R0, #0DFH  
	CALL readRow
	CLR C
	SUBB A, #0H
	JNZ getCode_exit
	MOV R0, #0EFH  
	CALL readRow
	getCode_exit:
	RET

; OUT: A = Ascii code
waitForInput:
	CLR A
	waitForInput_loop:
		CALL getCode
		CLR C
		SUBB A, #0H
		JZ waitForInput_loop
	RET

; OUT: R6 = number
; LOCAL: B
getNumber:
	CALL waitForInput
	CALL writeChar
	CALL wait
	CALL wait
	ANL A, #0FH
	MOV R6, A
	CALL waitForInput
	MOV B, A
	CLR C
	SUBB A, #41H
	JZ getNumber_exit
	MOV A, B
	CALL writeChar
	ANL A, #0FH
	MOV B, A
	MOV A, R6
	MOV R6, B
	MOV B, #10
	MUL AB
	ADD A, R6
	MOV R6, A
	getNumber_exit:
	RET

; GLOBAL: R7 = counter for one second
counterStart:
	MOV TMOD, #01H
	MOV R7, #0B7H
	MOV IE, #82H
	MOV TH0, #0EBH
	MOV TL0, #0C0H
	SETB TR0
	RET	

counterStop:
	MOV IE, #0H
	CLR TR0
	RET

; IN: R6 = number
; LOCAL: B
displayNumber:
	PUSH ACC
	MOV A, R6
	MOV B, #10
	DIV AB
	CLR C
	SUBB A, #00
	JZ displayNumber_writeNext
	ADD A, #30H
	CALL writeChar
	displayNumber_writeNext:
	MOV A, B
	ADD A, #30H
	CALL writeChar
	POP ACC
	RET

; IN: R6 = ASCII code of first number to count
; OUT: A = 0 - counting stopped | 1 - reset counting
; GLOBAL: R7 = counter for one second
; LOCAL: R5
countSeconds:
	MOV A, R6
	MOV R5, A
	MOV A, #CLEAR
	CALL writeControlWordLCD
	CALL displayNumber
	CALL counterStart
	countSeconds_loop:
		countSeconds_counterWait:
			CALL getCode
			CLR C
			SUBB A, #042H
			JZ countSeconds_counterWait_reset
			SUBB A, #01H
			JZ countSeconds_counterWait_pause
			SUBB A, #01H
			JNZ countSeconds_counterWait_count
			MOV A, #CLEAR
			CALL writeControlWordLCD
			MOV DPTR, #textStop
			CALL printString
			CALL wait
			CALL wait
			MOV A, #00
			JMP countSeconds_exit
			countSeconds_counterWait_reset:
			MOV A, #CLEAR
			CALL writeControlWordLCD
			MOV DPTR, #textReset
			CALL printString
			MOV A, R5
			MOV R6, A
			MOV A, #01H
			CALL wait
			CALL wait
			JMP countSeconds_exit
			countSeconds_counterWait_pause:
			CLR TR0
			MOV A, #CLEAR
			CALL writeControlWordLCD
			MOV DPTR, #textPause
			CALL printString
			countSeconds_counterWait_pause_wait:
				CALL waitForInput
				CLR C
				SUBB A, #043H
				JNZ countSeconds_counterWait_pause_wait
			MOV A, #CLEAR
			CALL writeControlWordLCD
			CALL displayNumber
			SETB TR0
			countSeconds_counterWait_count:
			MOV A, R7
			JNZ countSeconds_counterWait
		MOV R7, #0B7H
		MOV A, #CLEAR
		CALL writeControlWordLCD
		DEC R6
		CALL displayNumber
		MOV A, R6
		CLR C
		SUBB A, #00
		JNZ countSeconds_loop
	CLR A
	countSeconds_exit:
	CALL counterStop
	RET
						
start:
	CALL init
	MOV DPTR, #textEnterValue
	CALL printString
	loop:
		CALL getNumber
		loop_countAgain:
			CALL countSeconds
			CLR C
			SUBB A, #01H
			JZ loop_countAgain
		MOV A, #CLEAR
		CALL writeControlWordLCD
		MOV DPTR, #textEnterValue
		CALL printString
		JMP loop
	NOP
END start