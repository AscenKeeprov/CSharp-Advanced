using System;
using System.Collections.Generic;
using System.Linq;

namespace ListOfPredicates
{
    class Program
    {
	static void Main()
	{
	    List<int> numbers = new List<int>();
	    int rangeEnd = int.Parse(Console.ReadLine());
	    if (rangeEnd >= 1) numbers = Enumerable.Range(1, rangeEnd).ToList();
	    else Environment.Exit(50);
	    List<int> dividers = Console.ReadLine().Split().Select(int.Parse).Distinct().ToList();
	    numbers = numbers.Where(DivideByAll(dividers)).ToList();
	    Console.WriteLine(String.Join(" ", numbers));
	}

	private static Func<int, bool> DivideByAll(List<int> dividers)
	{
	    return number =>
	    {
		foreach (var divider in dividers)
		{
		    if (number % divider != 0) return false;
		}
		return true;
	    };
	}
    }
}
