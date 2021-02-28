FILES = $(patsubst %.c, bin/%, $(shell find src/ -type f -name "*.c" -exec basename {} \;))

.PHONY: all
all: mkbin $(FILES)

.PHONY: mkbin
mkbin:
	@mkdir -p bin/

bin/%: src/%.c
	@gcc -std=c11 $< -o $@

.PHONY: clean
clean:
	@rm bin/*