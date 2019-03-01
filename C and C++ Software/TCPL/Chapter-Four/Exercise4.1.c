#include <stdio.h>
#include <string.h>

/*
 * Exercise 4-1.
 * 
 * Write the function strindex(s,t) which returns the position of the rightmost
 * occurrence of t in s, or -1 if there is none.
 */

int strindex(char* s, char t);

int main(int argc, char* argv[])
{
    char* s = "Hello World.", t = 'l';
    printf("Calling strindex with \"%s\" and '%c' ...\nResult index = %d\n", s, t, strindex(s, t));
    return 0;
}

int strindex(char* s, char t)
{
    int result = -1, len = strlen(s);
    printf("len = %d\n", len);

    for (int i = len; i >= 0; i--)
    {
        if (s[i] == t)
        {
            result = i;
            break;
        }
    }

    return result;
}