#include <stdio.h>
#define MAX 1000

/* Exercise 3-3.
 *
 * Write a function expand(s1,s2) that expands shorthand notations like a-z in
 * the string s1 into the equivalent complete list abc...xyz in s2. Allow for letters of either
 * case and digits, and be prepared to handle cases like a-b-c and a-z0-9 and -a-z. Arrange
 * that a leading or trailing - is taken literally.
 */

void expand(char* s1, char* s2);

int main(int argc, char* argv[])
{
    char s2[MAX], *tests[] = {"a-z-", "a-b-c", "A-z", "a-z0-9", "-a-Z", "-", "--",
                              "-@-c-", "z-a", "q-a-q", "z-a-c", "-m-m", "m-m-m-"};

    for (int i = 0; i < (sizeof(tests) / sizeof(*tests)); i++)
    {
        expand(tests[i], s2);
        printf("%s \t\t> \t%s\n", tests[i], s2);
    }

    return 0;
}

void expand(char* s1, char* s2)
{
    char current, next;
    int i, k;

    for (i = 0; (current = s1[i]) != '\0'; i++)
    {
        next = s1[i+1];
        if (current == '-' && i == 0)
        {
            *(s2++) = '-';
        }
        if (next == '-')
        {
            if ((next = s1[i + 2]) != '\0')
            {
                if (current < next)
                {
                    k = 0;
                    while (current + k <= next)
                    {
                        if (*(s2 - 1) != (current + k))
                        {
                            *(s2++) = current + k++;
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
                else
                {
                    k = 0;
                    while (current - k >= next)
                    {
                        if (*(s2 - 1) != (current + k))
                        {
                            *(s2++) = current - k++;
                        }
                        else
                        {
                            k++;
                        }
                    }
                }
            }
        }
    }
    if (i > 1 && s1[i-1] == '-')
    {
        *(s2++) = '-';
    }
    *s2 = '\0';
}
