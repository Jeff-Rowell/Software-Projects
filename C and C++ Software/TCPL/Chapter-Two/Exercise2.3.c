#include <stdio.h>
#include <stdlib.h>
#define MAX_HEX_STRING 16

/* Exercise 2-3.
 *
 * Write a function htoi(s), which converts a string of hexadecimal digits (including an optional 0x or 0X)
 * into its equivalent integer value. The allowable digits are 0 through 9, a through f, and A through F.
 */

long h_to_i(char* s);
long long_exp(int base, int power);

int main(int argc, char* argv[])
{
    int i = 0;
    char c, hex_string[MAX_HEX_STRING];

    printf("Please enter a HEX value to convert to an integer ...\n");
    while ((c = getchar()) != EOF)
    {
        hex_string[i] = c;
        i++;
    }
    hex_string[i] = '\0';
    printf("Converting to integer: %sThe converted HEX value is: %ld\n", string, h_to_i(hex_string));
    return 0;
}

long long_exp(int base, int power)
{
    long result = 1;
    for (int i = 1; i <= power; i++)
        result = result * base;
    return result;
}

long h_to_i(char* s)
{
    char *temp = s, c;
    int start_index = 0, exponent = 0;
    long result = 0;

    while (*temp != '\n')
    {
        c = *temp;
        if (c == 'x' || c == 'X')
            break;
        else
            start_index++;
        temp++;
    }
    start_index++;
    if (start_index > 2)
        start_index = 0;

    temp = s + start_index;
    while (*(temp + 1) != '\n')
    {
        exponent++;
        temp++;
    }

    temp = s + start_index;
    while (*temp != '\n')
    {
        c = *temp;
        printf("Current char %c\nCurrent exp %d\n", c, exponent);
        if (c == '0')
        {
            result += 0 * long_exp(16, exponent);
        }
        else if (c == '1')
        {
            result += 1 * long_exp(16, exponent);
        }
        else if (c == '2')
        {
            result += 2 * long_exp(16, exponent);
        }
        else if (c == '3')
        {
            result += 3 * long_exp(16, exponent);
        }
        else if (c == '4')
        {
            result += 4 * long_exp(16, exponent);
        }
        else if (c == '5')
        {
            result += 5 * long_exp(16, exponent);
        }
        else if (c == '6')
        {
            result += 6 * long_exp(16, exponent);
        }
        else if (c == '7')
        {
            result += 7 * long_exp(16, exponent);
        }
        else if (c == '8')
        {
            result += 8 * long_exp(16, exponent);
        }
        else if (c == '9')
        {
            result += 9 * long_exp(16, exponent);
        }
        else if (c == 'A' || c == 'a')
        {
            result += 10 * long_exp(16, exponent);
        }
        else if (c == 'B' || c == 'b')
        {
            result += 11 * long_exp(16, exponent);
        }
        else if (c == 'C' || c == 'c')
        {
            result += 12 * long_exp(16, exponent);
        }
        else if (c == 'D' || c == 'd')
        {
            result += 13 * long_exp(16, exponent);
        }
        else if (c == 'E' || c == 'e')
        {
            result += 14 * long_exp(16, exponent);
        }
        else if (c == 'F' || c == 'f')
        {
            result += 15 * long_exp(16, exponent);
        }
        else
        {
            printf("Error: Unknown HEX value given: %c\n", c);
            exit(EXIT_FAILURE);
        }
        temp++;
        exponent--;
    }

    return result;
}