#include <stdio.h>
#include <stdlib.h>

#define MAXSIZE 100

/* Exercise 5-3.
 *
 * Write a pointer version of the function strcat that we showed in Chapter 2:
 * strcat(s,t) copies the string t to the end of s.
 */

void string_concat(char *s, char *t);

int main(int argc, char* argv[])
{
    char str1[MAXSIZE] = "Hello ";
    char str2[MAXSIZE] = "World!";

    printf("str1: %s\n\n", str1);
    printf("str2: %s\n\n", str2);
    string_concat(str1, str2);
    printf("After Concat: %s\n\n", str1);
}

void string_concat(char *s, char *t)
{
    while (*++s)
        ;
    while ((*s++ = *t++))
        ;
}
