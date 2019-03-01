#include <stdio.h>
#include <stdlib.h>

#define MAXSIZE 100

/* Exercise 5-4.
 *
 * Write the function strend(s,t), which returns 1 if the string t occurs at the
 * end of the string s, and zero otherwise.
 */

int string_end(char *s, char *t);

int main(int argc, char* argv[])
{
    char str1[MAXSIZE] = "Hello World";
    char str2[MAXSIZE] = "World";
    printf("str1 = %s\nstr2 = %s\nResult: %d\n\n", str1, str2, string_end(str1, str2));

    char str3[MAXSIZE] = "Not in this string!";
    char str4[MAXSIZE] = "Hello?";
    printf("str3 = %s\nstr4 = %s\nResult: %d\n", str3 ,str4, string_end(str3, str4));
}

int string_end(char *s, char *t)
{
    int t_len = 0, s_len = 0;
    char *s_temp;
    s_temp = s;

    while (*t++)
        t_len++;
    while (*s++)
        s_len++;

    s = s_temp;
    s += (s_len - t_len);
    t -= t_len + 1;

    while (*t)
    {
        if (*s != *t)
        {
            return 0;
        }
        s++;
        t++;
    }

    return 1;
}
