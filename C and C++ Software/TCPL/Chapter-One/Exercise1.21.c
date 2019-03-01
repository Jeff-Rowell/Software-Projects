#include <stdio.h>
#define TABSTOP 8

/* Exercise 1-21.
 *
 * Write a program entab that replaces strings of blanks by the minimum
 * number of tabs and blanks to achieve the same spacing. Use the same tab stops as for detab.
 * When either a tab or a single blank would suffice to reach a tab stop, which should be given
 * preference?
 */

void printBlanks(int num_blanks);

int main(int argc, char* argv[])
{
    int c, i, blanks = 0;

    while ((c = getchar()) != EOF)
    {
        if (c == ' ')
        {
            ++blanks; // blanks is 1
            while ((c = getchar()) == ' ')
            {
                ++blanks;
                if (blanks == TABSTOP)
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

    return 0;
}

void printBlanks(int num_blanks)
{
    int i;

    for (i = 0; i < num_blanks; ++i)
    {
        putchar(' ');
    }
}







