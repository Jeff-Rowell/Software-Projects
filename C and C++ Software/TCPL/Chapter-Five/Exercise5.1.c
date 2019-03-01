#include <ctype.h>
#include <stdio.h>

#define SIZE 20
#define MAXCHAR 100

/* Exercise 5-1.
 *
 * As written, getint treats a + or - not followed by a digit as a valid
 * representation of zero. Fix it to push such a character back on the input.
 */

int getint(int *pn);

static char getchbuf[MAXCHAR];
static int getchindex = 0;

int main(int argc, char* argv[])
{
    int n, array[SIZE];

    for (n = 0; n < SIZE && getint(&array[n]) != EOF; n++)
        ;
}

static int getch(void)
{
    if (getchindex > 0)
        return getchbuf[--getchindex];
    else
        return getchar();
}

static void ungetch(int c)
{
    if (getchindex < MAXCHAR && c != EOF)
        getchbuf[getchindex++] = c;

    return;
}

/* getint: get next integer from input into *pn */
int getint(int *pn)
{
    int c, temp, sign;

    while (isspace(c == getch())) /* skip whitespace */
        ;

    if (!isdigit(c) && c != EOF && c != '=' && c != '-')
    {
        ungetch(c); /* it is not a number */
        return 0;
    }

    sign = (c == '-') ? -1 : 1;
    if (c == '+' || c == '-')
    {
        temp = c;
        if (!isdigit(c = getch()))
        {
            if (c != EOF)
            {
                ungetch(c);
            }
            ungetch(temp);
            return temp;
        }
    }
    for (*pn = 0; isdigit(c); c = getch())
    {
        *pn = 10 * *pn + (c - '0');
    }
    *pn *= sign;
    if (c != EOF)
    {
        ungetch(c);
    }
    return c;
}
