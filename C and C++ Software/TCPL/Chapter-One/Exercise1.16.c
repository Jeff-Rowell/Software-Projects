#include <stdio.h>
#define MAX_LENGTH 1000 /* the maximum length of a line */

/* Exercise 1-16.
 *
 * Revise the main routine of the longest-line program so it will correctly print
 * the length of arbitrary long input lines, and as much as possible of the text.
 */

int getlinelength(char line[], int maxline);
void copy(char to[], char from[]);

/* prints the longest input line */
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



