BINDIR := bin/
SRCDIR := src/
CFLAGS := -std=c11 -Wall -ggdb -lpthread
SRC = $(patsubst %.c, $(BINDIR)%, $(shell find $(SRCDIR) -type f -name "*.c" -exec basename {} \;))

.PHONY: all
all: mkbin $(SRC)

.PHONY: run
run:
	@$(BINDIR)philo_ipc 0&
	@$(BINDIR)philo_ipc 1&
	@$(BINDIR)philo_ipc 2&
	@$(BINDIR)philo_ipc 3&
	@$(BINDIR)philo_ipc 4&

.PHONY: mkbin
mkbin:
	@mkdir -p $(BINDIR)

$(BINDIR)%: $(SRCDIR)%.c
	@gcc $(CFLAGS) $< -o $@

.PHONY: clean
clean:
	@rm -f $(BINDIR)*