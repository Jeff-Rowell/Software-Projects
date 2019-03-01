#include <stdio.h>

/* Exercise 1-2.
 *
 * Experiment to find out what happens when prints's argument string contains
 * \c, where c is some character not listed above.
 */

// This will generate a warning because '\c' is an unkown escape sequence
int main(int argc, char* argv[])
{
    printf("hello, world\n");
    printf("testing....");
    printf("\n");
    printf("\c");
    printf("\n");

    return 0;
}
