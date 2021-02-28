BINDIR := bin/
SRCDIR := src/
CFLAGS := -std=c11 -Wall -ggdb -lpthread
SRC = $(patsubst %.c, $(BINDIR)%, $(shell find $(SRCDIR) -type f -name "*.c" -exec basename {} \;))

.PHONY: all
all: mkbin $(SRC)

.PHONY: mkbin
mkbin:
	@mkdir -p $(BINDIR)

$(BINDIR)%: $(SRCDIR)%.c
	@gcc $(CFLAGS) $< -o $@

.PHONY: clean
clean:
	@rm -f $(BINDIR)*