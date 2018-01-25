using System;
using System.Collections.Generic;
using System.Linq;

namespace TruckTour
{
    class Program
    {
	static void Main()
	{
	    uint pumpsCount = uint.Parse(Console.ReadLine());
	    Queue<uint[]> pumps = new Queue<uint[]>();
	    for (int i = 0; i < pumpsCount; i++)
	    {
		uint[] pump = Console.ReadLine().Split().Select(uint.Parse).ToArray();
		pumps.Enqueue(pump);
	    }
	    uint tankPetrol = 0;
	    bool fullCircle = false;
	    for (int pumpStart = 0; pumpStart <= pumpsCount - 1; pumpStart++)
	    {
		for (int pumpsPassed = 0; pumpsPassed < pumpsCount; pumpsPassed++)
		{
		    uint[] currentPump = pumps.Dequeue();
		    uint pumpPetrol = currentPump[0];
		    tankPetrol += pumpPetrol;
		    uint petrolNeeded = currentPump[1];
		    pumps.Enqueue(currentPump);
		    if (tankPetrol < petrolNeeded)
		    {
			tankPetrol = 0;
			pumpStart += pumpsPassed;
			break;
		    }
		    else tankPetrol -= petrolNeeded;
		    if (pumpsPassed == (pumpsCount - 1)) fullCircle = true;
		}
		if (fullCircle)
		{
		    Console.WriteLine(pumpStart);
		    Environment.Exit(0);
		}
	    }
	}
    }
}
