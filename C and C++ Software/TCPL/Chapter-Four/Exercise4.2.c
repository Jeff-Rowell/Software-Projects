#include <stdio.h>
#include <ctype.h>
#include <string.h>

/* Exercise 4-2.
 *
 * Extend atof to handle scientific notation of the form
 * 123.45e-6
 * where a floating-point number may be followed by e or E and an optionally signed exponent.
 */

double atof(char s[]);

int main(int argc, char* argv[])
{
    int i = 0;
    char c, *tests[] = {"123.45", "0.001e-2", "0.1e+2", "123.456e+3", "123.456e-3", "123.45e-6", "123.45e+6"};

    for (int k = 0; k < (sizeof(tests) / sizeof(*tests)); k++)
        printf("Converting %s to a floating-point number ...\nResult = %.09f\n\n", tests[k], atof(tests[k]));

    return 0;
}

/* atof: convert string s to double */
double atof(char s[])
{
    double val, power;
    int i, sign, e_index = strlen(s), pad_length;

    while (e_index > 0)
    {
        if (s[e_index] == 'e' || s[e_index] == 'E')
            break;
        else
            e_index--;
    }

    for (i = 0; isspace(s[i]); i++) /* skip white space */
        ;
    sign = (s[i] == '-') ? -1 : 1;
    if (s[i] == '+' || s[i] == '-')
        i++;

    for (val = 0.0; isdigit(s[i]); i++)
        val = 10.0 * val + (s[i] - '0');

    while (s[i] != '.' && !isdigit(s[i])) /* pass over e- or E- */
    {
        i++;
    }

    if (s[i] == '.')
        i++;

    for (power = 1.0; isdigit(s[i]) && (i < e_index || e_index == 0); i++)
    {
        val = 10.0 * val + (s[i] - '0');
        power *= 10;
    }

    if (e_index > 0)
    {
        pad_length = s[e_index + 2] - '0';
        if (s[e_index + 1] == '-')
        {
            for (i = 0; i < pad_length; i++)
                power *= 10.0;
        }
        else
        {
            for (i = 0; i < pad_length; i++)
                power /= 10.0;
        }
    }

    return sign * val / power;
}
