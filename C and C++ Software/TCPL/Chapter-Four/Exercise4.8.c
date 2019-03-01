#include <stdio.h>

#define MAXOP 100       /* max size of operand or operator */
#define BUFSIZE 1       /* change to 1 to limit getch and ungetch to only pushback at most one character */

/* Exercise 4-8
 *
 * Suppose that there will never be more than one character of pushback.
 * Modify getch and ungetch accordingly.
 *
 * => Modify BUFSIZE so that getch() and ungetch() can remain the same.
 */

int getch(void);
void ungetch(int);
void ungets(char*);

char buf[BUFSIZE];              /* buffer for ungetch */
int bufp = 0;                   /* next free position in buf */

int main(int argc, char* argv[])
{
    char c;
    printf("BUFFSIZE = %d\nbufp = %d\nCalling getch();\nPlease enter a character: ", BUFSIZE, bufp);
    c = getch();
    printf("bufp = %d\nCalling ungetch(%c);\n", bufp, c);
    ungetch(c);
    printf("bufp = %d\nCalling ungetch(%c);\n", bufp, c);
    ungetch(c);
    printf("bufp = %d\n", bufp);

    return 0;
}

/* get a (possibly pushed-back) character */
int getch(void)
{
    return (bufp > 0) ? buf[--bufp] : getchar();
}

/* push character back on input */
void ungetch(int c)
{
    if (bufp >= BUFSIZE)
        printf("ungetch: more than one character attempted to be pushed back\n");
    else
        buf[bufp++] = c;
}
