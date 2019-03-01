#include <stdio.h>
#include <limits.h>
#include <float.h>

/* Exercise 2-1.
 *
 * Write a program to determine the ranges of char, short, int, and long
 * variables, both signed and unsigned, by printing appropriate values from standard headers
 * and by direct computation. Harder if you compute them: determine the ranges of the various
 * floating-point types.
*/

void print_char_min_max()
{
    char c, cmin, cmax;
    int lower_bound, upper_bound, midpoint;
    lower_bound = 0;
    upper_bound = 1;

    while ((char)upper_bound >= 0)
    {
        lower_bound = upper_bound;
        upper_bound *= 2;
    }
    while (lower_bound < upper_bound)
    {
        midpoint = (lower_bound + upper_bound) / 2;
        if ((char) midpoint > 0)
        {
            lower_bound = midpoint + 1;
        }
        else
        {
            upper_bound = midpoint;
        }
    }
    cmin = upper_bound;
    cmax = cmin - 1;
    printf("Type `char' min and max from direct computation:\nMIN = %d\nMAX = %d\n\n", cmin, cmax);
    printf("Type `char' min and max from Standard Headers:\nMIN = %d\nMAX = %d\n\n", CHAR_MIN, CHAR_MAX);
}

void print_short_min_max()
{
    short s, smin, smax;
    int lower_bound, upper_bound, midpoint;
    lower_bound = 0;
    upper_bound = 1;

    while ((short)upper_bound >= 0)
    {
        lower_bound = upper_bound;
        upper_bound *= 2;
    }
    while (lower_bound < upper_bound)
    {
        midpoint = (lower_bound + upper_bound) / 2;
        if ((short) midpoint > 0)
        {
            lower_bound = midpoint + 1;
        }
        else
        {
            upper_bound = midpoint;
        }
    }

    smin = upper_bound;
    smax = smin - 1;
    printf("Type `short' min and max from direct computation:\nMIN = %d\nMAX = %d\n\n", smin, smax);
    printf("Type `short' min and max from Standard Headers:\nMIN = %d\nMAX = %d\n\n", SHRT_MIN, SHRT_MAX);
}

void print_int_min_max()
{
    int i, imin, imax;
    int lower_bound, upper_bound, midpoint;
    lower_bound = 0;
    upper_bound = 1;

    while (upper_bound >= 0)
    {
        lower_bound = upper_bound;
        upper_bound *= 2;
    }
    while (lower_bound < upper_bound)
    {
        midpoint = (lower_bound + upper_bound) / 2;
        if (midpoint > 0)
        {
            lower_bound = midpoint + 1;
        }
        else
        {
            upper_bound = midpoint;
        }
    }

    imin = upper_bound;
    imax = imin - 1;
    printf("Type `int' min and max from direct computation:\nMIN = %d\nMAX = %d\n\n", imin, imax);
    printf("Type `int' min and max from Standard Headers:\nMIN = %d\nMAX = %d\n\n", INT_MIN, INT_MAX);
}

void print_long_min_max()
{
    long l, lmin, lmax;
    int lower_bound, upper_bound, midpoint;
    lower_bound = 0;
    upper_bound = 1;

    while ((long)upper_bound >= 0)
    {
        lower_bound = upper_bound;
        upper_bound *= 2;
    }
    while (lower_bound < upper_bound)
    {
        midpoint = (lower_bound + upper_bound) / 2;
        if ((long)midpoint > 0)
        {
            lower_bound = midpoint + 1;
        }
        else
        {
            upper_bound = midpoint;
        }
    }

    lmin = upper_bound;
    lmax = lmin - 1;
    printf("Type `long' min and max from direct computation:\nMIN = %d\nMAX = %d\n\n", lmin, lmax);
    printf("Type `long' min and max from Standard Headers:\nMIN = %d\nMAX = %d\n\n", LONG_MIN, LONG_MAX);
}

int main(int argc, char* argv[])
{
    print_char_min_max();
    print_short_min_max();
    print_int_min_max();
    print_long_min_max();
    return 0;
}
