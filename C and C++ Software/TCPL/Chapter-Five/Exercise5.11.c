#include <stdio.h>
#include <stdlib.h>

#define TABSTOP 8

/* Exercise 5-11.
 *
 * Modify the program entab and detab (written as exercises in Chapter 1) to accept a
 * list of tab stops as arguments. Use the default tab settings if there are no arguments.
 */

void printBlanks(int);
void entab(int);
void detab(int);

int main(int argc, char *argv[])
{
    if (argc == 1)
    {
        printf("Calling entab() ...\n");
        entab(TABSTOP);
        printf("\nCalling detab() ...\n");
        detab(TABSTOP);
    }
    else
    {
        printf("Calling entab() ...\n");
        entab(atoi(*(++argv)));
        printf("\nCalling detab() ...\n");
        detab(atoi(*argv));
    }
    printf("\n");
    return 0;
}

void entab(int tabstop)
{
    int c, i, blanks = 0;

    while ((c = getchar()) != '\n')
    {
        if (c == ' ')
        {
            ++blanks;
            while ((c = getchar()) == ' ')
            {
                ++blanks;
                if (blanks == tabstop)
                {
                    putchar('\t');
                    blanks = 0;
                }
            }
            printBlanks(blanks);
            if (c != ' ')
            {
                putchar(c);
            }
        }
        else
        {
            putchar(c);
        }
    }
}

void detab(int tabstop)
{
    int c, i, column = 0;

    while ((c = getchar()) != '\n')
    {
        if (c == '\t')
        {
            for (i = 0; i < tabstop - column % tabstop; ++i)
            {
                putchar(' ');
            }
            column += tabstop - column % tabstop;
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
}

void printBlanks(int num_blanks)
{
    int i;

    for (i = 0; i < num_blanks; ++i)
    {
        putchar(' ');
    }
}
