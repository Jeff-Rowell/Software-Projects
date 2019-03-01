#include <stdio.h>

/* Exercise 1-6.
 *
 * Verify that the expression getchar() != EOF is 0 or 1.
 */

int main(int argc, char* argv[])
{
    printf("Please enter a test character...\n");
    char c = getchar();
    int result = ((c != EOF) == 0) || ((c != EOF) == 1);
    if (result == 0 || result == 1)
    {
        printf("getchar() != EOF is indeed 0 or 1!\n");
    }

    return 0;
}
