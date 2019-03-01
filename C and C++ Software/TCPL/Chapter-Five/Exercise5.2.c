#include <ctype.h>
#include <stdio.h>

#define SIZE 20
#define MAXCHAR 100

/* Exercise 5-2.
 *
 * Write getfloat, the floating-point analog of getint. What type does
 * getfloat return as its function value?
 *
 * => getfloat() shall return type int the same as getint().
 */

int getfloat(float *pn);

static char getchbuf[MAXCHAR];
static int getchindex = 0;

int main(int argc, char* argv[])
{
    int n;
    float array[SIZE];

    for (n = 0; n < SIZE && getfloat(&array[n]) != EOF; n++)
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

/* getfloat: get next integer from input into *pn */
int getfloat(float *pn)
{
    int c, sign;
    float power;

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
        c = getch();
    }
    for (*pn = 0; isdigit(c); c = getch())
    {
        *pn = 10.0 * *pn + (c - '0');
    }
    if (c == '.')
    {
        c = getch();
    }
    for (power = 1.0; isdigit(c); c = getch())
    {
        *pn = 10 * *pn + (c - '0');
        power *= 10.0;
    }
    *pn *= (sign/power);
    if (c != EOF)
    {
        ungetch(c);
    }
    return c;
}
