public class Pair
{
    private  int item1;
    private  int item2;

    public Pair(int item1, int item2)
    {
        this.item1 = item1;
        this.item2 = item2;
    }

    public int getItem1()
    {
        int returnItem = item1;
        return returnItem;
    }

    public int getItem2()
    {
        int returnItem = item2;
        return returnItem;
    }

    public boolean equals(Pair p1)
    {
        return (this.item1 == p1.item1 && this.item2 == p1.item2);
    }
}