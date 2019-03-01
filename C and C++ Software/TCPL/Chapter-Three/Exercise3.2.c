#include <stdio.h>
#define MAX 150

/* Exercise 3-2.
 *
 * Write a function escape(s,t) that converts characters like newline and tab
 * into visible escape sequences like \n and \t as it copies the string t to s. Use a switch. Write
 * a function for the other direction as well, converting escape sequences into the real characters.
 */

void escape(char* s, char* t);
void un_escape(char* s, char* t);

int main(int argc, char* argv[])
{
    char s[MAX], t[MAX], c;
    int i = 0;
    printf("Please enter a string to copy ...\n");
    while ((c = getchar()) != EOF)
    {
        t[i] = c;
        i++;
    }
    t[i] = '\0';
    escape(s, t);
    printf("escape(s, t):   \t%s\n", s);
    un_escape(s, t);
    printf("un_escape(s, t):   \t%s", s);
    return 0;
}

void escape(char* s, char* t)
{
    char* temp = t, c;

    while ((c = *temp) != '\0')
    {
        switch (c)
        {
            case '\n':
                *s = '\\';
                *(++s) = 'n';
                break;
            case '\t':
                *s = '\\';
                *(++s) = 't';
                break;
            default:
                *s = *temp;
                break;
        }
        temp++;
        s++;
    }
    *s = '\0';
}

void un_escape(char* s, char* t)
{
    char* temp = t, c;

    while ((c = *temp) != '\0')
    {
        switch (c)
        {
            case '\\':
                c = *(++temp);
                switch (c)
                {
                    case 'n':
                        *s = '\n';
                        break;
                    case 't':
                        *s = '\t';
                        break;
                    default:
                        break;
                }
            default:
                *s = *temp;
                break;
        }
        temp++;
        s++;
    }
    *s = '\0';
}
