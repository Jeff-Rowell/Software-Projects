#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>

#define MAXLEN 1000
#define MAXLINES 500000
#define MAXSTOR 500000
#define ALLOCSIZE 10000 /* size of available space */

/* Exercise 5-7.
 *
 * Rewrite readlines to store lines in an array supplied by main, rather than
 * calling alloc to maintain storage. How much faster is the program?
 *
 * => readlines without alloc() is about 0.000026 seconds or about .5% faster with
 *    a large amount of input. With small number of input lines they are roughly
 *    the same.
 */

int getlinelength(char*, int);
int readlines(char*[], char*, int);
void writelines(char*[], int);
void swap(char*[], int, int);
void quick_sort(char*[], int, int);
int readlines2(char*[], int);
char *alloc(int);
int get_line(char[], int);


char *lineptr[MAXLINES];
static char allocbuf[ALLOCSIZE]; /* storage for alloc */
static char *allocp = allocbuf;  /* next free position */

int main(int argc, char* argv[])
{
    int nlines;
    char *linestore = (char*)malloc(0);
    clock_t start, finish;
    double elapsed;

    start = clock();
    if ((nlines = readlines(lineptr, linestore, MAXLINES)) >= 0)
    {
        quick_sort(lineptr, 0, nlines - 1);
        writelines(lineptr, nlines);
        finish = clock();
        elapsed = ((double) (finish - start)) / CLOCKS_PER_SEC;
        printf("Time Elapsed: %f seconds\n\n", elapsed);
    }
    else
    {
        printf("Error: input too big to sort\n\n");
    }

    start = clock();
    if ((nlines = readlines2(lineptr, MAXLINES)) >= 0)
    {
        quick_sort(lineptr, 0, nlines-1);
        writelines(lineptr, nlines);
        finish = clock();
        elapsed = ((double) (finish - start)) / CLOCKS_PER_SEC;
        printf("Time Elapsed using alloc(): %f seconds\n", elapsed);
    }
    else
        printf("error: input too big to sort\n");

    return 0;
}

int getlinelength(char *s, int lim)
{
    int c, i;
    char *p = s;

    for (i = 0; i < lim && ((c = getchar()) != EOF && c != '\n'); i++, s++)
    {
        *s = c;
    }

    if (c == '\n')
    {
        *s = c;
        ++i;
        ++s;
    }
    *s = '\0';
    return i;
}

int readlines(char *lineptr[], char *linestore, int maxlines)
{
    int len, nlines;
    char line[MAXLEN];
    char *p = linestore;
    char *linestop = linestore + MAXSTOR;

    nlines = 0;
    while ((len = getlinelength(line, MAXLEN)) > 1)
    {
        if (nlines >= maxlines || (p + len) > linestop)
        {
            return -1;
        }
        else
        {
            line[len - 1] = '\0';
            strcpy(p, line);
            lineptr[nlines++] = p;
            p += len;
        }
    }
    return nlines;
}

/* readlines2: read input lines */
int readlines2(char *lineptr[], int maxlines)
{
    int len, nlines;
    char *p, line[MAXLEN];
    nlines = 0;
    while ((len = get_line(line, MAXLEN)) > 0)
        if (nlines >= maxlines || (p = alloc(len)) == NULL)
            return -1;
        else
        {
            line[len-1] = '\0'; /* delete newline */
            strcpy(p, line);
            lineptr[nlines++] = p;
        }
    return nlines;
}

void writelines(char *lineptr[], int nlines)
{
    while (nlines-- > 0)
    {
        printf("%s\n", *lineptr++);
    }
}

void swap(char *v[], int i, int j)
{
    char *temp;

    temp = v[i];
    v[i] = v[j];
    v[j] = temp;
}

void quick_sort(char *v[], int left, int right)
{
    int i, last;

    void swap(char *v[], int i, int j);

    if (left > right)
    {
        return;
    }

    swap(v, left, (left + right) / 2);
    last = left;
    for (i = left + 1; i <= right; i++)
    {
        if (strcmp(v[i], v[left]) < 0)
        {
            swap(v, ++last, i);
        }
    }
    swap(v, left, last);
    quick_sort(v, left, last - 1);
    quick_sort(v, last + 1, right);
}

