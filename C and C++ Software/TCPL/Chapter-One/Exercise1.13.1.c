#include <stdio.h>

/* Exercise 1-13.
 *
 * Write a program to print a histogram of the lengths of words in its input. It is
 * easy to draw the histogram with the bars horizontal; a vertical orientation is more challenging.
 */

/* prints a horizontal histogram of the lengths of the words input */
int main(int argc, char* argv[])
{
    int c;

    while ((c = getchar()) != EOF)
    {
        if (c == '\n' || c == ' ' || c == '\t')
        {
            printf("\n");
        }
        else
            printf("*");
    }

    return 0;
}