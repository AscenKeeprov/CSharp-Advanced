using System;
using System.Linq;

namespace DangerousFloor
{
    class Program
    {
	static void Main()
	{
	    char[][] board = new char[8][];
	    for (int r = 0; r < 8; r++)
		board[r] = Console.ReadLine().Split(',').Select(char.Parse).ToArray();
	    string move;
	    while (!(move = Console.ReadLine().Trim()).Equals("END"))
	    {
		char piece = move[0];
		int rowFrom = (int)Char.GetNumericValue(move[1]);
		int colFrom = (int)Char.GetNumericValue(move[2]);
		if (!PieceExists(board, rowFrom, colFrom, piece))
		{
		    Console.WriteLine("There is no such a piece!");
		    continue;
		}
		int rowTo = (int)Char.GetNumericValue(move[4]);
		int colTo = (int)Char.GetNumericValue(move[5]);
		if (!IsMoveValid(piece, rowFrom, colFrom, rowTo, colTo))
		{
		    Console.WriteLine("Invalid move!");
		    continue;
		}
		if (!IsMoveWithinBoard(rowTo, colTo))
		{
		    Console.WriteLine("Move go out of board!");
		    continue;
		}
		if (board[rowTo][colTo] == 'x')
		{
		    board[rowTo][colTo] = board[rowFrom][colFrom];
		    board[rowFrom][colFrom] = 'x';
		}
	    }
	}

	private static bool PieceExists(char[][] board, int rowFrom, int colFrom, char piece)
	{
	    bool targetCell = board[rowFrom][colFrom] == piece;
	    bool anywhereOnBoard = board.Any(row => row.Contains(piece));
	    return targetCell && anywhereOnBoard;
	}

	private static bool IsMoveValid(char piece, int rowFrom, int colFrom, int rowTo, int colTo)
	{
	    switch (piece)
	    {
		case 'B':
		    return IsMoveDiagonal(rowFrom, colFrom, rowTo, colTo);
		case 'K':
		    return (Math.Abs(rowFrom - rowTo) == 1 && colFrom == colTo ||
			rowFrom == rowTo && Math.Abs(colFrom - colTo) == 1 ||
			Math.Abs(rowFrom - rowTo) == 1 && Math.Abs(colFrom - colTo) == 1);
		case 'P':
		    return (rowFrom - rowTo == 1 && colFrom == colTo);
		case 'Q':
		    return (IsMoveDiagonal(rowFrom, colFrom, rowTo, colTo) ||
			IsMoveLikeCross(rowFrom, colFrom, rowTo, colTo));
		case 'R':
		    return (IsMoveLikeCross(rowFrom, colFrom, rowTo, colTo));
		default:
		    return false;
	    }
	}

	private static bool IsMoveDiagonal(int rowFrom, int colFrom, int rowTo, int colTo)
	{
	    return Math.Abs(rowFrom - rowTo) == Math.Abs(colFrom - colTo);
	}

	private static bool IsMoveLikeCross(int rowFrom, int colFrom, int rowTo, int colTo)
	{
	    bool horizontal = rowFrom == rowTo;
	    bool vertical = colFrom == colTo;
	    return horizontal || vertical;
	}

	private static bool IsMoveWithinBoard(int rowTo, int colTo)
	{
	    return (rowTo >= 0 && rowTo < 8 && colTo >= 0 && colTo < 8);
	}
    }
}