char *alloc(int n) /* return pointer to n characters */
{
    if (allocbuf + ALLOCSIZE - allocp >= n) /* it fits */
    {
        allocp += n;
        return allocp - n; /* old p */
    }
    else /* not enough room */
        return 0;
}

/* getline: get line into s, return length */
int get_line(char s[], int lim)
{
    int c, i;
    i = 0;
    while (--lim > 0 && (c=getchar()) != EOF && c != '\n')
        s[i++] = c;
    if (c == '\n')
        s[i++] = c;
    s[i] = '\0';
    return i;
}

//#include <stdio.h>
//#include <string.h>
//#define MAXLINES 5000 /* max #lines to be sorted */
//char *lineptr[MAXLINES]; /* pointers to text lines */
//int readlines(char *lineptr[], int nlines);
//void writelines(char *lineptr[], int nlines);
//void qsort(char *lineptr[], int left, int right);
//
///* sort input lines */
//int main()
//{
//    int nlines; /* number of input lines read */
//    double elapsed;
//    clock_t start, finish;
//    start = clock();
//    if ((nlines = readlines(lineptr, MAXLINES)) >= 0) {
//        qsort(lineptr, 0, nlines-1);
//        writelines(lineptr, nlines);
//        finish = clock();
//        elapsed = ((double) (finish - start)) / CLOCKS_PER_SEC;
//        printf("%f seconds elapsed\n", elapsed);
//        return 0;
//    } else {
//        printf("error: input too big to sort\n");
//        return 1;
//    }
//}
//
//#define MAXLEN 1000 /* max length of any input line */
//int get_line(char *, int);
//char *alloc(int);
//
///* readlines: read input lines */
//int readlines(char *lineptr[], int maxlines)
//{
//    int len, nlines;
//    char *p, line[MAXLEN];
//    nlines = 0;
//    while ((len = get_line(line, MAXLEN)) > 0)
//        if (nlines >= maxlines || (p = alloc(len)) == NULL)
//            return -1;
//        else {
//            line[len-1] = '\0'; /* delete newline */
//            strcpy(p, line);
//            lineptr[nlines++] = p;
//        }
//    return nlines;
//}
//
///* writelines: write output lines */
//void writelines(char *lineptr[], int nlines)
//{
//    while (nlines-- > 0)
//        printf("%s\n", *lineptr++);
//}
//
///* qsort: sort v[left]...v[right] into increasing order */
//void qsort(char *v[], int left, int right)
//{
//    int i, last;
//    void swap(char *v[], int i, int j);
//    if (left >= right) /* do nothing if array contains */
//        return; /* fewer than two elements */
//    swap(v, left, (left + right)/2);
//    last = left;
//    for (i = left+1; i <= right; i++)
//        if (strcmp(v[i], v[left]) < 0)
//            swap(v, ++last, i);
//    swap(v, left, last);
//    qsort(v, left, last-1);
//    qsort(v, last+1, right);
//}
//
///* swap: interchange v[i] and v[j] */
//void swap(char *v[], int i, int j)
//{
//    char *temp;
//    temp = v[i];
//    v[i] = v[j];
//    v[j] = temp;
//}
//
///* getline: get line into s, return length */
//int get_line(char s[], int lim)
//{
//    int c, i;
//    i = 0;
//    while (--lim > 0 && (c=getchar()) != EOF && c != '\n')
//        s[i++] = c;
//    if (c == '\n')
//        s[i++] = c;
//    s[i] = '\0';
//    return i;
//}
//
//#define ALLOCSIZE 10000 /* size of available space */
//static char allocbuf[ALLOCSIZE]; /* storage for alloc */
//static char *allocp = allocbuf; /* next free position */
//
//char *alloc(int n) /* return pointer to n characters */
//{
//    if (allocbuf + ALLOCSIZE - allocp >= n) { /* it fits */
//        allocp += n;
//        return allocp - n; /* old p */
//    } else /* not enough room */
//        return 0;
//}
