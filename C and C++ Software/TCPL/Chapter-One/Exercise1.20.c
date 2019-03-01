#include <stdio.h>
#define TABSTOP 8

/* Exercise 1-20.
 *
 * Write a program detab that replaces tabs in the input with the proper number
 * of blanks to space to the next tab stop. Assume a fixed set of tab stops, say every n columns.
 * Should n be a variable or a symbolic parameter?
 */

int main(int argc, char* argv[])
{
    int c, i, column = 0;

    while ((c = getchar()) != EOF)
    {
        if (c == '\t')
        {
            for (i = 0; i < TABSTOP - column % TABSTOP; ++i)
            {
                putchar(' ');
            }
            column += TABSTOP - column % TABSTOP;
        }
        else if (c == '\n')
        {
            putchar(c);
            column = 0;
        }
        else
        {
            putchar(c);
            column++;
        }
    }

    return 0;
}







