all: hello writeText compareText

all: hello writeText compareText

hello: hello.o
	ld hello.o -o hello

hello.o: hello.s
	as hello.s -o hello.o

writeText: writeText.o
	ld writeText.o -o writeText

writeText.o: writeText.s
	as writeText.s -o writeText.o

compareText: compareText.o
	ld compareText.o -o compareText

compareText.o: compareText.s
	as compareText.s -o compareText.o

clean:
	rm -f *.o compareText writeText hello

.PHONY: all