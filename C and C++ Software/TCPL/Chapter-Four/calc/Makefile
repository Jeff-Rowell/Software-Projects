FILES = calc

CC = gcc
CFLAGS = -g

all: $(FILES)

calc: main.o Exercise4.11.o getch.o stack.o
	$(CC) -o calc main.o Exercise4.11.o getch.o stack.o
	make clean

main.o: main.c calc.h

Exercise4.11.o: Exercise4.11.c calc.h

getch.o: getch.c calc.h

stack.o: stack.c calc.h

clean:
	rm -f *.o
