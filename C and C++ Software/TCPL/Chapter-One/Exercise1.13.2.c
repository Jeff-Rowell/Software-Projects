#include <stdio.h>
#define MAXSIZE 30 /* max number of words per line */

/* Exercise 1-13.
 *
 * Write a program to print a histogram of the lengths of words in its input. It is
 * easy to draw the histogram with the bars horizontal; a vertical orientation is more challenging.
 */

/* prints a vertical histogram of the lengths of the words input */
int main(int argc, char* argv[])
{
    int i, j, c, nc, nw;
    int words[MAXSIZE];

    nc = nw = 0;

    for (i = 0; i < MAXSIZE; ++i)
    {
        words[i] = 0;
    }

    while ((c = getchar()) != EOF)
    {
        ++nc;
        if (c == '\n' || c == ' ' || c == '\t')
        {
            words[nw] = nc - 1;
            ++nw;
            nc = 0;
        }
    }

    for (i = MAXSIZE; i >= 1; --i)
    {
        for (j = 0; j <= nw; ++j)
        {
            if (i <= words[j])
            {
                putchar('*');
            }
            else
            {
                putchar(' ');
            }
        }
        putchar('\n');
    }

    return 0;
}

