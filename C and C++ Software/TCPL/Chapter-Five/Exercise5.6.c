#include <stdio.h>
#include <stdlib.h>
#include <ctype.h>
#include <string.h>

#define MAX_LENGTH 1000
#define BUFSIZE 1000
#define NUMBER '0'

/* Exercise 5-6.
 *
 * Rewrite appropriate programs from earlier chapters and exercises with pointers
 * instead of array indexing. Good possibilities include getline (Chapters 1 and 4), atoi, itoa,
 * and their variants (Chapters 2, 3, and 4), reverse (Chapter 3), and strindex and getop
 * (Chapter 4).
 */

void get_line(char*);
int a_to_i(char*);
void itoa(int, char*);
void reverse(char*);
int strindex(char*, char*);
int getop(char*);
void ungetch(int);
int getch(void);

char buf[BUFSIZE]; /* buffer for ungetch */
int bufp = 0;      /* next free position in buf */

int main(int argc, char* argv[])
{
    char *line = (char*)malloc(MAX_LENGTH * sizeof(char));
    get_line(line);
    printf("line = %s\n", line);

    int i = a_to_i("   -400012   ");
    printf("i = %d\n", i);
    i = a_to_i("123456");
    printf("i = %d\n", i);

    *line = '\0';
    itoa(321, line);
    printf("i = %d, line = %s\n", 321, line);

    *line = '\0';
    itoa(-5155, line);
    printf("i = %d, line = %s\n", -5155, line);

    *line = '\0';
    itoa(-12345, line);
    printf("i = %d, line = %s\n", -12345, line);

    printf("index of \"%s\" in \"%s\" is %d\n", "Hello", "World, Hello", strindex("World, Hello", "Hello"));
    printf("index of \"%s\" in \"%s\" is %d\n", "Not here!", "World, Hello", strindex("World, Hello", "Not here!"));

    char *s = (char*)malloc(BUFSIZE * sizeof(char));
    printf("please enter a reverse Polish statement with no more than 10 operators: ");

    i = 0;
    char *positions[10] = {"first", "second", "third", "fourth", "fifth",
                           "sixth", "seventh", "eighth", "ninth", "tenth"};
    while (getop(s) != '\n')
        printf("%s operator of statement was %c\n", positions[i++], *s);


    return 0;
}

/* get_line: get a line on input and store it in line */
void get_line(char *line)
{
    char c;

    while ((c = getchar()) != '\n')
        *(line++) = c;
}

/* atoi: convert s to integer */
int a_to_i(char *s)
{
    int n = 0, sign;
    while(isspace(*s)) /* skip white space */
        s++;
    sign = (*s == '-') ? -1 : 1;
    if (*s == '+' || *s == '-') /* skip sign */
        s++;
    while(isdigit(*s))
        n = 10 * n + (*(s++) - '0');
    return sign * n;
}

/* itoa: convert n to characters in s */
void itoa(int n, char *s)
{
    int sign;
    char *p = s; /* record a pointer to the start of the string */

    if ((sign = n) < 0) /* record sign */
        n = -n; /* make n positive */
    do { /* generate digits in reverse order */
        *s = n % 10 + '0'; /* get next digit */
        s++;
    } while ((n /= 10) > 0); /* delete it */
    if (sign < 0)
        *(s++) = '-';
    *s = '\0';
    reverse(p);
}

/* reverse: reverse string s in place */
void reverse(char *s)
{
    int c, j, i = 0;

    j = strlen(s) - 1;
    while (i < j)
    {
        c = *(s + i);
        *(s + i) = *(s + j);
        *(s + j) = c;
        j--;
        i++;
    }
}

/* strindex: return index of t in s, -1 if none */
int strindex(char *s, char *t)
{
    int i, j, k;
    for (i = 0; *(s + i) != '\0'; i++)
    {
        for (j=i, k=0; *(t + k) !='\0' && *(s + j) == *(t + k); j++, k++)
            ;
        if (k > 0 && *(t + k) == '\0')
            return i;
    }
    return -1;
}

/* getop: get next character or numeric operand */
int getop(char *s)
{
    int i, c;
    while ((*s = c = getch()) == ' ' || c == '\t')
        ;
    *(s + 1) = '\0';
    if (!isdigit(c) && c != '.')
        return c; /* not a number */
    i = 1;
    if (isdigit(c)) /* collect integer part */
        while (isdigit(*(s + i) = c = getch()))
            i++;
    i++;
    if (c == '.') /* collect fraction part */
        while (isdigit(*(s + i) = c = getch()))
            i++;
    *(s + i) = '\0';
    if (c != EOF)
        ungetch(c);
    return NUMBER;
}

/* getch: get a (possibly pushed-back) character */
int getch(void)
{
    return (bufp > 0) ? buf[--bufp] : getchar();
}

/* ungetch: push character back on input */
void ungetch(int c)
{
    if (bufp >= BUFSIZE)
        printf("ungetch: too many characters\n");
    else
        buf[bufp++] = c;
}
