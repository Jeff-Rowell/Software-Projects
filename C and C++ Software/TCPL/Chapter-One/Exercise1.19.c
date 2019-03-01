#include <stdio.h>
#define MAX_LENGTH 1000 /* the maximum length of a line */

/* Exercise 1-19.
 *
 * Write a function reverse(s) that reverses the character string s. Use it to
 * write a program that reverses its input a line at a time.
 */

void reverseLine(char forward[]);
int getlinelength(char s[], int lim);

/* prints the longest input line, and prints each input that is greater than 80 characters in length */
int main(int argc, char* argv[])
{

    int len; /* the current length of the line */

    char line[MAX_LENGTH];     /* the current line */

    while ((len = getlinelength(line, MAX_LENGTH)) > 0)
    {
        reverseLine(line);
        printf("Reversed Line: %s\n", line);
    }

    return 0;
}

/* getlinelength: reads a line into s, and returns its length. This also removes any trailing blanks and tabs,
 *                and removes blank lines entirely.
 */
int getlinelength(char s[], int lim)
{
    int c,i;

    for (i = 0; i < lim && ( (c = getchar()) != EOF && c != '\n'); ++i)
    {
        s[i] = c;
    }

    if (c == '\n')
    {
        s[i] = c;
        ++i;
    }
    s[i] = '\0';
    return i;
}

/* reverse: takes a line of input, and reverses it. */
void reverseLine(char s[])
{
    int i, j;
    char temp;

    j = 0;
    for (i = 0; s[i] != '\0'; ++i)
    {
        if (s[i] == '\n')
        {
            j = i - 1;
        }
    }

    for (i = 0; i <= j; ++i)
    {
        temp = s[i];
        s[i] = s[j];
        s[j] = temp;
        --j;
    }
}





