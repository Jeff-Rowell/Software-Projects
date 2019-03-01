#include <stdio.h>
#include <string.h>

/* Exercise 4-13.
 *
 * Write a recursive version of the function reverse(s), which reverses the
 * string s in place.
 */

void reverse(char []);

int main(int argc, char* argv[])
{
    char s1[] = "!dlrow olleH", s2[] = "Race car", s3[] = "123456789";
    reverse(s1);
    printf("%s\n", s1);
    reverse(s2);
    printf("%s\n", s2);
    reverse(s3);
    printf("%s\n", s3);
    return 0;
}

void reverse(char s[])
{
    int c, i, length = strlen(s);
    if (length)
    {
        reverse(s + 1);
        c = s[0];
        for (i = 1; i < length; i++)
            s[i - 1] = s[i];
        s[i - 1] = c;
    }

}