using System;
using System.Collections.Generic;
using System.Linq;

namespace ParkingSystem
{
    class Program
    {
	static void Main()
	{
	    SortedDictionary<int, int> freeSpots = new SortedDictionary<int, int>();
	    int[] size = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    int[][] parking = new int[size[0]][];
	    string entry;
	    while (!(entry = Console.ReadLine().ToUpper()).Equals("STOP"))
	    {
		int entryRow = int.Parse(entry.Split()[0]);
		int row = int.Parse(entry.Split()[1]);
		int col = int.Parse(entry.Split()[2]);
		if (parking[row] == null) parking[row] = new int[size[1]];
		if (!freeSpots.ContainsKey(row)) freeSpots.Add(row, size[1] - 1);
		Tuple<int, int> spot = new Tuple<int, int>(row, col);
		if (freeSpots[row] > 0)
		{
		    spot = Find(parking, spot, freeSpots);
		    Park(parking, spot, freeSpots, entryRow);
		}
		else Console.WriteLine($"Row {row} full");
	    }
	}

	private static Tuple<int, int> Find(int[][] parking, Tuple<int, int> spot,
	    SortedDictionary<int, int> freeSpots)
	{
	    int row = spot.Item1;
	    int col = spot.Item2;
	    Tuple<int, int> desiredSpot = new Tuple<int, int>(row, col);
	    bool spotFound = false;
	    if (freeSpots[row] == 1)
	    {
		for (int c = 1; c < parking[row].Length; c++)
		{
		    if (parking[row][c] == 0)
		    {
			spot = Tuple.Create(row, c);
			spotFound = true;
			break;
		    }
		}
	    }
	    while (spotFound == false)
	    {
		for (int c = col; c > 0; c--)
		{
		    if (parking[row][c] == 0)
		    {
			spot = Tuple.Create(row, c);
			spotFound = true;
			break;
		    }
		}
		for (int c = col + 1; c < parking[row].Length; c++)
		{
		    if (parking[row][c] == 0)
		    {
			if (spotFound == false)
			{
			    spot = Tuple.Create(row, c);
			    spotFound = true;
			}
			else
			{
			    int spotDistanceLeft = desiredSpot.Item2 - spot.Item2;
			    int spotDistanceRight = c - desiredSpot.Item2;
			    if (spotDistanceRight < spotDistanceLeft)
				spot = Tuple.Create(row, c);
			}
			break;
		    }
		}
	    }
	    return spot;
	}

	private static void Park(int[][] parking, Tuple<int, int> spot,
	    SortedDictionary<int, int> freeSpots, int entryRow)
	{
	    parking[spot.Item1][spot.Item2] = 1;
	    freeSpots[spot.Item1]--;
	    int distanceToParkSpot = 1;
	    int distanceToSpotRow = Math.Abs(spot.Item1 - entryRow);
	    int distanceToSpotColumn = spot.Item2 - 0;
	    distanceToParkSpot += distanceToSpotRow + distanceToSpotColumn;
	    Console.WriteLine(distanceToParkSpot);
	}
    }
}
