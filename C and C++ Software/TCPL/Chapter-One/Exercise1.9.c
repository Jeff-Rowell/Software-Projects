#include <stdio.h>
#include <unistd.h>

/* Exercise 1-9.
 *
 * Write a program to copy its input to its output, replacing each string of one or
 * more blanks by a single blank.
 */

int main(int argc, char* argv[])
{
    char string[BUFSIZ];
    int c;

    while ((c = read(0, string, BUFSIZ)) > 0)
    {
         write(1, string, c);
    }

    return 0;
}