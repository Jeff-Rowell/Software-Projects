#include <stdio.h>
#include "calc.h"

#define MAXVAL 100      /* maximum depth of val stack */
int sp = 0;         /* next free stack position */
double val[MAXVAL]; /* value stack */
double* valp = val;

/* push: push f onto value stack */
void push(double f)
{
    if (sp < MAXVAL)
    {
        sp++;
        *(valp++) = f;
    }
    else
        printf("error: stack full, can't push %g\n", f);
}

/* pop: pop and return top value from stack */
double pop(void)
{
    if (sp > 0)
    {
        sp--;
        return *(--valp);
    }
    else
    {
        printf("error: stack empty\n");
        return 0.0;
    }
}

/* peek: get the top element of the stack without removing it. */
double peek(void)
{
    return *(valp - 1);
}

/* duplicate: duplicates the top element on the stack. */
void duplicate(void)
{
    push(peek());
}

/* swap: swaps the top two elements on the stack. */
void swap(void)
{
    double item1 = pop(), item2 = pop();
    push(item1);
    push(item2);
}

/* clear: removes all elements from the stack. */
void clear(void)
{
    valp = val;
    sp = 0;
}

/* size: get the size of the stack */
int size(void)
{
    return valp - val;
}