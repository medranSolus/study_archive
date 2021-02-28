.macro calcVecQuick shuffleA1B2, shuffleA2B1, shuffleB2_B1, regA, regB, regOut
    movaps \regA, \regOut
    shufps \shuffleA1B2, \regOut, \regOut
    shufps \shuffleA2B1, \regA, \regA
    shufps \shuffleA2B1, \regB, \regB
    mulps \regB, \regOut
    shufps \shuffleB2_B1, \regB, \regB
    mulps \regB, \regA
    subps \regA, \regOut
.endm

.macro calcVec shuffleA1B2, shuffleA2B1, regA, regB, regBcopy, regOut
    movaps \regB, \regBcopy
    movaps \regA, \regOut
    shufps \shuffleA1B2, \regOut, \regOut
    shufps \shuffleA2B1, \regA, \regA
    shufps \shuffleA1B2, \regBcopy, \regBcopy
    shufps \shuffleA2B1, \regB, \regB
    mulps \regB, \regOut
    mulps \regBcopy, \regA
    subps \regA, \regOut
.endm

.macro calcVecLong offset1, offset2, regPointer1, regPointer2, shuffleA1B2, shuffleA2B1, regA1_Out, regA2, regB1, regB2, regTemp
    movaps \offset1(\regPointer2), \regA1_Out
    movaps \regA1_Out, \regA2
    movaps \offset1(\regPointer1), \regTemp
    shufps \shuffleA1B2, \regTemp, \regA1_Out
    shufps \shuffleA2B1, \regTemp, \regA2
    movaps \offset2(\regPointer2), \regB1
    movaps \regB1, \regB2
    movaps \offset2(\regPointer1), \regTemp
    shufps \shuffleA2B1, \regTemp, \regB1
    shufps \shuffleA1B2, \regTemp, \regB2
    mulps \regB1, \regA1_Out
    mulps \regB2, \regA2
    subps \regA2, \regA1_Out  
.endm
