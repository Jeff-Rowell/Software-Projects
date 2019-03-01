#include <stdio.h>

/* Exercise 1-14.
 *
 * Write a program to print a histogram of the frequencies of different characters
 * in its input.
 */

int main(int argc, char* argv[])
{
    int count_array[26], c;

    for (int i = 0; i < 26; i++)
        count_array[i] = 0;

    while ((c = getchar()) != EOF)
    {
        if (c == 'a')
            count_array[0]++;
        if (c == 'b')
            count_array[1]++;
        if (c == 'c')
            count_array[2]++;
        if (c == 'd')
            count_array[3]++;
        if (c == 'e')
            count_array[4]++;
        if (c == 'f')
            count_array[5]++;
        if (c == 'g')
            count_array[6]++;
        if (c == 'h')
            count_array[7]++;
        if (c == 'i')
            count_array[8]++;
        if (c == 'j')
            count_array[9]++;
        if (c == 'k')
            count_array[10]++;
        if (c == 'l')
            count_array[11]++;
        if (c == 'm')
            count_array[12]++;
        if (c == 'n')
            count_array[13]++;
        if (c == 'o')
            count_array[14]++;
        if (c == 'p')
            count_array[15]++;
        if (c == 'q')
            count_array[16]++;
        if (c == 'r')
            count_array[17]++;
        if (c == 's')
            count_array[18]++;
        if (c == 't')
            count_array[19]++;
        if (c == 'u')
            count_array[20]++;
        if (c == 'v')
            count_array[21]++;
        if (c == 'w')
            count_array[22]++;
        if (c == 'x')
            count_array[23]++;
        if (c == 'y')
            count_array[24]++;
        if (c == 'z')
            count_array[25]++;
    }

    int j = 0;
    char* alphabet = "abcdefghijklmnopqrstuvwxyz";
    char* temp = alphabet;
    for (int k = 0; k < 26; k++)
    {
        char c = *temp;
        temp++;
        printf("%c: ", c);
        while (j < count_array[k])
        {
            printf("*");
            j++;
        }
        j = 0;
        printf("\n");
    }

    return 0;
}
