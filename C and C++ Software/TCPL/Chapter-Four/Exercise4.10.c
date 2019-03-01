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
#define VARIABLE '3'    /* signal that a variable is found */
#define MAX_LINE 500    /* maximum length of a line */

#define NUM_VARIABLES 26
#define BUFSIZE 100

/* Exercise 4-10.
 *
 * An alternate organization uses getline to read an entire input line; this makes
 * getch and ungetch unnecessary. Revise the calculator to use this approach.
 */

int getop(char []);
void push(double);
double pop(void);
int size(void);
double peek(void);
void duplicate(void);
void swap(void);
void clear(void);
int is_variable(char c);
double resolve_variable(char);
void get_line();

char buf[BUFSIZE];  /* buffer for ungetch */
int bufp = 0;       /* next free position in buf */
int sp = 0;         /* next free stack position */
double val[MAXVAL]; /* value stack */
double* valp = val;
char math_word_buf[5];   /* longest math operator can be 'sqrt', plus space for null terminator. */

int var_count = 0;       /* next free variable position in variables. */
const char* variables = "abcdefghijklmnopqrstuvwxyz";
double variable_vals[NUM_VARIABLES];

int linep = 0;          /* current character in the line */
char line[MAX_LINE] = "";

/* reverse Polish calculator */
int main(int argc, char* argv[])
{
    int type;
    double power, op2, temp;
    char s[MAXOP];

    while ((type = getop(s)) != EOF)
    {
        switch (type)
        {
            case NUMBER:
                push(atof(s));
                break;
            case NEG_NUMBER:
                if (is_variable(s[0]))
                    push(-resolve_variable(s[0]));
                else
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
                linep = 0;
                temp = pop();
                variable_vals[var_count++] = temp;
                printf("\t%.8g\tstored in variable %c\n", temp, variables[var_count - 1]);
                break;
            case VARIABLE:
                push(resolve_variable(s[0]));
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
                math_word_buf[0] = '\0';
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
    if (linep < 0)
        printf("linep is negative.\n");
    if (linep == 0)
        get_line();

    while ((s[0] = c = line[linep++]) == ' ' || c == '\t')
        ;
    s[1] = '\0';
    if (!isdigit(c) && c != '.')
    {
        if (c == '\n')
            return c;
        if (isdigit(temp = line[linep++]))
        {
            s[0] = temp;
            i = 0;/* collect integer part */
            while (isdigit(s[++i] = c = line[linep++]))
                ;
            if (c == '.') /* collect fraction part */
                while (isdigit(s[++i] = c = line[linep++]))
                    ;
            s[i] = '\0';
            return NEG_NUMBER;
        }
        else
        {
            if (c == '-' && is_variable(temp))
            {
                s[0] = temp;
                return NEG_NUMBER;
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                linep--;
                return c; /* not a number */
            }
            if (temp == ' ' || temp == '\t')
            {
                s[0] = c;
                return VARIABLE; /* then c is the variable. */
            }
            math_word_buf[0] = c;
            math_word_buf[1] = temp;
            int l = 2;
            while (( c = line[linep++]) != ' ' && c != '\t')
            {
                if (c == '\n')
                {
                    linep--;
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
        while (isdigit(s[++i] = c = line[linep++]))
            ;
    if (c == '.') /* collect fraction part */
        while (isdigit(s[++i] = c = line[linep++]))
            ;
    s[i] = '\0';
    if (c != EOF)
        linep--;
    return NUMBER;
}

void get_line()
{
    int c, i;

    while ((c = getchar()) != '\n')
        line[i++] = c;
    line[i] = c;
}

/* is_variable: checks to see if a given character is a variable. */
int is_variable(char c)
{
    return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') ? 1 : 0;
}

/* resolve_variable: given a variable name, returns its corresponding value. */
double resolve_variable(char c)
{
    double result = 0.0;
    for (int i = 0; i < NUM_VARIABLES; i++)
    {
        if (variables[i] == tolower(c))
        {
            result = variable_vals[i];
        }
    }
    return result;
}
