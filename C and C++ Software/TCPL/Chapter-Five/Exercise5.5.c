#include <stdio.h>

#define MAXSIZE 10000

/* Exercise 5-5.
 *
 * Write versions of the library functions strncpy, strncat, and strncmp, which
 * operate on at most the first n characters of their argument strings. For example,
 * strncpy(s,t,n) copies at most n characters of t to s. Full descriptions are in Appendix B.
 */

void string_n_copy(char *s, char *t, int n);
int string_n_compare(char *s, char *t, int n);
void string_n_concat(char *s, char *t, int n);

int main(int argc, char* argv[])
{
    char str1[MAXSIZE] = "";
    char str2[MAXSIZE] = "Hello World";
    char str3[MAXSIZE] = "Helo World";
    char str4[MAXSIZE] = "Hello World";
    char str5[MAXSIZE] = "Hello";
    char str6[MAXSIZE] = " World";
    int result, n = 5;

    string_n_copy(str1, str2, n);
    printf("After Copying the first %d characters of \"%s\": %s\n\n", n, str2, str1);

    result = string_n_compare(str3, str4, n);
    printf("After Comparing the first %d characters of \"%s\" and \"%s\", Result: %d\n\n", n, str3, str4, result);

    n = 3;
    result = string_n_compare(str3, str4, n);
    printf("After Comparing the first %d characters of \"%s\" and \"%s\", Result: %d\n\n", n, str3, str4, result);

    n = 6;
    string_n_concat(str5, str6, n);
    printf("After Concatenating the first %d characters of \"%s\" to the end of \"%s\": %s\n\n", n, str6, "Hello", str5);

    n = 3;
    string_n_concat(str5, str6, n);
    printf("After Concatenating the first %d characters of \"%s\" to the end of \"%s\": %s\n\n",
           n, str6, "Hello World", str5);
}

void string_n_copy(char *s, char *t, int n)
{
    int i = 0;

    while ((*s++ = *t++) && i < n)
        i++;
}

int string_n_compare(char *s, char *t, int n)
{
    int i = 0;

    while(*s && *t && i < n)
    {
        if (*s != *t)
        {
            return 0;
        }
        s++;
        t++;
        i++;
    }
    return 1;
}

void string_n_concat(char *s, char *t, int n)
{
    int i = 0;
    while (*++s)
        ;
    while (i < n && (*s++ = *t++))
        i++;
}
