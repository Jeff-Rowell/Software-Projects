#include <stdio.h>
#define NUM_BITS 32

/* Exercise 2-7.c
 *
 * Write a function invert(x,p,n) that returns x with the n bits that begin at position p inverted
 * (i.e., 1 changed into 0 and vice versa), leaving the others unchanged.
 */

void to_bin(unsigned x, char* s);
unsigned invert(unsigned x, int p, int n);

int main(int argc, char* argv[])
{
    char s1[NUM_BITS + 1]; /* +1 to make room for null terminator */
    char s2[NUM_BITS + 1];
    unsigned x = 0xABD01234;
    unsigned result = invert(x, 31, 2);
    to_bin(x, s1);
    to_bin(result, s2);
    printf("invert(x, 31, 2);\nx:\t%s\nResult: %s\n", s1, s2);

    return 0;
}

unsigned invert(unsigned x, int p, int n)
{
    return x ^ (~(~0 << n) << (p - n + 1));
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
