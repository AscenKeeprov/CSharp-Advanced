using System;
using System.Collections.Generic;
using System.Linq;

namespace TargetPractice
{
    class Program
    {
	static void Main()
	{
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    char[,] staircase = new char[size[0], size[1]];
	    Queue<char> snake = new Queue<char>(Console.ReadLine().ToCharArray());
	    Infest(staircase, snake);
	    int[] shot = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    Purge(staircase, shot);
	    Fallout(staircase);
	    for (int r = 0; r < staircase.GetLength(0); r++)
	    {
		for (int c = 0; c < staircase.GetLength(1); c++)
		    Console.Write(staircase[r, c]);
		Console.WriteLine();
	    }
	}

	private static void Infest(char[,] staircase, Queue<char> snake)
	{
	    int stair = 0;
	    for (int r = staircase.GetLength(0) - 1; r >= 0; r--)
	    {
		if (stair % 2 == 0)
		    for (int c = staircase.GetLength(1) - 1; c >= 0; c--)
		    {
			staircase[r, c] = snake.Peek();
			snake.Enqueue(snake.Dequeue());
		    }
		else for (int c = 0; c < staircase.GetLength(1); c++)
		    {
			staircase[r, c] = snake.Peek();
			snake.Enqueue(snake.Dequeue());
		    }
		stair++;
	    }
	}

	private static void Purge(char[,] staircase, int[] shot)
	{
	    for (int r = 0; r < staircase.GetLength(0); r++)
	    {
		for (int c = 0; c < staircase.GetLength(1); c++)
		{
		    double cellDistanceFromImpact = Math.Sqrt(
			Math.Pow(r - shot[0], 2) + Math.Pow(c - shot[1], 2));
		    if (cellDistanceFromImpact <= shot[2]) staircase[r, c] = ' ';
		}
	    }
	}

	private static void Fallout(char[,] staircase)
	{
	    for (int c = 0; c < staircase.GetLength(1); c++)
	    {
		int emptyCells = 0;
		for (int r = staircase.GetLength(0) - 1; r >= 0; r--)
		{
		    if (staircase[r, c] == ' ') emptyCells++;
		    else if (emptyCells > 0)
		    {
			staircase[r + emptyCells, c] = staircase[r, c];
			staircase[r, c] = ' ';
		    }
		}
	    }
	}
    }
}
