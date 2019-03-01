#include <stdio.h>
#include <stdlib.h> /* for atof() */
#include <ctype.h>
#include <math.h>   /* for fmod(a, b) */

#define MAXOP 100       /* max size of operand or operator */
#define MAXVAL 100      /* maximum depth of val stack */
#define NUMBER '0'      /* signal that a number was found */
#define NEG_NUMBER '1'  /* signal that a negative number was found */

#define BUFSIZE 100

/* Exercise 4-4
 *
 * Add the commands to print the top elements of the stack without popping, to
 * duplicate it, and to swap the top two elements. Add a command to clear the stack.
 */

int getop(char []);
void push(double);
double pop(void);
int getch(void);
void ungetch(int);
int size(void);
double peek(void);
void duplicate(void);
void swap(void);
void clear(void);

char buf[BUFSIZE];  /* buffer for ungetch */
int bufp = 0;       /* next free position in buf */
int sp = 0;         /* next free stack position */
double val[MAXVAL]; /* value stack */
double* valp = val;

/* reverse Polish calculator */
int main(int argc, char* argv[])
{
    int type;
    double op2;
    char s[MAXOP];

    push(2.0);
    push(3.0);
    push(4.0);
    printf("peek(): %f\n", peek());
    pop();
    printf("duplicating %f ...\n", peek());
    duplicate();
    printf("peek(): %f\n", peek());
    pop();
    printf("swapping top two elements ...\n");
    swap();
    printf("peek(): %f\n", peek());
    pop();
    printf("peek(): %f\n", peek());
    pop();
    push(2.0);
    push(3.0);
    push(4.0);
    printf("stack size before clear(): %d\n", size());
    clear();
    printf("stack size after clear(): %d\n\nStarting reverse Polish calculator ...\n", size());

    while ((type = getop(s)) != EOF)
    {
        switch (type)
        {
            case NUMBER:
                push(atof(s));
                break;
            case NEG_NUMBER:
                push(-atof(s));
                break;
            case '+':
                push(pop() + pop());
                break;
            case '*':
                push(pop() * pop());
                break;
            case '-':
                op2 = pop();
                push(pop() - op2);
                break;
            case '/':
                op2 = pop();
                if (op2 != 0.0)
                    push(pop() / op2);
                else
                    printf("error: zero divisor\n");
                break;
            case '%':
                op2 = pop();
                push(fmod(pop(), op2));
                break;
            case '\n':
                printf("\t%.8g\n", pop());
                break;
            default:
                printf("error: unknown command %s\n", s);
                break;
        }
    }
    return 0;
}

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

/* getop: get next character or numeric operand */
int getop(char s[])
{
    int i, c, temp;
    while ((s[0] = c = getch()) == ' ' || c == '\t')
        ;
    s[1] = '\0';
    if (!isdigit(c) && c != '.')
    {
        if (c == '\n')
            return c;
        if (isdigit(temp = getch()))
        {
            s[0] = temp;
            i = 0;/* collect integer part */
            while (isdigit(s[++i] = c = getch()))
                ;
            if (c == '.') /* collect fraction part */
                while (isdigit(s[++i] = c = getch()))
                    ;
            s[i] = '\0';
            return NEG_NUMBER;
        }
        else
        {
            ungetch(temp);
            return c; /* not a number */
        }
    }
    i = 0;
    if (isdigit(c)) /* collect integer part */
        while (isdigit(s[++i] = c = getch()))
            ;
    if (c == '.') /* collect fraction part */
        while (isdigit(s[++i] = c = getch()))
            ;
    s[i] = '\0';
    if (c != EOF)
        ungetch(c);
    return NUMBER;
}

/* get a (possibly pushed-back) character */
int getch(void)
{
    return (bufp > 0) ? buf[--bufp] : getchar();
}

/* push character back on input */
void ungetch(int c)
{
    if (bufp >= BUFSIZE)
        printf("ungetch: too many characters\n");
    else
        buf[bufp++] = c;
}
