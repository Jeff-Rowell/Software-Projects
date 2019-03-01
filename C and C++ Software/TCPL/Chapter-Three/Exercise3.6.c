#include <stdio.h>
#include <string.h>
#include <limits.h>
#define MAX 150

/* Exercise 3-6.
 * 
 * Write a version of itoa that accepts three arguments instead of two. The third
 * argument is a minimum field width; the converted number must be padded with blanks on the
 * left if necessary to make it wide enough.
 */

void itoa(int n, char s[], int min);
void reverse(char s[]);
void left_pad(char s[], int min);

int main(int argc, char* argv[])
{
    char s[MAX];
    int n = 210, min = 40;
    itoa(INT_MIN, s, min);
    printf("Converting %d using minimum field width %d ...\nResult = %s\n", INT_MIN, min, s);
    return 0;
}

void itoa(int n, char* s, int min)
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
    left_pad(s, min);
}

void reverse(char s[])
{
    int c, i, j;

    for (i = 0, j = strlen(s) - 1; i < j; i++, j--)
    {
        c = s[i];
        s[i] = s[j];
        s[j] = c;
    }
}

void left_pad(char s[], int min)
{
    int i, j;
    int len = strlen(s), diff;
    if (len < min)
        diff = min - len;
    else
        diff = len - min;
    char padded_s[MAX];

    j = 0;
    for (i = 0; i < (diff + len); i++)
    {
        if (i < diff)
        {
            padded_s[i] = ' ';
        }
        else
        {
            padded_s[i] = s[j];
            j++;
        }
    }

    for (i = 0; padded_s[i] != '\0'; i++)
        s[i] = padded_s[i];
    s[i] = '\0';
}
