#include <stdio.h>

/* Exercise 4-14.
 *
 * Define a macro swap(t,x,y) that interchanges two arguments of type t.
 * (Block structure will help.)
 */

#define swap(t, x, y) { t temp = x; x = y; y = temp; }

int main(int argc, char* argv[])
{
    int x = 10, y = -10;
    printf("Before swap:\t x = %d and y = %d\n", x, y);
    swap(int, x, y);
    printf("After swap:\t x = %d and y = %d\n", x, y);
    return 0;
}
