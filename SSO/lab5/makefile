BINDIR := bin/
SRCDIR := src/
CFLAGS := -std=c11 -Wall -ggdb
SRC = $(patsubst %.c, $(BINDIR)%, $(shell find $(SRCDIR) -type f -name "*.c" -exec basename {} \;))

.PHONY: all
all: mkbin $(SRC)

.PHONY: run
run:
	@$(BINDIR)philo 0&
	@$(BINDIR)philo 1&
	@$(BINDIR)philo 2&
	@$(BINDIR)philo 3&
	@$(BINDIR)philo 4&

.PHONY: mkbin
mkbin:
	@mkdir -p $(BINDIR)

$(BINDIR)%: $(SRCDIR)%.c
	@gcc $(CFLAGS) $< -o $@

.PHONY: clean
clean:
	@rm -f $(BINDIR)*