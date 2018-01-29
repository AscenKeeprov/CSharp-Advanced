using System;
using System.Linq;

namespace RadioactiveBunnies
{
    class Program
    {
	static void Main()
	{
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    char[,] lair = new char[size[0], size[1]];
	    Player player = new Player() { Status = "Moving" };
	    for (int r = 0; r < lair.GetLength(0); r++)
	    {
		char[] actors = Console.ReadLine().ToCharArray();
		for (int c = 0; c < lair.GetLength(1); c++)
		{
		    lair[r, c] = actors[c];
		    if (actors[c] == 'P') player.Position = Tuple.Create(r, c);
		}
	    }
	    string movement = Console.ReadLine();
	    foreach (char move in movement)
	    {
		player = PlayerMoves(lair, player, move);
		BunniesMultiply(lair);
		if (!player.Status.Equals("Moving"))
		{
		    ViewLair(lair);
		    Console.WriteLine($"{player.Status.ToLower()}:" +
			$" {player.Position.Item1} {player.Position.Item2}");
		    Environment.Exit(0);
		}
	    }
	}

	private static Player PlayerMoves(char[,] lair, Player player, char move)
	{
	    int row = player.Position.Item1;
	    int col = player.Position.Item2;
	    switch (move)
	    {
		case 'U':
		    if (row == 0) player.Status = "Won";
		    else
		    {
			if (lair[row - 1, col] == 'B') player.Status = "Dead";
			else lair[row - 1, col] = 'P';
			player.Position = Tuple.Create(row - 1, col);
		    }
		    lair[row, col] = '.';
		    break;
		case 'D':
		    if (row == lair.GetLength(0) - 1) player.Status = "Won";
		    else
		    {
			if (lair[row + 1, col] == 'B') player.Status = "Dead";
			else lair[row + 1, col] = 'P';
			player.Position = Tuple.Create(row + 1, col);
		    }
		    break;
		case 'R':
		    if (col == lair.GetLength(1) - 1) player.Status = "Won";
		    else
		    {
			if (lair[row, col + 1] == 'B') player.Status = "Dead";
			else lair[row, col + 1] = 'P';
			player.Position = Tuple.Create(row, col + 1);
		    }
		    break;
		case 'L':
		    if (col == 0) player.Status = "Won";
		    else
		    {
			if (lair[row, col - 1] == 'B') player.Status = "Dead";
			else lair[row, col - 1] = 'P';
			player.Position = Tuple.Create(row, col - 1);
		    }
		    break;
	    }
	    lair[row, col] = '.';
	    if (!player.Status.Equals("Won")) player = BiohazardCheck(player, lair);
	    return player;
	}

	private static Player BiohazardCheck(Player player, char[,] lair)
	{
	    int row = player.Position.Item1;
	    int col = player.Position.Item2;
	    char cellAbove = '\0';
	    char cellBelow = '\0';
	    char cellToLeft = '\0';
	    char cellToRight = '\0';
	    if (row > 0) cellAbove = lair[row - 1, col];
	    if (row < lair.GetLength(0) - 1) cellBelow = lair[row + 1, col];
	    if (col > 0) cellToLeft = lair[row, col - 1];
	    if (col < lair.GetLength(1) - 1) cellToRight = lair[row, col + 1];
	    if (cellAbove == 'B' || cellBelow == 'B' || cellToLeft == 'B' || cellToRight == 'B')
		player.Status = "Dead";
	    return player;
	}

	private static void BunniesMultiply(char[,] lair)
	{
	    for (int r = 0; r < lair.GetLength(0); r++)
	    {
		for (int c = 0; c < lair.GetLength(1); c++)
		{
		    if (lair[r, c] == 'B')
		    {
			if (r > 0 && lair[r - 1, c] != 'B') lair[r - 1, c] = 'ѣ';
			if (r < lair.GetLength(0) - 1 && lair[r + 1, c] != 'B') lair[r + 1, c] = 'ѣ';
			if (c > 0 && lair[r, c - 1] != 'B') lair[r, c - 1] = 'ѣ';
			if (c < lair.GetLength(1) - 1 && lair[r, c + 1] != 'B') lair[r, c + 1] = 'ѣ';
		    }
		}
	    }
	    for (int r = 0; r < lair.GetLength(0); r++)
		for (int c = 0; c < lair.GetLength(1); c++)
		    if (lair[r, c] == 'ѣ') lair[r, c] = 'B';
	}

	private static void ViewLair(char[,] lair)
	{
	    for (int r = 0; r < lair.GetLength(0); r++)
	    {
		for (int c = 0; c < lair.GetLength(1); c++)
		    Console.Write(lair[r, c]);
		Console.WriteLine();
	    }
	}
    }

    class Player
    {
	public Tuple<int, int> Position { get; set; }
	public string Status { get; set; }
    }
}
