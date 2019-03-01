#include <stdio.h>
#include <string.h>

#define MAXOP 100       /* max size of operand or operator */
#define BUFSIZE 10

/* Exercise 4-7
 *
 * Write a routine ungets(s) that will push back an entire string onto the input.
 * Should ungets know about buf and bufp, or should it just use ungetch?
 *
 * => I decided that ungets() should know about bufp to first check the length of the string. If
 *    ungets() must rely only on ungetch(), then we will only know if we have enough space once we
 *    copy over all but the last character, which is inefficient. For this reason, I have ungets(s) get
 *    the length of the string first and then compare against BUFFSIZE to verify if there is space for the
 *    entire string before copying anything, but ungets() will still use ungetch() to do the actual copying.
 *    In my opinion I think it is better to still use ungetch() to do all of the copying and its own additional
 *    checks in case I want to change the underlying implementation to anything other than I stack, I will only
 *    need to make changes in ungetch(), rather than having to change both ungetch() and ungets().
 */

int getch(void);
void ungetch(int);
void ungets(char*);

char buf[BUFSIZE];              /* buffer for ungetch */
int bufp = 0;                   /* next free position in buf */

int main(int argc, char* argv[])
{

    printf("BUFFSIZE = %d\nCalling ungets(\"This is a test.\"); => ", BUFSIZE);
    ungets("This is a test.");
    if (bufp > 0)
        printf("buf = %s\n", buf);
    printf("Calling ungets(\"Test.\"); => ");
    ungets("Test.");
    if (bufp > 0)
        printf("buf = %s\n", buf);

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
        printf("ungetch: too many characters\n");
    else
        buf[bufp++] = c;
}

/* push an entire string back onto the input */
void ungets(char* s)
{
    int size = strlen(s);
    if (BUFSIZE - bufp >= size)
        while (size > 0)
            ungetch(s[--size]);
    else
        printf("ungets: too many characters\n");
}
