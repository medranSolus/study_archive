LJMP start
P5 EQU 0F8H
P7 EQU 0DBH
ORG 0100H
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
;OUT: Ascii in A
decode:
	MOV R2, A
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
			
start:
	CALL init
	loop:
		MOV A, #0FFH
		CALL setLed
		MOV R0, #07FH
		CALL getFromRow
		MOV R1, A
		CLR C
		SUBB A, R0
		JZ second
		MOV A, R1
		CALL decode
		CALL setLed
		second:
			MOV A, #0FFH
			CALL setLed
			MOV R0, #0BFH 
			CALL getFromRow
			MOV R1, A
			CLR C
			SUBB A, R0
			JZ third
			MOV A, R1
			CALL decode
			CALL setLed
		third:
			MOV A, #0FFH
			CALL setLed
			MOV R0, #0DFH 
			CALL getFromRow
			MOV R1, A
			CLR C
			SUBB A, R0
			JZ fourth
			MOV A, R1
			CALL decode
			CALL setLed
		fourth:
			MOV A, #0FFH
			CALL setLed
			MOV R0, #0EFH 
			CALL getFromRow
			MOV R1, A
			CLR C
			SUBB A, R0
			JZ loop
			MOV A, R1
			CALL decode
			CALL setLed
		JMP loop
END start