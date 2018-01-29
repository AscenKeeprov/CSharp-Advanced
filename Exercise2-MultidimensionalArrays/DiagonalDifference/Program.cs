using System;
using System.Linq;

namespace DiagonalDifference
{
    class Program
    {
        static void Main()
        {
	    int n = int.Parse(Console.ReadLine());
	    int[,] square = new int[n, n];
	    long sumD1 = 0;
	    long sumD2 = 0;
	    for (int r = 0; r < n; r++)
	    {
		int[] rowValues = Console.ReadLine()
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .Select(int.Parse).ToArray();
		for (int c = 0; c < n; c++)
		{
		    square[r, c] = rowValues[c];
		    if (r == c) sumD1 += square[r, c];
		    if (r + c == n - 1) sumD2 += square[r, c];
		}
	    }
	    Console.WriteLine(Math.Abs(sumD1 - sumD2));
	}
    }
}
