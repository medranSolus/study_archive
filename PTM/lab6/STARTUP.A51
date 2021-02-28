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

; IN: R0 = Row
; OUT: A = Ascii code (0 == no input)
; LOCAL: R2
readRow:
	PUSH DPH
	PUSH DPL
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

; IN: R6 = ASCII code of first number to count
; GLOBAL: R7 = counter for one second
; LOCAL: A
countSeconds:
	MOV A, #CLEAR
	CALL writeControlWordLCD
	MOV A, R6
	CALL writeChar
	CALL counterStart
	countSeconds_loop:
		countSeconds_counterWait:
			MOV A, R7
			JNZ countSeconds_counterWait
		MOV R7, #0B7H
		MOV A, #CLEAR
		CALL writeControlWordLCD
		DEC R6
		MOV A, R6
		CALL writeChar
		CLR C
		SUBB A, #030H
		JNZ countSeconds_loop
	CALL counterStop
	RET
						
start:
	CALL init
	loop:
		CALL waitForInput
		MOV R6, A
		CALL countSeconds
		JMP loop   
	NOP
END start




			
