import java.util.*;
import java.lang.*;
import java.io.*;


public class Dijkstra
{
    private final static String _FILE_NAME = "topo.txt";
    private static ArrayList<Tuple> RouterCostTuple;
    private static ArrayList<Integer> _N;
    private static ArrayList<Integer> _NPrime;
    private static ArrayList<Pair> _YPrime;
    private static int[] _D;
    private static int[] _P;
    private static int[][] _costMatrix;
    private static int _n;

    public static void main (String[] args)
    {
        RouterCostTuple = new ArrayList<>();

        Scanner input = new Scanner(System.in);
        System.out.println("Enter the number of routers in your network: ");
        _n = input.nextInt();
        _N = new ArrayList<>();
        while(_n < 2)
        {
            System.out.println("At least two routers are required to find the least cost path...\n");
            System.out.println("Please enter the number of routers in your network: ");
            _n = input.nextInt();
        }
        System.out.println("\nComputing the shortest path for your " + _n + " routers ...\n");
        processInput();
        buildCostMatrix();
        dijkstra();
    }

        private static int findMinDistance(Boolean previouslySeen[])
        {
            int min = Integer.MAX_VALUE, min_index = -1;

            for (int v = 0; v < _n; v++)
            {
                if (previouslySeen[v] == false && _D[v] <= min)
                {
                    min = _D[v];
                    min_index = v;
                }
            }
            return min_index;
        }

        private static void printForwardingTable()
        {
            System.out.println("Shortest path found!\n");
            System.out.println("Forwarding Table:");
            System.out.println("Destination\t  Link");
            System.out.println("========================");

            int j;
            for (int i = 1; i < _NPrime.size(); i++)
            {
                j = i;
                while(j != -1 && _P[j] != 0)
                {
                    j = _P[j];
                }
                if (j != -1)
                {
                    System.out.println("V" + i + "\t\t(V" + 0 + ", " + "V" + j + ")");
                }
            }
            System.out.println();
        }

        private static void dijkstra()
        {
            _D = new int[_n];
            _P = new int[_n];
            _YPrime = new ArrayList<>();  //empty set
            _NPrime = new ArrayList<>(); // start with u in N'
            _NPrime.add(0);

            Boolean previouslySeen[] = new Boolean[_n];

            for (int i = 0; i < _n; i++)
            {
                _D[i] = Integer.MAX_VALUE;
                previouslySeen[i] = false;
            }

            for (int i = 0; i < _n; i++)
            {
                if(isAdjacent(0, i))
                {
                    _P[i] = 0;
                }
                else
                {
                    _P[i] = -1;
                }
            }

            _D[0] = 0;
            Pair tempPair;

            for (int count = 0; count < _n; count++)
            {
                int k = findMinDistance(previouslySeen);
                if (!_NPrime.contains(k))
                {
                    _NPrime.add(k);

                    boolean pairExists = false;
                    tempPair = new Pair(_P[k], k);
                    for (Pair p : _YPrime)
                    {
                        if (p.equals(tempPair))
                        {
                            pairExists = true;
                            break;
                        }
                    }
                    if (!pairExists)
                    {
                        _YPrime.add(tempPair);
                    }
                }

                previouslySeen[k] = true;
                for (int v = 0; v < _n; v++)
                {
                    if (!previouslySeen[v] && _costMatrix[k][v] != 0 && _costMatrix[k][v] != Integer.MAX_VALUE &&
                            _D[k] != Integer.MAX_VALUE && _D[k] + _costMatrix[k][v] < _D[v])
                    {
                        _D[v] = _D[k] + _costMatrix[k][v];
                        _P[v] = k;
                    }
                }

                if (count == 0)
                {
                    System.out.println("Initialization:");
                    System.out.println("----------------");
                }
                else
                {
                    System.out.println("Iteration " + (count) + ":");
                    System.out.println("----------------");
                }
                printIntermediateValues(_NPrime, "N'");
                System.out.println();

                printIntermediateValues(_YPrime);
                System.out.println();

                printIntermediateValues(_D, "D(i)", true);
                System.out.println();

                printIntermediateValues(_P, "p(i)", false);
                System.out.println("\n");
            }

            printForwardingTable();
        }

        private static boolean isAdjacent(int u, int i)
        {
            for(Tuple t: RouterCostTuple)
            {
                if( (u == t.getItem1() && i == t.getItem2()) || (u == t.getItem2() && i == t.getItem1()) )
                {
                    return true;
                }
            }
            return false;
        }

