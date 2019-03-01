#include <stdio.h>
#include <string.h>
#include <limits.h>
#define MAX 10

/* Exercise 3-4.
 *
 * In a two's complement number representation, our version of itoa does not
 * handle the largest negative number, that is, the value of n equal to -(2^(wordsize-1)). Explain why
 * not. Modify it to print that value correctly, regardless of the machine on which it runs.
 */

void itoa(int n, char *s);
void reverse(char s[]);

int main(int argc, char* argv[])
{
    char s[MAX + 2];        /* room for 10 digits, null terminator, and a negative sign if needed */
    itoa(INT_MIN, s);
    printf("Result = %s\n", s);

    return 0;
}

/* itoa: convert n to characters in s */
void itoa(int n, char s[])
{
    int i, temp, sign;

    sign = n;
    i = 0;
    do
    { /* generate digits in reverse order */
        temp = n % 10;

        if (temp < 0) /* only check the digits for negativity, then make them positive to avoid overflow */
            temp = -temp;

        s[i++] = temp + '0'; /* get next digit */
    } while ((n /= 10) != 0); /* delete it */

    if (sign < 0)
        s[i++] = '-';

    s[i] = '\0';
    reverse(s);
}

/* reverse: reverse string s in place */
void reverse(char s[])
{
    int c, i, j;
    for (i = 0, j = strlen(s)-1; i < j; i++, j--)
    {
        c = s[i];
        s[i] = s[j];
        s[j] = c;
    }
}

/*
 * The reason that the book version of itoa does not handle the largest negative number (i.e. -2147483648) is because
 * it relies on converting the negative numbers into positive numbers first before translating into a string. The
 * problem with this is that the absolute value of the largest negative number is one greater than that of the largest
 * positive number, so when itoa converts the largest negative number into a positive number, it overflows by 1
 * resulting back into the largest negative number.
 */
