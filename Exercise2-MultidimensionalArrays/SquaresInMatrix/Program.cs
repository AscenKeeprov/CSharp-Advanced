using System;
using System.Linq;

namespace SquaresInMatrix
{
    class Program
    {
        static void Main()
        {
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    char[,] matrix = new char[size[0], size[1]];
	    for (int r = 0; r < size[0]; r++)
	    {
		char[] rowValues = Console.ReadLine()
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .Select(char.Parse).ToArray();
		for (int c = 0; c < size[1]; c++)
		    matrix[r, c] = rowValues[c];
	    }
	    int equalSquares = 0;
	    for (int row = 0; row < matrix.GetLength(0) - 1; row++)
	    {
		for (int col = 0; col < matrix.GetLength(1) - 1; col++)
		{
		    if (matrix[row, col] == matrix[row, col + 1] &&
			matrix[row, col + 1] == matrix[row + 1, col] &&
			matrix[row + 1, col] == matrix[row + 1, col + 1])
			equalSquares++;
		}
	    }
	    Console.WriteLine(equalSquares);
        }
    }
}
