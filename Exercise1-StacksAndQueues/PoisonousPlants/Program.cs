using System;
using System.Collections.Generic;
using System.Linq;

namespace PoisonousPlants
{
    class Program
    {
	static void Main()
	{
	    int n = int.Parse(Console.ReadLine());
	    List<int> plants = Console.ReadLine()
		.Split().Select(int.Parse).ToList();
	    Queue<int> pots = new Queue<int>(plants);
	    Stack<int> mutants = new Stack<int>();
	    bool casualties;
	    int days = 0;
	    do
	    {
		int plantLeft;
		int deaths = 0;
		casualties = false;
		for (int pot = 1; pot < n; pot++)
		{
		    if (mutants.Count > 0)
			plantLeft = mutants.Pop();
		    else
		    {
			plantLeft = pots.Dequeue();
			pots.Enqueue(plantLeft);
		    }
		    int plantRight = pots.Peek();
		    if (plantRight > plantLeft)
		    {
			mutants.Push(pots.Dequeue());
			casualties = true;
			deaths++;
		    }
		    else if (pot == (n - 1))
			pots.Enqueue(pots.Dequeue());
		}
		if (casualties == false)
		{
		    Console.WriteLine(days);
		    Environment.Exit(0);
		}
		else
		{
		    mutants.Clear();
		    n -= deaths;
		    days++;
		}
	    }
	    while (casualties == true);
	}
    }
}