        private static void printIntermediateValues(int[] arrs, String name, boolean isDistance)
        {
            int j;
            if (isDistance)
            {
                System.out.print(name + ": {");
                j = 0;
                for (int i : arrs)
                {
                    if (j == arrs.length - 1)
                    {
                        System.out.print(i);
                    }
                    else
                    {
                        System.out.print(i + ", ");
                    }
                    j++;
                }
                System.out.print("}");
            }
            else
            {
                System.out.print(name + ": {");
                j = 0;
                for (int i : arrs)
                {
                    if (i == -1)
                    {
                        if (j == arrs.length - 1)
                        {
                            System.out.print("-");
                        }
                        else
                        {
                            System.out.print("-, ");
                        }
                    }
                    else
                    {
                        if (j == arrs.length - 1)
                        {
                            System.out.print("V" + i);
                        }
                        else
                        {
                            System.out.print("V" + i + ", ");
                        }
                    }
                    j++;
                }
                System.out.print("}");
            }
        }

    private static void printIntermediateValues(ArrayList<Integer> arrs, String name)
    {
        System.out.print(name + ": {");
        int j = 0;
        for(int i : arrs)
        {
            if (j == arrs.size() - 1)
            {
                System.out.print("V" + i);
            }
            else
            {
                System.out.print("V" + i + ", ");
            }
            j++;
        }
        System.out.print("}");
    }

    private static void printIntermediateValues(ArrayList<Pair> arrs)
    {
        System.out.print("Y': {");
        int j = 0;
        for (Pair p : arrs)
        {
            if (j == arrs.size() - 1)
            {
                System.out.print("(" + "V" + p.getItem1() + ", " + "V" + p.getItem2() + ")");
            }
            else
            {
                System.out.print("(" + "V" + p.getItem1() + ", " + "V" + p.getItem2() + "), ");
            }
            j++;
        }
        System.out.print("}");
    }

        private static void processInput()
        {
            Scanner input = new Scanner(System.in);
            int firstRouter;
            int secondRouter;
            int cost;
            int i;

            BufferedReader reader;
            String next;
            String replacementFileName = _FILE_NAME;

            boolean isRouterValid;
            boolean isCostValid;
            boolean isInvalid = false;

            try
            {
                reader = new BufferedReader(new FileReader(_FILE_NAME));
                i = 0;

                while ((next = reader.readLine()) != null)
                {
                    i++;
                    String[] stringHolder = next.split("\\t");
                    firstRouter = Integer.parseInt(stringHolder[0]);
                    secondRouter = Integer.parseInt(stringHolder[1]);
                    cost = Integer.parseInt(stringHolder[2]);

                    while (true)
                    {
                        isRouterValid = (firstRouter >= 0 && firstRouter <= _n - 1) &&
                                         (secondRouter >= 0 && secondRouter <= _n - 1);
                        isCostValid = cost > 0;
                        if (isRouterValid && isCostValid)
                        {
                            RouterCostTuple.add(new Tuple(firstRouter, secondRouter, cost));
                            if (!_N.contains(firstRouter))
                            {
                                _N.add(firstRouter);
                            }
                            if (!_N.contains(secondRouter))
                            {
                                _N.add(secondRouter);
                            }
                            break;
                        }
                        else
                        {
                            reader.close();
                            if (!isRouterValid)
                            {
                                System.out.println("Invalid router number detected in row " + i +
                                                   "\nPlease enter the name of the text file with valid data: ");
                                isInvalid = true;
                            }
                            if(!isCostValid)
                            {
                                System.out.println("Invalid cost value detected in row " + i +
                                        "\nPlease enter the name of the text file with valid data: ");
                                isInvalid = true;
                            }
                            replacementFileName = input.next();
                            reader = new BufferedReader(new FileReader(replacementFileName));
                            i = 0;
                            RouterCostTuple.clear();
                            break;
                        }
                    }
                }
            }
            catch (IOException e)
            {
                if (isInvalid)
                {
                    System.out.println("File " + replacementFileName + " was not found.");
                    System.exit(1);
                }
                else
                {
                    System.out.println("File " + _FILE_NAME + " was not found.");
                    System.exit(1);
                }
            }
        }

        private static void buildCostMatrix()
        {
            _costMatrix = new int[_n][_n];

            //build matrix
            for (int k = 0; k < _n; k++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (k == j)
                    {
                        _costMatrix[k][j] = 0;
                    }
                    else
                    {
                        _costMatrix[k][j] = Integer.MAX_VALUE;
                    }
                }
            }

            int rowIndex;
            int colIndex;
            int cost;
            for (Tuple p : RouterCostTuple)
            {
                rowIndex = p.getItem1();
                colIndex = p.getItem2();
                cost = p.getItem3();
                if (rowIndex == colIndex)
                {
                    _costMatrix[rowIndex][colIndex] = 0;
                }
                else
                {
                    _costMatrix[rowIndex][colIndex] = cost;
                    _costMatrix[colIndex][rowIndex] = cost;
                }
            }
        }
}
