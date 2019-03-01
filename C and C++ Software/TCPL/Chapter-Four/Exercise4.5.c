#include <stdio.h>
#include <stdlib.h> /* for atof() */
#include <ctype.h>
#include <math.h>   /* for fmod(a, b) */
#include <string.h> /* for strcmp */

#define MAXOP 100       /* max size of operand or operator */
#define MAXVAL 100      /* maximum depth of val stack */
#define NUMBER '0'      /* signal that a number was found */
#define NEG_NUMBER '1'  /* signal that a negative number was found */
#define MATH_WORD '2'   /* signal that a math operator is found */

#define BUFSIZE 100

/* Exercise 4-5
 *
 * Add access to library functions like sin, exp, and pow. See <math.h> in
 * Appendix B, Section 4.
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
char math_word_buf[5];   /* longest math operator can be sqrt, plus space for null terminator. */

/* reverse Polish calculator */
int main(int argc, char* argv[])
{
    int type;
    double power, op2;
    char s[MAXOP];

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
            case MATH_WORD:
                if (strcmp(math_word_buf, "pow") == 0)
                {
                    power = pop();
                    push(pow(pop(), power));
                }
                else if (strcmp(math_word_buf, "sin") == 0)
                {
                    push(sin(pop()));
                }
                else if (strcmp(math_word_buf, "cos") == 0)
                {
                    push(cos(pop()));
                }
                else if (strcmp(math_word_buf, "exp") == 0)
                {
                    push(exp(pop()));
                }
                else if (strcmp(math_word_buf, "sqrt") == 0)
                {
                    push(sqrt(pop()));
                }
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
            if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                ungetch(temp);
                return c; /* not a number */
            }
            math_word_buf[0] = c;
            math_word_buf[1] = temp;
            int l = 2;
            while (( c = getch()) != ' ' && c != '\t')
            {
                if (c == '\n')
                {
                    ungetch(c);
                    break;
                }
                math_word_buf[l++] = c;
            }
            math_word_buf[l] = '\0';
            l = 0;
            while (math_word_buf[l])
            {
                math_word_buf[l] = tolower(math_word_buf[l]);
                l++;
            }
            if (strcmp(math_word_buf, "sin") == 0 || strcmp(math_word_buf, "cos") == 0 ||
                strcmp(math_word_buf, "exp") == 0 || strcmp(math_word_buf, "pow") == 0 ||
                strcmp(math_word_buf, "sqrt") == 0)
            {
                return MATH_WORD;
            }
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
