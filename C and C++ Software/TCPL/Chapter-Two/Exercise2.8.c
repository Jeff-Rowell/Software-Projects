#include <stdio.h>
#define NUM_BITS 32

/* Exercise 2-8.
 *
 * Write a function rightrot(x,n) that returns the value of the integer x rotated
 * to the right by n positions.
 */

unsigned right_rot(unsigned x, int n);
unsigned left_rot(unsigned x, int n);
void to_bin(unsigned x, char* s);

int main(int argc, char* argv[])
{
    char s[NUM_BITS + 1]; /* +1 to make room for null terminator */
    unsigned x = 0xabcdef12;
    unsigned result = right_rot(x, 4);
    to_bin(x, s);
    printf("right_rot(x, 4);\nx: %s\n", s);
    to_bin(result, s);
    printf("x: %s\n", s);
    result = left_rot(result, 4);
    to_bin(result, s);
    printf("left_rot(x, 4);\nx: %s\n", s);

    return 0;
}

unsigned left_rot(unsigned x, int n)
{
    while (n > 0)
    {
        /* Push off the leftmost bit, then save the leftmost bit and shift it right by 31 bits */
        x = (x << 1) | ((x & 0x80000000) >> (NUM_BITS - 1));
        n--;
    }
    return x;
}

unsigned right_rot(unsigned x, int n)
{
    while (n > 0)
    {
        /* Push off the rightmost bit, then save the rightmost bit and shift it left by 31 bits */
        x = (x >> 1) | ((x & 1) << (NUM_BITS - 1));
        n--;
    }
    return x;
}

/* Helper to convert results to binary */
void to_bin(unsigned x, char* s)
{
    int i = NUM_BITS - 1, index = 0;
    unsigned bit;

    while (i >= 0)
    {
        bit = get_bits(x, i, 1);
        if (bit & 1)
        {
            s[index] = '1';
        }
        else
        {
            s[index] = '0';
        }
        i--;
        index++;
    }
    s[index] = '\0';
}
