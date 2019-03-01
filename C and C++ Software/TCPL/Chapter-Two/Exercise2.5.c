#include <stdio.h>
#define MAX_STRING 50

/* Exercise 2-5.
 *
 * Write the function any(s1,s2), which returns the first location in a string s1
 * where any character from the string s2 occurs, or -1 if s1 contains no characters from s2.
 * (The standard library function strpbrk does the same job but returns a pointer to the
 * location.)
 */

int any(char* s1, char* s2);

int main(int argc, char* argv[])
{
    char s1[MAX_STRING], s2[MAX_STRING], c;
    int i = 0, result;
    printf("Enter the first string ending with a newline: ");
    while ((c = getchar()) != EOF)
    {
        s1[i] = c;
        i++;
        while ((c = getchar()) != '\n')
        {
            s1[i] = c;
            i++;
        }
        s1[i] = '\0';

        i = 0;
        printf("Enter the second string ending with a newline: ");
        while ((c = getchar()) != '\n')
        {
            s2[i] = c;
            i++;
        }
        s2[i] = '\0';
        break;
    }

    result = any(s1, s2);
    printf("Location: %d\n", result);
    return result;
}

int any(char* s1, char* s2)
{
    int index = -1;
    char *temp1 = s1, *temp2 = s2;

    for (int i = 0; temp1[i] != '\0'; i++)
    {
        for (int k = 0; temp2[k] != '\0'; k++)
        {
            if (temp1[i] == temp2[k])
            {
                return i;
            }
        }
    }
    return index;
}
