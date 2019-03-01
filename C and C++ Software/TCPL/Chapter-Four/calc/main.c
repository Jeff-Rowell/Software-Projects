#include <stdio.h>
#include <string.h>
#include <math.h>
#include <stdlib.h>
#include <ctype.h>
#include "calc.h"

#define MAXOP 100       /* max size of operand or operator */
#define NUM_VARIABLES 26

int var_count = 0;       /* next free variable position in variables. */
const char* variables = "abcdefghijklmnopqrstuvwxyz";
double variable_vals[NUM_VARIABLES];

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
                break;
            default:
                printf("error: unknown command %s\n", s);
                break;
        }
    }
    return 0;
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