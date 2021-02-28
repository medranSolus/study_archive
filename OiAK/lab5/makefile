AS = as --gstabs --32
CC = gcc -c -m32
OUT = ./obj/

.PHONY: lab5

lab5: determinant.o main.o 
	gcc -m32 $(OUT)*.o -o lab5

main.o: main.c
	$(CC) main.c -o $(OUT)main.o

determinant.o: determinant.s
	$(AS) determinant.s -o $(OUT)determinant.o

clean:
	rm -f $(OUT)*.o lab5
