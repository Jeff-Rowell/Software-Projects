#include <stdio.h>
#define MAX_LENGTH 1000 /* the maximum length of a line */

/* Exercise 1-18.
 *
 * Write a program to remove trailing blanks and tabs from each line of input,
 * and to delete entirely blank lines.
 */

int getlinelength(char line[], int maxline);
int remove_blanks_tabs(char line[]);

/* prints the longest input line, removing trailing blanks, tabs, and deletes blank lines entirely*/
int main(int argc, char* argv[])
{
    int len; /* the current length of the line */
    char line[MAX_LENGTH];     /* the current line */

    while (getlinelength(line, MAX_LENGTH) > 0)
    {
        if (remove_blanks_tabs(line) > 0)
        {
            printf("String: %s\n", line);
        }
    }

    return 0;
}

int remove_blanks_tabs(char s[])
{
    int i = 0;

    while (s[i] != '\n') // find the end of the string
    {
        ++i;
    }
    --i; // start searching before the newline character, Note: strings are stored as [H,e,l,l,o,\n,\0]

    while (i >= 0 && (s[i] == '\t' || s[i] == ' '))
    {
        --i; // skip past all of the blanks and tabs
    }

    if (i >= 0) // check that we do not have an empty line
    {
        ++i;
        s[i] = '\n'; // shifts newline to where the first blank/tab was
        ++i;
        s[i] = '\0'; // shifts terminating character one space after newline
    }
    return i;
}

/* getlinelength: reads a line into s, and returns its length. This also removes any trailing blanks and tabs,
 *                and removes blank lines entirely.
 */
int getlinelength(char s[], int lim)
{
    int c, i;

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






