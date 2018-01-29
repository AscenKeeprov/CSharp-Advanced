using System;
using System.Linq;

namespace LegoBlocks
{
    class Program
    {
        static void Main()
        {
	    int n = int.Parse(Console.ReadLine());
	    int[][] jagArrayLeft = new int[n][];
	    int jagArrayLeftCellCount = 0;
	    for (int r = 0; r < n; r++)
	    {
		int[] jagArrayLeftRow = Console.ReadLine()
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .Select(int.Parse).ToArray();
		jagArrayLeftCellCount += jagArrayLeftRow.Length;
		jagArrayLeft[r] = new int[jagArrayLeftRow.Length];
		Array.Copy(jagArrayLeftRow, jagArrayLeft[r], jagArrayLeftRow.Length);
	    }
	    int[][] jagArrayRight = new int[n][];
	    int jagArrayRightCellCount = 0;
	    for (int r = 0; r < n; r++)
	    {
		int[] jagArrayRightRow = Console.ReadLine()
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .Select(int.Parse).ToArray();
		jagArrayRight[r] = new int[jagArrayRightRow.Length];
		jagArrayRightCellCount += jagArrayRightRow.Length;
		Array.Reverse(jagArrayRightRow);
		Array.Copy(jagArrayRightRow, jagArrayRight[r], jagArrayRightRow.Length);
	    }
	    int cellCountTotal = jagArrayLeftCellCount + jagArrayRightCellCount;
	    if (cellCountTotal % n != 0)
	    {
		Console.WriteLine($"The total number of cells is: {cellCountTotal}");
		Environment.Exit(0);
	    }
	    int[][] jagArrayMerged = new int[n][];
	    for (int r = 0; r < n; r++)
	    {
		int rowLength = jagArrayLeft[r].Length + jagArrayRight[r].Length;
		if (rowLength != jagArrayLeft[0].Length + jagArrayRight[0].Length)
		{
		    Console.WriteLine($"The total number of cells is: {cellCountTotal}");
		    Environment.Exit(0);
		}
		jagArrayMerged[r] = new int[rowLength];
		Array.Copy(jagArrayLeft[r], jagArrayMerged[r], jagArrayLeft[r].Length);
		Array.Copy(jagArrayRight[r], 0, jagArrayMerged[r], jagArrayLeft[r].Length, jagArrayRight[r].Length);
	    }
	    for (int r = 0; r < jagArrayMerged.Length; r++)
	    {
		for (int i = 0; i < jagArrayMerged[r].Length; i++)
		{
		    if (i == 0) Console.Write("[" + jagArrayMerged[r][i] + ", ");
		    else if (i == jagArrayMerged[r].Length - 1)
			Console.WriteLine(jagArrayMerged[r][i] + "]");
		    else Console.Write(jagArrayMerged[r][i] + ", ");
		}
	    }
	}
    }
}
