#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#define MAXLINELENGTH 120

/* Exercise 1-24.
 *
 * Write a program to check a C program for rudimentary syntax errors like
 * unmatched parentheses, brackets and braces. Don't forget about quotes, both single and
 * double, escape sequences, and comments. (This program is hard if you do it in full
 * generality.)
 */

int pop();
int push(int);
int peek();

int main(int argc, char* argv[])
{
    FILE *infp = fopen("program-in2.txt","r");
    char line[MAXLINELENGTH];
    char c, temp;
    int result, i = 0, k = 0, line_number = 1, errors = 0, open_delimiter_count = 0, closed_delimiter_count = 0;

    while(fgets(line, MAXLINELENGTH, infp))
    {
        while (i < MAXLINELENGTH && (c = line[i]) != '\n')
        {
            if ((i + 1) < MAXLINELENGTH)
            {
                if (c == '/' && line[i + 1] == '*')
                {
                    open_delimiter_count++;
                }
                if (c == '*' && line[i + 1] == '/')
                {
                    closed_delimiter_count++;
                }
            }

            switch (c)
            {
                case '[': case '{': case '(':
                    result = push(c);
                    if (result == -1)
                    {
                        printf("Stack is empty.\n");
                        exit(EXIT_FAILURE);
                    }
                    break;
                case ')':
                    if (peek() == '(')
                    {
                        pop();
                    }
                    else
                    {
                        printf("'%c' error on line %d\n", c, line_number);
                        errors++;
                    }
                    break;
                case '}':
                    if (peek() == '{')
                    {
                        pop();
                    }
                    else
                    {
                        printf("'%c' error on line %d\n", c, line_number);
                        errors++;
                    }
                    break;
                case ']':
                    if (peek() == '[')
                    {
                        pop();
                    }
                    else
                    {
                        printf("'%c' error on line %d\n", c, line_number + 1);
                        errors++;
                    }
                    break;
                case '\'':
                    k = i + 1;
                    while ((temp = line[k]) != '\0')
                    {
                        if (temp == '\'')
                        {
                            break;
                        }
                        if (temp == '\n')
                        {
                            printf("Missing a closing ' on line %d\n", line_number);
                            errors++;
                            break;
                        }
                        k++;
                    }
                    i = k;
                    break;
                case '"':
                    k = i + 1;
                    while ((temp = line[k]) != '\0')
                    {
                        if (temp == '"')
                        {
                            break;
                        }
                        if (temp == '\n')
                        {
                            printf("Missing a closing \" on line %d\n", line_number);
                            errors++;
                            break;
                        }
                        k++;
                    }
                    i = k;
                    break;
            }
            i++;
        }
        i = 0;
        line_number++;
    }

    while (peek() != -1)
    {
        temp = pop();
        if (temp == '(')
            printf("Missing a closing )\n");
        if (temp == '[')
            printf("Missing a closing ]\n");
        if (temp == '{')
            printf("Missing a closing }\n");
        errors++;
    }
    if (open_delimiter_count != closed_delimiter_count)
    {
        printf("Missing a comment delimiter\n");
        errors++;
    }
    if (errors > 0)
    {
        printf("\nTotal Number of Errors: %d\n", errors);
        return 1;
    }

    return 0;
}

#define STACK_MAX 2000

int stack[STACK_MAX];
int *stackp = stack;

int get_stack_size()
{
    return stackp - stack;
}

int push(int item)
{
    if (get_stack_size() >= STACK_MAX)
    {
        return -1;
    }

    *(stackp++) = item;
    return item;
}

int pop()
{
    if (get_stack_size() <= 0)
    {
        return -1;
    }

    return *(--stackp);
}

int peek()
{
    if (get_stack_size() <= 0)
    {
        return -1;
    }
    return *(stackp - 1);
}
