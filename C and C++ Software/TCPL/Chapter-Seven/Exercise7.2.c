#include <ctype.h>
#include <stdio.h>
#include <string.h>

/*
 * Exercise 7-2.
 *
 * Write a program that will print arbitrary input in a sensible way. As a
 * minimum, it should print non-graphic characters in octal or hexadecimal according to local
 * custom, and break long text lines.
 */

void encode(void);

main()
{
	encode();

	return 0;
}

#define MAXCOLUMN 76
#define ungetchar(c) ungetc((c), stdin)

static int peekchar(void)
{
	int c;

	c = getchar();
	if (c != EOF)
		ungetchar(c);

	return c;
}

/* This encodes stdin to stdout using the quoted-printable content transfer
encoding described in RFC 2045. */
void encode(void)
{
	/* The longest any character can expand to is
	three characters plus '\0'. */
	char buffer[4];
	int c;
	int column, length;

	column = 0;
	while ((c = getchar()) != EOF) {
		if (c == '\n') {
			printf("\r\n");
			column = 0;
			continue;
		} else if (c == ' ' || c == '\t') {
			if (peekchar() != '\n')
				sprintf(buffer, "%c", c);
			else
				sprintf(buffer, "=%02X", c);
		} else if (isprint(c) && c != '=') {
			sprintf(buffer, "%c", c);
		} else {
			sprintf(buffer, "=%02X", c);
		}
		length = strlen(buffer);
		if (column + length > MAXCOLUMN) {
			printf("=\r\n");
			column = 0;
		} else if (column + length == MAXCOLUMN) {
			if (peekchar() != '\n') {
				printf("=\r\n");
				column = 0;
			}
		}
		printf("%s", buffer);
		column += length;
	}

	return;
}
