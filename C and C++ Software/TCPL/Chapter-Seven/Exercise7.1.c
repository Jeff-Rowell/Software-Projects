#include <stdio.h>
#include <ctype.h>

/*
 * Exercise 7-1.
 *
 * Write a program that converts upper case to lower or lower case to upper,
 * depending on the name it is invoked with, as found in argv[0].
 */

static int null_transform(int c)
{
	return c;
}

static char *basename(char *path)
{
	char *p;

	for (p = path; *p != '\0'; p++) {
		if (*p == '/')
			path = p + 1;
	}

	return path;
}

main(int argc, char *argv[])
{
	int (*transform)(int);
	char *prog_name;
	int c;

	prog_name = basename(argv[0]);
	if (strcmp(prog_name, "utol") == 0)
		transform = tolower;
	else if (strcmp(prog_name, "ltou") == 0)
		transform = toupper;
	else
		transform = null_transform;
	while ((c = getchar()) != EOF)
		putchar(transform(c));
	return 0;
}
