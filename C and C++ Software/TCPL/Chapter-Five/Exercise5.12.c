#include <stdio.h>
#include <stdlib.h>

#define TABSTOP 8

/* Exercise 5-12.
 *
 * Extend entab and detab to accept the shorthand:
 *
 * entab -m +n
 *
 * to mean tab stops every n columns, starting at column m. Choose convenient (for the user)
 * default behavior.
 */

void printBlanks(int);
void entab(int, int);
void detab(int, int);

int main(int argc, char *argv[])
{
    char *start_str, *stop_str;
	int start, stop;
	if (argc == 1)
	{
		printf("Calling entab() ...\n");
		entab(0, TABSTOP);
		printf("\nCalling detab() ...\n");
		detab(0, TABSTOP);
	}
	else if (argc == 3)
	{
	    start_str = *(++argv);
        start_str++;
        stop_str = *(++argv);
        stop_str++;

        start = atoi(start_str);
        stop = atoi(stop_str);
        printf("Calling entab() ...\n");
        entab(start, stop);
        printf("\nCalling detab() ...\n");
        detab(start, stop);

	}
	else
	{
	    printf("Invalid number of arguments given.\nPlease enter the starting and stopping columns for tabs,\n"
               "such as `-m' for the starting column and `+n' for tab stops every `n' columns.");
	}
	printf("\n");
	return 0;
}

void entab(int start, int tabstop)
{
    int c, blanks = 0;

    while ((blanks++) < start)
    {
        putchar(getchar());
    }

	while ((c = getchar()) != '\n')
	{
		if (c == ' ')
		{
			++blanks;
			while ((c = getchar()) == ' ')
			{
				++blanks;
				if (blanks == tabstop)
				{
					putchar('\t');
					blanks = 0;
				}
			}
			printBlanks(blanks);
			if (c != ' ')
			{
				putchar(c);
			}
		}
		else
		{
			putchar(c);
		}
	}
}

void detab(int start, int tabstop)
{
    int c, i, column = 0;

    while ((column++) < start)
    {
        putchar(getchar());
    }
    while ((c = getchar()) != '\n')
    {
	if (c == '\t')
	{
		for (i = 0; i < tabstop - column % tabstop; ++i)
		{
			putchar(' ');
		}
		column += tabstop - column % tabstop;
	}
	else if (c == '\n')
	{
		putchar(c);
		column = 0;
	}
	else
	{
		putchar(c);
		column++;
	}
    }
}

void printBlanks(int num_blanks)
{
	int i;

	for (i = 0; i < num_blanks; ++i)
	{
		putchar(' ');
	}
}
