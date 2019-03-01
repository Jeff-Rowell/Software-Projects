#include <stdio.h>
#include <string.h>

/* Exercise 1-15.
 *
 * Rewrite the temperature conversion program of Section 1.2 to use a function for conversion.
 */

void convert_temp(char* from);

int main(int argc, char* argv[])
{
    printf("Converting from celsius to fahrenheit ...\n");
    convert_temp("f");
    printf("\nConverting from fahrenheit to celsius ...\n");
    convert_temp("c");
    return 0;
}

void convert_temp(char* from)
{
    float fahr, celsius;
    float lower, upper, step;

    if (strcmp(from, "f") != 0)
    {
        lower = 0;    /* lower limit of temperature scale */
        upper = 300;  /* upper limit */
        step = 20;

        fahr = lower;
        printf("  F\tC\n----------\n");
        while (fahr <= upper)
        {
            celsius = (5.0/9.0) * (fahr - 32.0);
            printf("%3.0f %6.1f\n", fahr, celsius);
            fahr += step;
        }
    }
    if (strcmp(from, "c") != 0)
    {
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
    }
    else
    {
        printf("Invalid 'from' value given.");
    }
}
