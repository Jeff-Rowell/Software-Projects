#include <stdio.h>

void *malloc(unsigned);
void free(void *);
void *calloc(int, int);
void bfree(void *, unsigned);

static void printhex(void *p, size_t len);
static void fillarray(void *p, size_t len);


/*
 * Exercise 8-6, 7, and 8.
 *
 * The standard library function calloc(n,size) returns a pointer to n objects of
 * size size, with the storage initialized to zero. Write calloc, by calling malloc or by
 * modifying it. malloc accepts a size request without checking its plausibility; free believes
 * that the block it is asked to free contains a valid size field. Improve these routines so they
 * make more pains with error checking. Write a routine bfree(p,n) that will free any arbitrary block
 * p of n characters into the free list maintained by malloc and free. By using bfree, a user can add
 * a static or external array to the free list at any time.
 */

/* must be greater than NALLOC * sizeof(Header) for the test to work */
#define STATIC_ARRAY_SIZE 10000U
#define MALLOC_SIZE 50U
#define CALLOC_SIZE 10U

main(int argc, char *argv[])
{
	static char array[STATIC_ARRAY_SIZE];
	void *p1;
	int *p2;

	printf("p1 = malloc(%u)\n", MALLOC_SIZE);
	p1 = malloc(MALLOC_SIZE);
	printf("p1 == %p\n", p1);
	if (p1 != NULL) {
		printf("Contents of p1: ");
		printhex(p1, MALLOC_SIZE);
		printf("\n");
	}
	printf("p2 = calloc(%u, %u)\n", sizeof(*p2), CALLOC_SIZE);
	p2 = calloc(sizeof(*p2), CALLOC_SIZE);
	printf("p2 == %p\n", p2);
	if (p2 != NULL) {
		printf("Contents of p2: ");
		printhex(p2, sizeof(*p2) * CALLOC_SIZE);
		printf("\n");
	}
	printf("free(p1)\n");
	free(p1);
	printf("fillarray(%p, %u)\n", array, sizeof(array));
	fillarray(array, sizeof(array));
	printf("Contents of %p: ", array);
	printhex(array, 100);
	printf("...\n");
	printf("bfree(%p, %u)\n", array, sizeof(array));
	bfree(array, sizeof(array));
	/* leave room for the header */
	printf("p1 = malloc(%u)\n", STATIC_ARRAY_SIZE - 100);
	p1 = malloc(STATIC_ARRAY_SIZE - 100);
	printf("p1 == %p (should be inside the block freed by bfree)\n", p1);
	if (p1 != NULL) {
		printf("Contents of p1: ");
		printhex(p1, 100);
		printf("...\n");
	}
	printf("free(p1)\n");
	free(p1);
	printf("free(p2)\n");
	free(p2);
	printf("free(NULL)\n");
	free(NULL);

	return 0;
}

static void printhex(void *p, size_t len)
{
	char *cp;

	cp = (char *) p;
	while (len-- > 0)
		printf("%02X", *cp++);

	return;
}

static void fillarray(void *p, size_t len)
{
	int *ip;

	len /= sizeof(int);
	ip = (int *) p;
	while (len-- > 0)
		*ip++ = 0x12345678;
}
