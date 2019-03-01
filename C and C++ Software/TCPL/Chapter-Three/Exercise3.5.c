#include <stdio.h>
#include <string.h>
#define MAX 150

/* Exercise 3-5.
 *
 * Write the function itob(n,s,b) that converts the integer n into a base b
 * character representation in the string s. In particular, itob(n,s,16) formats n as a
 * hexadecimal integer in s.
 */

void itob(int n, char* s, int b);
void reverse(char* s);

int main(int argc, char* argv[])
{
    int n = 10;
    int b = 16;
    char s[MAX];

    itob(n, s, b);
    printf("Converting %d to base %d ...\nResult = %s\n", n, b, s);
    return 0;
}

/* converts n into the string s, using base b. I assume b will not be a negative base. */
void itob(int n, char* s, int b)
{
    int i, temp, sign;
    sign = n;   /* record sign */
    i = 0;

    do
    { /* generate digits in reverse order */
        temp = n % b;
        if (temp < 0)
            temp = -temp;
        if (temp >= 10)
            s[i++] = temp + '7';
        else
            s[i++] = temp + '0'; /* get next digit */
    } while ((n /= b) != 0);  /* delete it */

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
