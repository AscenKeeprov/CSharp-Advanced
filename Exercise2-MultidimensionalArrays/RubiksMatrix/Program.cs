using System;
using System.Linq;

namespace RubiksMatrix
{
    class Program
    {
	static void Main()
	{
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    int[,] matrix = new int[size[0], size[1]];
	    int[,] rubix = new int[size[0], size[1]];
	    int cellValue = 1;
	    for (int r = 0; r < size[0]; r++)
	    {
		for (int c = 0; c < size[1]; c++)
		{
		    matrix[r, c] = cellValue++;
		    rubix[r, c] = matrix[r, c];
		}
	    }
	    int n = int.Parse(Console.ReadLine());
	    for (int i = 1; i <= n; i++)
	    {
		string[] command = Console.ReadLine()
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .ToArray();
		Shuffle(rubix, command);
	    }
	    Restore(rubix, matrix);
	}

	private static int[,] Shuffle(int[,] rubix, string[] command)
	{
	    int rowCount = rubix.GetLength(0);
	    int colCount = rubix.GetLength(1);
	    string direction = command[1].ToUpper();
	    switch (direction)
	    {
		case "UP":
		    int col = int.Parse(command[0]);
		    int moves = int.Parse(command[2]) % rowCount;
		    for (int m = 1; m <= moves; m++)
		    {
			int colTop = rubix[0, col];
			for (int r = 0; r < rowCount - 1; r++)
			    rubix[r, col] = rubix[r + 1, col];
			rubix[rowCount - 1, col] = colTop;
		    }
		    break;
		case "DOWN":
		    col = int.Parse(command[0]);
		    moves = int.Parse(command[2]) % rowCount;
		    for (int m = 1; m <= moves; m++)
		    {
			int colBottom = rubix[rowCount - 1, col];
			for (int r = rowCount - 1; r > 0; r--)
			    rubix[r, col] = rubix[r - 1, col];
			rubix[0, col] = colBottom;
		    }
		    break;
		case "RIGHT":
		    int row = int.Parse(command[0]);
		    moves = int.Parse(command[2]) % colCount;
		    for (int m = 1; m <= moves; m++)
		    {
			int rowEnd = rubix[row, colCount - 1];
			for (int c = colCount - 1; c > 0; c--)
			    rubix[row, c] = rubix[row, c - 1];
			rubix[row, 0] = rowEnd;
		    }
		    break;
		case "LEFT":
		    row = int.Parse(command[0]);
		    moves = int.Parse(command[2]) % colCount;
		    for (int m = 1; m <= moves; m++)
		    {
			int rowStart = rubix[row, 0];
			for (int c = 0; c < colCount - 1; c++)
			    rubix[row, c] = rubix[row, c + 1];
			rubix[row, colCount - 1] = rowStart;
		    }
		    break;
	    }
	    return rubix;
	}

	private static void Restore(int[,] rubix, int[,] matrix)
	{

	    for (int r = 0; r < matrix.GetLength(0); r++)
	    {
		for (int c = 0; c < matrix.GetLength(1); c++)
		{
		    if (rubix[r, c] == matrix[r, c])
			Console.WriteLine("No swap required");
		    else
		    {
			int strayNum = rubix[r, c];
			int originalNum = matrix[r, c];
			Tuple<int, int> coordinates = Locate(originalNum, rubix);
			rubix[r, c] = rubix[coordinates.Item1, coordinates.Item2];
			rubix[coordinates.Item1, coordinates.Item2] = strayNum;
			Console.WriteLine($"Swap ({r}, {c}) with ({coordinates.Item1}, {coordinates.Item2})");
		    }
		}
	    }
	}

	private static Tuple<int, int> Locate(int originalNum, int[,] rubix)
	{
	    Tuple<int, int> coordinates = new Tuple<int, int>(0, 0);
	    for (int r = 0; r < rubix.GetLength(0); r++)
		for (int c = 0; c < rubix.GetLength(1); c++)
		    if (rubix[r, c].Equals(originalNum))
			coordinates = Tuple.Create(r, c);
	    return coordinates;
	}
    }
}
