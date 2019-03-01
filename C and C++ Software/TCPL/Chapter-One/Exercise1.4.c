#include <stdio.h>

/* Exercise 1-4.
 *
 * Write a program to print the corresponding Celsius to Fahrenheit table.
 */

int main(int argc, char* argv[])
{
    float fahr, celsius;
    float lower, upper, step;

    lower = -20;   /* lower limit of temperature scale */
    upper = 160;   /* upper limit */
    step = 20;

    celsius = lower;
    printf("  C\tF\n----------\n");
    while (celsius <= upper)
    {
        fahr = (celsius * (9.0/5.0)) + 32.0;
        printf("%3.0f %6.1f\n", celsius, fahr);
        celsius += step;
    }

    return 0;
}
