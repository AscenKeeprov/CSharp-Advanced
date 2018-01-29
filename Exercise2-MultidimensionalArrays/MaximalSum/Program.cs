using System;
using System.Linq;

namespace MaximalSum
{
    class Program
    {
        static void Main()
        {
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    int[,] matrix = new int[size[0], size[1]];
	    for (int r = 0; r < size[0]; r++)
	    {
		int[] rowValues = Console.ReadLine()
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .Select(int.Parse).ToArray();
		for (int c = 0; c < size[1]; c++)
		    matrix[r, c] = rowValues[c];
	    }
	    long maxSum = 0;
	    Tuple<int,int> origin = new Tuple<int, int>(0,0);
	    for (int r = 0; r < matrix.GetLength(0) - 2; r++)
	    {
		for (int c = 0; c < matrix.GetLength(1) - 2; c++)
		{
		    long currSum = matrix[r, c] + matrix[r, c + 1] + matrix[r, c + 2]
			+ matrix[r + 1, c] + matrix[r + 1, c + 1] + matrix[r + 1, c + 2]
			+ matrix[r + 2, c] + matrix[r + 2, c + 1] + matrix[r + 2, c + 2];
		    if (currSum > maxSum)
		    {
			maxSum = currSum;
			origin = Tuple.Create(r, c);
		    }
		}
	    }
	    Console.WriteLine($"Sum = {maxSum}");
	    for (int r = origin.Item1; r < origin.Item1 + 3; r++)
	    {
		for (int c = origin.Item2; c < origin.Item2 + 3; c++)
		    Console.Write(matrix[r, c] + " ");
		Console.WriteLine();
	    }
	}
    }
}
