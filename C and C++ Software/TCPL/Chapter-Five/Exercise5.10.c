#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>

#define MAXOP 100
#define NUMBER '0'
#define MAXVAL 100

/* Exercise 5-10.
 * Write the program expr, which evaluates a reverse Polish expression from the
 * command line, where each operator or operand is a separate argument. For example,
 *
 * expr 2 3 4 + *   => (Note: '*' is a wildcard and needs to be escaped.... so expr 2 3 4 + \* evaluates to 14)
 * evaluates 2 * (3+4).
 */

int getop(char[], char*[]);
double pop();
void push(double);

int sp = 0;
double val[MAXVAL];

int main(int argc, char *argv[])
{
    int type;
    double op2;
    char s[MAXOP];

    while ((type = getop(s, ++argv)) != EOF)
    {
        switch (type)
        {
            case NUMBER:
                push(atof(s));
                break;
            case '+':
                push(pop() + pop());
                break;
            case '-':
                op2 = pop();
                push(pop() - op2);
                break;
            case '*':
                push(pop() * pop());
                break;
            case '/':
                op2 = pop();
                if (op2 != 0.0)
                {
                    push(pop() / op2);
                }
                else
                {
                    printf("Error: division by 0\n");
                }
                break;
            default:
                printf("Error: unknown command\n");
                break;
        }
    }
    printf("Result = %.8g\n", pop());
    return 0;
}

void push(double f)
{
    if (sp < MAXVAL)
    {
        val[sp++] = f;
    }
    else
    {
        printf("Error: stack is full, cannot push %g\n", f);
    }
}

double pop()
{
    if (sp > 0)
    {
        return val[--sp];
    }
    else
    {
        printf("Error: stack is empty\n");
        return 0.0;
    }
}

int getop(char s[], char *argv[])
{
    int i;
    char *p;

    if (*argv == NULL)
    {
        return EOF;
    }
    if (**argv == '+' || **argv == '-' || **argv == '*' || **argv == '/')
    {
        return **argv;
    }
    p = *argv;
    s[0] = *p;
    s[1] = '\0';

    if ((!isdigit(*p) && *p != '.'))
    {
        printf("c = %c\n", *p);
        return *p;
    }
    i = 0;
    if (isdigit(*p))
    {
        while(isdigit(s[++i] = *++p))
            ;
    }
    if (*p == '.')
    {
        while(isdigit(s[++i] = *++p))
            ;
    }
    s[i] = '\0';
    return NUMBER;
}
