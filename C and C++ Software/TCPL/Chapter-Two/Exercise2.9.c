#include <stdio.h>

/* Exercise 2-9.
 *
 * In a two's complement number system, x &= (x-1) deletes the rightmost 1-bit
 * in x. Explain why. Use this observation to write a faster version of bitcount.
 */

int bit_count(unsigned x);

int main(int argc, char* argv[])
{
    unsigned x = 0xAB;
    int result = bit_count(x);
    printf("Bitcount: %d\n", result);
    return result;
}

int bit_count(unsigned x)
{
    int count = 1;
    while ((x &= (x-1)) != 0)
        count++;
    return count;
}

/*
 * The reason the operation x = x & (x - 1) deletes the rightmost 1-bit
 * in x is because (x - 1) converts the rightmost group of 0's to all
 * 1's, and it also turns the rightmost 1 to 0 after the AND. Taking the
 * original x and performing an AND operation with (x - 1) has the effect
 * of converting the rightmost 1 into a 0, and leaving all of the other bits
 * in tact.
 *
 * x = 0100
 * x - 1 = 0011
 *
 *   0100
 * & 0011
 * --------
 *   0000
 */
