using System;
using System.Linq;

namespace Crossfire
{
    class Program
    {
	static void Main()
	{
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    int[][] target = new int[size[0]][];
	    int targetWidth = size[1];
	    int value = 1;
	    for (int r = 0; r < target.Length; r++)
	    {
		target[r] = new int[size[1]];
		for (int c = 0; c < target[r].Length; c++)
		    target[r][c] = value++;
	    }
	    string command = Console.ReadLine();
	    while (command != "Nuke it from orbit")
	    {
		int[] blast = command
		    .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		    .Select(int.Parse).ToArray();
		long blastRow = blast[0];
		long blastColumn = blast[1];
		int blastRadius = blast[2];
		if (blastRow >= 0 && blastRow < target.Length)
		{
		    long blastLeft = Math.Max(blastColumn - blastRadius, 0);
		    long blastRight = Math.Min(blastColumn + blastRadius, target[blastRow].Length - 1);
		    for (long col = blastLeft; col <= blastRight; col++)
			target[blastRow][col] = 0;
		}
		if (blastColumn >= 0 && blastColumn < targetWidth)
		{
		    long blastTop = Math.Max(blastRow - blastRadius, 0);
		    long blastBottom = Math.Min(blastRow + blastRadius, target.Length - 1);
		    for (long row = blastTop; row <= blastBottom; row++)
			if (blastColumn <= target[row].Length - 1)
			    target[row][blastColumn] = 0;
		}
		targetWidth = 0;
		for (int r = 0; r < target.Length; r++)
		{
		    if (target[r].Any(c => c == 0))
			target[r] = target[r].Where(n => n > 0).ToArray();
		    if (target[r].Length == 0)
		    {
			target = target.Take(r).Concat(target.Skip(r + 1)).ToArray();
			if (r > 0) r--;
		    }
		    if (target[r].Length > targetWidth) targetWidth = target[r].Length;
		}
		command = Console.ReadLine();
	    }
	    for (int r = 0; r < target.Length; r++)
	    {
		for (int c = 0; c < target[r].Length; c++)
		    Console.Write(target[r][c] + " ");
		Console.WriteLine();
	    }
	}
    }
}
