#include <stdio.h>
#include <time.h>
#include <stdlib.h>
#define ARRAY_LENGTH 1000000
#define ITERS 7000000

/* Exercise 3-1.
 *
 * Our binary search makes two tests inside the loop, when one would suffice (at
 * the price of more tests outside.) Write a version with only one test inside the loop and
 * measure the difference in run-time.
 */

int bin_search1(int x, int v[], int n);
int bin_search2(int x, int v[], int n);

int main(int argc, char* argv[])
{
    int arr[ARRAY_LENGTH];
    int to_find = 777, result1, result2;
    clock_t start, diff1, diff2;

    for (int i = 0; i < ARRAY_LENGTH; i++)
        arr[i] = i;

    start = clock();
    for (int i = 0; i < ITERS; i++)
        result1 = bin_search1(to_find, arr, ARRAY_LENGTH);
    diff1 =  clock() - start;

    start = clock();
    for (int i = 0; i < ITERS; i++)
        result2 = bin_search2(to_find, arr, ARRAY_LENGTH);
    diff2 = clock() - start;

    printf("bin_search1:   %d\ttime: %.04f seconds\n", result1, ((double) diff1) / CLOCKS_PER_SEC);
    printf("bin_search2:   %d\ttime: %.04f seconds\n", result2, ((double) diff2) / CLOCKS_PER_SEC);

    to_find = 1000001;

    start = clock();
    for (int i = 0; i < ITERS; i++)
        result1 = bin_search1(to_find, arr, ARRAY_LENGTH);
    diff1 =  clock() - start;

    start = clock();
    for (int i = 0; i < ITERS; i++)
        result2 = bin_search2(to_find, arr, ARRAY_LENGTH);
    diff2 = clock() - start;

    printf("bin_search1:   %d\ttime: %.04f seconds\n", result1, ((double) diff1) / CLOCKS_PER_SEC);
    printf("bin_search2:   %d\ttime: %.04f seconds\n", result2, ((double) diff2) / CLOCKS_PER_SEC);

    return result1;
}

int bin_search1(int x, int v[], int n)
{
    int low, high, mid;
    low = 0;
    high = n - 1;

    while (low <= high)
    {
        mid = (low + high)/2;
        if (x < v[mid])
            high = mid + 1;
        else if (x > v[mid])
            low = mid + 1;
        else /* found match */
            return mid;
    }

    return -1; /* no match */
}

int bin_search2(int x, int v[], int n)
{
    int low, high, mid;
    low = 0;
    high = n - 1;
    mid = (low + high)/2;

    while (low <= high && v[mid] != x)
    {
        if (x < v[mid])
            high = mid + 1;
        else
            low = mid + 1;
        mid = (low + high)/2;
    }

    if (low > high) /* no match */
    {
        mid = -1;
    }

    return mid;
}
