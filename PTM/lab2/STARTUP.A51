LJMP start
P6 EQU 0FAH
BUZZ_ON EQU 00010000b
BUZZ_OFF EQU 11101111b
LED_ON EQU 00000001b  
LED_OFF EQU 11111110b
RELAY_ON EQU 01000000b 
RELAY_OFF EQU 10111111b
ORG 050H
;Funkcja dodajaca opóznienie
wait:
	MOV R1, #0FFH
	wait_loop: 
		MOV R0, #0FFH
		DJNZ R0, $
		DJNZ R1, wait_loop
	ret
;Wysoki dzwiek brzeczyka
buzzFast:
	MOV R1, #0FFH
	buzzFast_loop: 
		MOV R0, #0FFH
	buzz_change:
		CPL P3.2
		DJNZ R0, buzz_change
		DJNZ R1, buzzFast_loop
	ret
;Niski dzwiek brzeczyka
buzzSlow:
	MOV R1, #0FFH
	buzzSlow_loop: 
		MOV R0, #0FFH
		DJNZ R0, $ 
		CPL P3.2
		DJNZ R1, buzzSlow_loop
	ret		
ORG 0100H
start:
	;Slowo odpowiedzialne za zapalenie diod LED
	MOV R4, #0AAH
	MOV A, R4
	MOV R2, #0FFH
	loop:
		;Zaswiecenie zewnetrznych diod LED
		MOV P1, A
		ACALL buzzFast 
		ACALL wait
		;Wlaczenie buzzera
		MOV A, P6
		ORL A, #BUZZ_ON
		MOV P6, A
		ACALL buzzSlow 
		ACALL wait
		;Wylaczenie buzzera
		MOV A, P6
		ANL A, #BUZZ_OFF
		MOV P6, A
		;Wlaczenie czerwonej diody LED
		MOV A, P6
		ORL A, #LED_ON
		MOV P6, A
		ACALL buzzFast 
		ACALL wait
		;Wylaczenie czerwonej diody LED
		MOV A, P6
		ANL A, #LED_OFF
		MOV P6, A
		;Zmiana pozycji przelacznika
		MOV A, P6
		ORL A, #RELAY_ON
		MOV P6, A
		ACALL buzzSlow 
		ACALL wait
		;Zmiana pozycji przelacznika
		MOV A, P6
		ANL A, #RELAY_OFF
		MOV P6, A
		MOV A, R4
		;Zmiana aktywnych diod LED
		RR A
		MOV R4, A
		DJNZ R2, loop
	MOV A, #0FFH
	MOV P1, A
	NOP
END start