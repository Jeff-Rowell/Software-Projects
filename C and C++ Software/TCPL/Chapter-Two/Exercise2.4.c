#include <stdio.h>
#define MAX_STRING 50

/* Exercise 2-4.
 *
 * Write an alternative version of squeeze(s1,s2) that deletes each character in
 * s1 that matches any character in the string s2.
 */

void squeeze(char* s1, char* s2);

int main(int argc, char* argv[])
{
    char s1[MAX_STRING], s2[MAX_STRING], c;
    int i = 0;
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

    squeeze(s1, s2);
    printf("Resulting string: %s\n", s1);
    return 0;
}

void squeeze(char* s1, char* s2)
{
    char new_str[MAX_STRING];
    int flag = 0, j = 0, i, k;
    char *temp1 = s1, *temp2 = s2;

    for (i = 0; temp1[i] != '\0'; i++)
    {
        for (k = 0; temp2[k] != '\0'; k++)
        {
            if(temp1[i] == temp2[k])
            {
                flag = 1;
            }
        }
        if (!flag)
        {
            new_str[j] = temp1[i];
            j++;
        }
        flag = 0;
    }
    new_str[j] = '\0';

    for (i = 0; new_str[i] != '\0'; i++)
        s1[i] = new_str[i];
    s1[i] = '\0';
}
