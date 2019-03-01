all: scheduler

scheduler: cpu.o child1.o child2.o

cpu.o: CPU.cc
	g++ -g CPU.cc -o cpu -Wall

child1.o: child1.cc
	g++ -g child1.cc -o child1 -Wall

child2.o: child2.cc
	g++ -g child2.cc -o child2 -Wall

clean:
	rm -rf *0 scheduler
