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

;IN: Ascii code in A
LCDcharWR MACRO
	LOCAL loop
	PUSH ACC
	loop:
		MOV DPTR, #LCDstatus
		MOVX A, @DPTR
		JB ACC.7, loop	
	MOV DPTR, #LCDdataWR
	POP ACC
	MOVX @DPTR, A
ENDM

init_LCD MACRO
	LCDctrlWR #INITDISP
	LCDctrlWR #CLEAR
	LCDctrlWR #LCDON
ENDM

;IN: Asci code in A
writeChar:
	PUSH DPH
	PUSH DPL 
	LCDcharWR
	POP DPL
	POP DPH
	RET

wait:
	MOV R1, #0FFH
	wait_loop: 
		MOV R0, #0A0H
		DJNZ R0, $
		DJNZ R1, wait_loop
	RET
		
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
	RET

;IN: Code in A
setLed:
	MOV P1, A
	RET

;IN: Row in R0
;OUT: Code in A
getFromRow:
	MOV A, R0 
	MOV P5, A
	MOV A, P7
	ANL A, R0
	RET

;IN: Code in A
;IN: Row in R0
;OUT: Ascii in A (0==no input)
decode:
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
	RET

;OUT: A = ascii code (0==no input)
getCode:
	MOV R0, #07FH
	CALL getFromRow
	CALL decode
	CLR C
	SUBB A, #0H
	JNZ getCode_exit
	MOV R0, #0BFH 
	CALL getFromRow
	CALL decode
	CLR C
	SUBB A, #0H
	JNZ getCode_exit
	MOV R0, #0DFH  
	CALL getFromRow
	CALL decode
	CLR C
	SUBB A, #0H
	JNZ getCode_exit
	MOV R0, #0EFH  
	CALL getFromRow
	CALL decode
	getCode_exit:
	RET

;OUT: ascii code in A
waitForInput:
	CLR A
	waitForInput_loop:
		CALL getCode
		CLR C
		SUBB A, #0H
		JZ waitForInput_loop
	RET

;IN: B number of chars
countLine:
	PUSH ACC
	PUSH DPH
	PUSH DPL
	DEC B
	MOV A, B
	CLR C
	SUBB A, #0FH
	JZ countLine_nextLine
	MOV A, B
	ADD A, #01H
	JNZ countLine_exit
	LCDctrlWR #CLEAR
	MOV B, #020H
	JMP countLine_exit
	countLine_nextLine:
		LCDctrlWR #HOM2
	countLine_exit:
	POP DPL
	POP DPH
	POP ACC	 
	RET
							
start:
	init_LCD
	CALL init
	MOV B, #020H
	loop:
		CALL waitForInput
		CALL countLine
		CALL writeChar
		CALL wait
		CALL wait
		JMP loop
	NOP
END start