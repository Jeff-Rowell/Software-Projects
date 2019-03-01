#include <stdio.h>

/* Exercise 5-8.
 *
 * There is no error checking in day_of_year or month_day. Remedy this defect.
 */

int day_of_year(int, int, int);
void month_day(int, int, int*, int*);
char *month_name(int);

int main(int argc, char* argv[])
{
    int *m, *d, month = 5, day = 7, year = 1988;
    m = &month;
    d = &day;
    printf("Calling day_of_year(%d, %d, %d); => %d\n", year, month, day, day_of_year(year, month, day));
    month_day(year, 60, m, d);
    printf("Calling month_day(%d, 60, %d, %d);\nMonth: %s\nDay: %d\n", year, month, day, month_name(*m), *d);
    printf("%d/%d/%d is the %dth day of the year\n", *m, *d, year, day_of_year(year, *m, *d));
    return 0;
}

/* Note: we use 0 in first column so we can access the months with their natural 1-12 indexing */
static char daytab[2][13] =
        {{0, 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31},
         {0, 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31}};

int day_of_year(int year, int month, int day)
{
    int i, leap;

    if (year < 1)
    {
        printf("----------------------\nError: invalid year\n----------------------\n");
        return -1;
    }

    leap = ( (year % 4 == 0) && (year % 100 != 0) ) || (year % 400 == 0);

    if (day < 1 || day > daytab[leap][month])
    {
        printf("----------------------\nError: invalid day\n----------------------\n");
        return -1;
    }
    if (month < 1 || month > 12)
    {
        printf("----------------------\nError: invalid month\n----------------------\n");
        return -1;
    }

    for (i = 0; i < month; i++)
    {
        day += daytab[leap][i];
    }

    return day;
}

void month_day(int year, int yearday, int *pmonth, int *pday)
{
    int i, leap;

    if (year < 1)
    {
        *pmonth = -1;
        *pday = -1;
        return;
    }

    leap = ( (year % 4 == 0) && (year % 100 != 0) ) || (year % 400 == 0);

    for (i = 1; i <= 12 && yearday > daytab[leap][i]; i++)
    {
        yearday -= daytab[leap][i];
    }

    if (i > 12 && yearday > daytab[leap][12])
    {
        *pmonth = -1;
        *pday = -1;
    }
    else
    {
        *pmonth = i;
        *pday = yearday;
    }
}

char *month_name(int month)
{
    static char *name[] = {
            "Illegal Month", "January",
            "February", "March", "April",
            "May", "June", "July",
            "August", "September", "October",
            "November", "December"
    };

    return (month < 1 || month > 12) ? name[0] : name[month];
}
