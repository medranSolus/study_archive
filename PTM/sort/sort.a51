LJMP main
ORG 0100H
N EQU 0F5H
MEMSTART EQU 0F0H
main:
	MOV R0, #MEMSTART
	MOV @R0, #0C5H
	INC R0
	MOV @R0, #04H
	INC R0
	MOV @R0, #0F2H
	INC R0
	MOV @R0, #03H
	INC R0
	MOV @R0, #0F1H
	MOV R0, #MEMSTART
	loop1:
		CLR PSW.7
		MOV R1, #MEMSTART
		INC R1
		loop2:
			MOV A, @R1
			DEC R1
			SUBB A, @R1
			INC R1
			JNC loop2_end
				CLR PSW.7
				MOV A, @R1
				DEC R1
				MOV B, @R1
				MOV @R1, A
				INC R1
				MOV @R1, B
			loop2_end:
			INC R1
			MOV A, #N
			SUBB A, R0
			ADD A, #MEMSTART
			SUBB A, R1
			JC loop1_end
			JZ loop1_end
			LJMP loop2
		loop1_end:
		CLR PSW.7
		INC R0
		MOV	A, R0
		SUBB A, #N
		JC loop1
END main