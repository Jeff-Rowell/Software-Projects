#include <stdio.h>

/* Exercise 2-10.
 *
 * Rewrite the function lower, which converts upper case letters to lower case,
 * with a conditional expression instead of if-else.
 */

int ternary_lower(int c);

int main(int argc, char* argv[])
{
    printf("Input a letter to convert to lower case: ");
    char c = getchar();

    printf("Result = %c\n", ternary_lower(c));
    return 0;
}

int ternary_lower(int c)
{
    return (c >= 'A' && c <= 'Z') ? c + 'a' - 'A' : c;
}


