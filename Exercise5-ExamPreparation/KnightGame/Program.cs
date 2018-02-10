using System;

namespace KnightGame
{
    class Program
    {
	static void Main()
	{
	    int n = int.Parse(Console.ReadLine());
	    char[][] board = new char[n][];
	    for (int r = 0; r < n; r++) board[r] = Console.ReadLine().Trim().ToCharArray();
	    Knight bestKnight = new Knight() { Row = -1, Column = -1, Kills = 0 };
	    int knightsRemoved = 0;
	    do
	    {
		if (bestKnight.Kills > 0)
		{
		    board[bestKnight.Row][bestKnight.Column] = '0';
		    bestKnight.Kills = 0;
		    knightsRemoved++;
		}
		for (int r = 0; r < n; r++)
		{
		    for (int c = 0; c < n; c++)
		    {
			int knightKills = 0;
			if (board[r][c] == 'K')
			{
			    if (r - 2 >= 0 && c - 1 >= 0 && board[r - 2][c - 1] == 'K') knightKills++;
			    if (r - 2 >= 0 && c + 1 < n && board[r - 2][c + 1] == 'K') knightKills++;
			    if (r - 1 >= 0 && c - 2 >= 0 && board[r - 1][c - 2] == 'K') knightKills++;
			    if (r - 1 >= 0 && c + 2 < n && board[r - 1][c + 2] == 'K') knightKills++;
			    if (r + 1 < n && c - 2 >= 0 && board[r + 1][c - 2] == 'K') knightKills++;
			    if (r + 1 < n && c + 2 < n && board[r + 1][c + 2] == 'K') knightKills++;
			    if (r + 2 < n && c - 1 >= 0 && board[r + 2][c - 1] == 'K') knightKills++;
			    if (r + 2 < n && c + 1 < n && board[r + 2][c + 1] == 'K') knightKills++;
			}
			if (knightKills > bestKnight.Kills)
			{
			    bestKnight.Row = r;
			    bestKnight.Column = c;
			    bestKnight.Kills = knightKills;
			}
		    }
		}
	    }
	    while (bestKnight.Kills > 0);
	    Console.WriteLine(knightsRemoved);
	}
    }

    public class Knight
    {
	public int Row { get; set; }
	public int Column { get; set; }
	public int Kills { get; set; }
    }
}
