#include <stdio.h>
#define MAX_LENGTH 1000 /* the maximum length of a line */

/* Exercise 1-17.
 *
 * Write a program to print all input lines that are longer than 80 characters.
 */

int getlinelength(char line[], int maxline);
void copy(char to[], char from[]);

/* prints the longest input line, and prints each input that is greater than 80 characters in length */
int main(int argc, char* argv[])
{

    int len; /* the current length of the line */
    int max; /* the maximum length seen thus far. */

    char line[MAX_LENGTH];     /* the current line */
    char longest[MAX_LENGTH];  /* the longest line saved */

    max = 0;
    while ((len = getlinelength(line, MAX_LENGTH)) > 0)
    {
        if (len > max)
        {
            max = len;
            copy(longest, line);
        }
        if (len > 80) /* then there was a line */
        {
            printf("\nInput Line: %s", longest);
            printf("Length of Line: %d\n", max - 1); /* we subtract 1 to exclude the newline character \n. */
        }
    }

    if (max > 0) /* then there was a line */
    {
        printf("Longest Line: %s", longest);
        printf("Length of Longest Line: %d\n", max - 1); /* we subtract 1 to exclude the newline character \n. */
    }

    return 0;
}

/* getlinelength: reads a line into s, and returns its length */
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

/* copy: copies 'from' into 'to'; we assume 'to' is big enough */
void copy(char to[], char from[])
{

    int i = 0;

    while ( (to[i] = from[i]) != '\0')
    {
        ++i;
    }
}





