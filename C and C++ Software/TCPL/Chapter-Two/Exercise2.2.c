#include <stdio.h>
#define BUFFER_SIZE 50

/* Exercise 2-2.
 *
 * for (i=0; i < lim-1 && (c=getchar()) != '\n' && c != EOF; ++i)
 *     s[i] = c;
 *
 * Write a loop equivalent to the for loop above without using && or ||.
 */

int main(int argc, char* argv[])
{
    char s[BUFFER_SIZE], c;
    s[BUFFER_SIZE - 1] = '\0';

    for (int i = 0; (i < BUFFER_SIZE - 1) ? ((c = getchar()) != '\n') ? (c != EOF) : 0 : 0; ++i)
        s[i] = c;

    printf("%s\n", s);

    return 0;
}