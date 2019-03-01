#include <stdio.h>
#define NUM_BITS 32

/* Exercise 2-6.
 *
 * Write a function setbits(x,p,n,y) that returns x with the n bits that begin at
 * position p set to the rightmost n bits of y, leaving the other bits unchanged.
 */

void to_bin(unsigned int x, char* s);
unsigned set_bits(unsigned x, int p, int n, unsigned y);
unsigned get_bits(unsigned x, int p, int n);

int main(int argc, char* argv[])
{
    char bit_string[NUM_BITS + 1];    /* +1 to make room for null terminator */
    unsigned x = 0x12300000;
    unsigned y = 0x000AB123;
    unsigned z;

    z = set_bits(x, 20, 20, y);
    to_bin(x, bit_string);
    printf("x: %s\n", bit_string);
    to_bin(y, bit_string);
    printf("y: %s\n", bit_string);
    to_bin(z, bit_string);
    printf("z: %s\n", bit_string);
    return 0;
}

unsigned int set_bits(unsigned x, int p, int n, unsigned y)
{
    unsigned a = (x & ~(~(~0 << n) << (p - n)));
    unsigned b = ((y & ~(~0 << n)) << (p - n));
    return  (a | b);
}

/* get_bits: get n bits from position p */
unsigned get_bits(unsigned x, int p, int n)
{
    return (x >> (p+1-n)) & ~(~0 << n);
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
