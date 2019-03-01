#define NUMBER '0'      /* signal that a number was found */
#define NEG_NUMBER '1'  /* signal that a negative number was found */
#define MATH_WORD '2'   /* signal that a math operator is found */
#define VARIABLE '3'    /* signal that a variable is found */

int getop(char []);
void push(double);
double pop(void);
int getch(void);
void ungetch(int);
int size(void);
double peek(void);
void duplicate(void);
void swap(void);
void clear(void);
int is_variable(char);
double resolve_variable(char);

char math_word_buf[5];   /* longest math operator can be 'sqrt', plus space for null terminator. */
