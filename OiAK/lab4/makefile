AS = as --gstabs --32
CC = gcc -c -m32
SSEDIR = ./SSEoperations/
VECDIR = ./vectorOperations/
OUT = ./obj/

.PHONY: lab4

lab4: fpuPrecision.o checkSSE4_1.o checkSSE2.o addSSE.o subSSE.o mulSSE.o divSSE.o addVec.o subVec.o mulVec.o divVec.o main.o 
	gcc -m32 $(OUT)*.o -o lab4

main.o: main.c
	$(CC) main.c -o $(OUT)main.o

fpuPrecision.o: fpuPrecision.s
	$(AS) fpuPrecision.s -o $(OUT)fpuPrecision.o

checkSSE4_1.o: $(SSEDIR)checkSSE4_1.s
	$(AS) $(SSEDIR)checkSSE4_1.s -o $(OUT)checkSSE4_1.o

checkSSE2.o: $(SSEDIR)checkSSE2.s
	$(AS) $(SSEDIR)checkSSE2.s -o $(OUT)checkSSE2.o

addSSE.o: $(SSEDIR)addSSE.s
	$(AS) $(SSEDIR)addSSE.s -o $(OUT)addSSE.o

subSSE.o: $(SSEDIR)subSSE.s
	$(AS) $(SSEDIR)subSSE.s -o $(OUT)subSSE.o

mulSSE.o: $(SSEDIR)mulSSE.s
	$(AS) $(SSEDIR)mulSSE.s -o $(OUT)mulSSE.o

divSSE.o: $(SSEDIR)divSSE.s
	$(AS) $(SSEDIR)divSSE.s -o $(OUT)divSSE.o

addVec.o: $(VECDIR)addVec.s
	$(AS) $(VECDIR)addVec.s -o $(OUT)addVec.o

subVec.o: $(VECDIR)subVec.s
	$(AS) $(VECDIR)subVec.s -o $(OUT)subVec.o

mulVec.o: $(VECDIR)mulVec.s
	$(AS) $(VECDIR)mulVec.s -o $(OUT)mulVec.o

divVec.o: $(VECDIR)divVec.s
	$(AS) $(VECDIR)divVec.s -o $(OUT)divVec.o

clean:
	rm -f $(OUT)*.o lab4
