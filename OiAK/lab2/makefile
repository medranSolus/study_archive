AS = as --gstabs --32
LD = ld -m elf_i386

.PHONY: lab2

lab2: main.o 
	$(LD) *.o -o lab2

main.o: main.s
	$(AS) main.s -o main.o

clean:
	rm -f *.o lab2