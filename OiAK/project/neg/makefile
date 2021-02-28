AS=as
CC=g++
SDIR=./src/
CFLAGS=-std=c++11 -c -I src/headers -g
LFLAGS=-lrt -std=c++11
OUT=-o ./obj/
_OBJDIR=./obj/
_OBJSEQ=checkSSE2.obj BMP.obj mainSeq.obj
OBJSEQ=$(patsubst %,$(_OBJDIR)%,$(_OBJSEQ))
_OBJPAR=checkSSE2.obj BMP.obj utils.obj mainPar.obj
OBJPAR=$(patsubst %,$(_OBJDIR)%,$(_OBJPAR))
_OBJROW=checkSSE2.obj BMP.obj utils.obj row.obj
OBJROW=$(patsubst %,$(_OBJDIR)%,$(_OBJROW))
_OBJROWLOAD=checkSSE2.obj BMP.obj utils.obj rowLoad.obj
OBJROWLOAD=$(patsubst %,$(_OBJDIR)%,$(_OBJROWLOAD))
_OBJGEN=checkSSE2.obj BMP.obj mainTestCol.obj
OBJGEN=$(patsubst %,$(_OBJDIR)%,$(_OBJGEN))

.PHONY: all

all: structures _negSeq rowLoad row _negPar _testCol testGen

structures: checkSSE2.o BMP.o utils.o

testGen:
	$(CC) $(LFLAGS) $(SDIR)testGenerator/main.cpp -o bin/testGen

testCol: BMP.o mainTestCol.o
	$(CC) $(LFLAGS) $(OBJGEN) -o bin/testCol

_testCol: mainTestCol.o
	$(CC) $(LFLAGS) $(OBJGEN) -o bin/testCol

mainTestCol.o:
	$(CC) $(CFLAGS) $(SDIR)testGenerator/main.cpp $(OUT)mainTestCol.obj

negPar: structures rowLoad row mainPar.o
	$(CC) $(LFLAGS) $(OBJPAR) -o bin/negPar

_negPar: mainPar.o
	$(CC) $(LFLAGS) $(OBJPAR) -o bin/negPar

row: row.o
	$(CC) $(LFLAGS) $(OBJROW) -o bin/negParRow

row.o:
	$(CC) $(CFLAGS) $(SDIR)parallel/subNegation/row.cpp $(OUT)row.obj

rowLoad: rowLoad.o
	$(CC) $(LFLAGS) $(OBJROWLOAD) -o bin/negParRowLoad

rowLoad.o:
	$(CC) $(CFLAGS) $(SDIR)parallel/subNegation/rowLoad.cpp $(OUT)rowLoad.obj

mainPar.o:
	$(CC) $(CFLAGS) $(SDIR)parallel/main.cpp $(OUT)mainPar.obj

utils.o:
	$(CC) $(CFLAGS) $(SDIR)utils.cpp $(OUT)utils.obj

negSeq: structures mainSeq.o
	$(CC) $(LFLAGS) $(OBJSEQ) -o bin/negSeq

_negSeq: mainSeq.o
	$(CC) $(LFLAGS) $(OBJSEQ) -o bin/negSeq

mainSeq.o:
	$(CC) $(CFLAGS) $(SDIR)sequential/main.cpp $(OUT)mainSeq.obj

BMP.o:
	$(CC) $(CFLAGS) $(SDIR)BMP.cpp $(OUT)BMP.obj

checkSSE2.o:
	$(AS) $(SDIR)checkSSE2.s $(OUT)checkSSE2.obj

clean:
	rm -f ./obj/*.obj ./bin/* _imageTime.txt NEG_*.bmp