#include <stdio.h>
#include <ctype.h>
#include <string.h>
#include "calc.h"

/* getop: get next character or numeric operand */
int getop(char s[])
{
    int i, c, temp;
    while ((s[0] = c = getch()) == ' ' || c == '\t')
        ;
    s[1] = '\0';
    if (!isdigit(c) && c != '.')
    {
        if (c == '\n')
            return c;
        if (isdigit(temp = getch()))
        {
            s[0] = temp;
            i = 0;/* collect integer part */
            while (isdigit(s[++i] = c = getch()))
                ;
            if (c == '.') /* collect fraction part */
                while (isdigit(s[++i] = c = getch()))
                    ;
            s[i] = '\0';
            return NEG_NUMBER;
        }
        else
        {
            if (c == '-' && is_variable(temp))
            {
                s[0] = temp;
                return NEG_NUMBER;
            }
            else if (c == '+' || c == '-' || c == '*' || c == '/')
            {
                ungetch(temp);
                return c; /* not a number */
            }
            if (temp == ' ' || temp == '\t')
            {
                s[0] = c;
                return VARIABLE; /* then c is the variable. */
            }
            math_word_buf[0] = c;
            math_word_buf[1] = temp;
            int l = 2;
            while (( c = getch()) != ' ' && c != '\t')
            {
                if (c == '\n')
                {
                    ungetch(c);
                    break;
                }
                math_word_buf[l++] = c;
            }
            math_word_buf[l] = '\0';
            l = 0;
            while (math_word_buf[l])
            {
                math_word_buf[l] = tolower(math_word_buf[l]);
                l++;
            }
            if (strcmp(math_word_buf, "sin") == 0 || strcmp(math_word_buf, "cos") == 0 ||
                strcmp(math_word_buf, "exp") == 0 || strcmp(math_word_buf, "pow") == 0 ||
                strcmp(math_word_buf, "sqrt") == 0)
            {
                return MATH_WORD;
            }
        }
    }
    i = 0;
    if (isdigit(c)) /* collect integer part */
        while (isdigit(s[++i] = c = getch()))
            ;
    if (c == '.') /* collect fraction part */
        while (isdigit(s[++i] = c = getch()))
            ;
    s[i] = '\0';
    if (c != EOF)
        ungetch(c);
    return NUMBER;
}