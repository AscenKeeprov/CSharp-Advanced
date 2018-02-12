using System;
using System.Collections.Generic;
using System.Linq;

namespace Sneaking
{
    class Program
    {
	static void Main()
	{
	    int rows = int.Parse(Console.ReadLine());
	    char[][] room = new char[rows][];
	    for (int r = 0; r < rows; r++) room[r] = Console.ReadLine().Trim().ToCharArray();
	    Tuple<int, int> samPosition = new Tuple<int, int>(-1, -1);
	    samPosition = LocateActor(room, samPosition, "Sam");
	    Tuple<int, int> nikoladzePosition = new Tuple<int, int>(-1, -1);
	    nikoladzePosition = LocateActor(room, nikoladzePosition, "Nikoladze");
	    Queue<char> samMovement = new Queue<char>(Console.ReadLine().ToCharArray());
	    bool isNikoladzeAlive = true;
	    bool isSamAlive = true;
	    while (isSamAlive && isNikoladzeAlive)
	    {
		EnemiesMove(room);
		isSamAlive = CheckSamStatus(room, isSamAlive, samPosition);
		if (isSamAlive && samMovement.Count > 0)
		    samPosition = SamMoves(room, samPosition, samMovement);
		isNikoladzeAlive = CheckNikoladzeStatus(room, nikoladzePosition, isNikoladzeAlive);
		if (!isSamAlive || !isNikoladzeAlive)
		    ViewRoom(room, isSamAlive, samPosition, isNikoladzeAlive, nikoladzePosition);
	    }
	}

	private static Tuple<int, int> LocateActor(char[][] room, Tuple<int, int> actorPosition, string actor)
	{
	    for (int row = 0; row < room.Length; row++)
	    {
		string rowLayout = String.Join("", room[row]);
		if (rowLayout.Contains('S') && actor == "Sam")
		{
		    int column = rowLayout.IndexOf('S');
		    actorPosition = Tuple.Create(row, column);
		    break;
		}
		else if (rowLayout.Contains('N') && actor == "Nikoladze")
		{
		    int column = rowLayout.IndexOf('N');
		    actorPosition = Tuple.Create(row, column);
		    break;
		}
	    }
	    return actorPosition;
	}

	private static void EnemiesMove(char[][] room)
	{
	    for (int r = 1; r < room.Length; r++)
	    {
		if (room[r].Contains('b') || room[r].Contains('d'))
		{
		    string row = String.Join("", room[r]);
		    int enemyPosition;
		    if (room[r].Contains('b'))
		    {
			enemyPosition = row.IndexOf('b');
			if (enemyPosition == row.Length - 1) room[r][enemyPosition] = 'd';
			else
			{
			    room[r][enemyPosition + 1] = 'b';
			    room[r][enemyPosition] = '.';
			}
		    }
		    else if (room[r].Contains('d'))
		    {
			enemyPosition = row.IndexOf('d');
			if (enemyPosition == 0) room[r][enemyPosition] = 'b';
			else
			{
			    room[r][enemyPosition - 1] = 'd';
			    room[r][enemyPosition] = '.';
			}
		    }
		}
	    }
	}

	private static Tuple<int, int> SamMoves(char[][] room, Tuple<int, int> samPosition, Queue<char> samMovement)
	{
	    int samRow = samPosition.Item1;
	    int samCol = samPosition.Item2;
	    char move = '\0';
	    if (samMovement.Count > 0) move = samMovement.Dequeue();
	    if (move == 'U' && samRow > 0)
	    {
		room[samRow - 1][samCol] = 'S';
		room[samRow][samCol] = '.';
		samRow--;
	    }
	    else if (move == 'D' && samRow < room.Length - 1)
	    {
		room[samRow + 1][samCol] = 'S';
		room[samRow][samCol] = '.';
		samRow++;
	    }
	    else if (move == 'L' && samCol > 0)
	    {
		room[samRow][samCol - 1] = 'S';
		room[samRow][samCol] = '.';
		samCol--;
	    }
	    else if (move == 'R' && samCol < room[samRow].Length - 1)
	    {
		room[samRow][samCol + 1] = 'S';
		room[samRow][samCol] = '.';
		samCol++;
	    }
	    samPosition = Tuple.Create(samRow, samCol);
	    return samPosition;
	}

	private static bool CheckSamStatus(char[][] room, bool isSamAlive, Tuple<int, int> samPosition)
	{
	    int samRow = samPosition.Item1;
	    int samCol = samPosition.Item2;
	    for (int r = 0; r < room.Length; r++)
	    {
		if (room[r].Contains('S') && room[r].Contains('b') ||
		    room[r].Contains('S') && room[r].Contains('d'))
		{
		    string fightRow = String.Join("", room[r]);
		    int bPosition = fightRow.IndexOf('b');
		    int dPosition = fightRow.IndexOf('d');
		    if (bPosition >= 0 && samCol > bPosition)
		    {
			isSamAlive = false;
			room[samRow][samCol] = 'X';
			break;
		    }
		    if (dPosition >= 0 && samCol < bPosition)
		    {
			isSamAlive = false;
			break;
		    }
		}
	    }
	    return isSamAlive;
	}

	private static bool CheckNikoladzeStatus(char[][] room, Tuple<int, int> nikoladzePosition, bool isNikoladzeAlive)
	{
	    int nikoladzeRow = nikoladzePosition.Item1;
	    int nikoladzeCol = nikoladzePosition.Item2;
	    for (int r = 0; r < room.Length; r++)
	    {
		if (room[r].Contains('S') && room[r].Contains('N'))
		{
		    isNikoladzeAlive = false;
		    room[nikoladzeRow][nikoladzeCol] = 'X';
		    break;
		}
	    }
	    return isNikoladzeAlive;
	}

	private static void ViewRoom(char[][] room, bool isSamAlive, Tuple<int, int> samPosition,
	    bool isNikoladzeAlive, Tuple<int, int> nikoladzePosition)
	{
	    if (!isSamAlive) Console.WriteLine($"Sam died at {samPosition.Item1}, {samPosition.Item2}");
	    if (!isNikoladzeAlive) Console.WriteLine("Nikoladze killed!");
	    foreach (var row in room)
	    {
		Console.Write(String.Join("", row));
		Console.WriteLine();
	    }
	}
    }
}
