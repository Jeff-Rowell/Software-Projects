#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define LAST 10
#define MAX_LINES 10000
#define MAX_LINE_LEN 80

/*
 * Exercise 5-13.c
 *
 * Write the program tail, which prints the last n lines of its input. By default, n
 * is set to 10, let us say, but it can be changed by an optional argument so that
 *
 * tail -n
 *
 * prints the last n lines. The program should behave rationally no matter how unreasonable the
 * input or the value of n. Write the program so it makes the best use of available storage; lines
 * should be stored as in the sorting program of Section 5.6, not in a two-dimensional array of
 * fixed size.
 *
 */

void tail(int, char*);

int main(int argc, char *argv[])
{
    char *arg1, *arg2;
    int n;

    if (argc < 2)
    {
        printf("Error. Need filename, or filename and number of last lines to print.\n");
        exit(1);
    }
    else if (argc > 2)
    {
        arg1 = *(++argv);
        arg2 = *(++argv);
        if (arg1[0] == '-')
        {
            n = atoi(++arg1);
            tail(n, arg2);
        }
        else
        {
            n = atoi(++arg2);
            tail(n, arg1);
        }
    }
    else
    {
        arg1 = *(++argv);
        if (arg1[0] == '-')
        {
            printf("Error. Need filename, or filename and number of last lines to print.\n");
            exit(1);
        }
        tail(n, arg1);
    }
    return 0;
}

void tail(int n, char *filename)
{
    FILE *fp;
    if (!(fp = fopen(filename, "r")))
    {
        printf("Error. File \"%s\" not found.\n", filename);
        exit(1);
    }
    char temp_buf[MAX_LINES][MAX_LINE_LEN], ins[MAX_LINE_LEN];
    int num_lines = 0, start;

    while (fgets(ins, MAX_LINE_LEN, fp))
    {
        strcpy(temp_buf[num_lines++], ins);
    }
    fclose(fp);
    if (n > num_lines)
    {
        printf("Printing the last %d lines of %s\n\n", num_lines, filename);
        for (int j = 0; j < num_lines; j++)
            printf("%s", temp_buf[j]);
        return;
    }

    printf("Printing the last %d lines of %s\n\n", n, filename);
    start = num_lines - n;
    for (; start < num_lines; start++)
        printf("%s", temp_buf[start]);
    return;
}
