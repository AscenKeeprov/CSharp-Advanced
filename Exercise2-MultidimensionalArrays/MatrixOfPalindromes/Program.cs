using System;
using System.Linq;
using System.Text;

namespace MatrixOfPalindromes
{
    class Program
    {
	static void Main()
	{
	    char[] alphabet = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
	    StringBuilder palindrome = new StringBuilder();
	    int[] size = Console.ReadLine().Split().Select(int.Parse).ToArray();
	    if (size[0] + size[1] > 27) Environment.Exit(50);
	    string[,] matrix = new string[size[0], size[1]];
	    for (int r = 0; r < matrix.GetLength(0); r++)
	    {
		for (int c = 0; c < matrix.GetLength(1); c++)
		{
		    palindrome.Append(alphabet[r]);
		    palindrome.Append(alphabet[r + c]);
		    palindrome.Append(alphabet[r]);
		    matrix[r, c] =  palindrome.ToString();
		    Console.Write(matrix[r,c] + " ");
		    palindrome.Clear();
		}
		Console.WriteLine();
	    }
	}
    }
}
