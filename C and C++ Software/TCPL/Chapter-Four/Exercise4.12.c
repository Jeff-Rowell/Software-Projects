#include <stdio.h>

#define MAX 100

/* Exercise 4-12.
 *
 * Adapt the ideas of printd to write a recursive version of itoa; that is, convert
 * an integer into a string by calling a recursive routine.
 */

int itoa(int, char[]);

int main(int argc, char* argv[])
{
    char a[MAX];
    itoa(0, a);
    printf("%d : %s\n", 0, a);
    itoa(-303, a);
    printf("%d : %s\n", -303, a);
    itoa(99979, a);
    printf("%d : %s\n", 99979, a);
    itoa(123456, a);
    printf("%d : %s\n", 123456, a);
    return 0;
}

int itoa(int i, char a[])
{
    int index = 0;

    if (i < 0)
    {
        a[index++] = '-';
        i = -i;
    }
    if (i / 10)
        index += itoa((i / 10), a + index);
    a[index++] = (i % 10) + '0';
    a[index] = '\0';
    return index;
}