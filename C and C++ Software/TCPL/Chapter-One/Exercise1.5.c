#include <stdio.h>

/* Exercise 1-5.
 *
 * Modify the temperature conversion program to print the table in reverse order,
 * that is, from 300 degrees to 0.
 */

int main(int argc, char* argv[])
{
    float fahr, celsius;
    float lower, upper, step;

    lower = 0;    /* lower limit of temperature scale */
    upper = 300;  /* upper limit */
    step = 20;

    fahr = upper;
    printf("  F\tC\n----------\n");
    while (fahr >= lower)
    {
        celsius = (5.0/9.0) * (fahr - 32.0);
        printf("%3.0f %6.1f\n", fahr, celsius);
        fahr -= step;
    }

    return 0;
}
