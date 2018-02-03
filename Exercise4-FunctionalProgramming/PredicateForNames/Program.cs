using System;
using System.Collections.Generic;
using System.Linq;

namespace PredicateForNames
{
    class Program
    {
	static void Main()
	{
	    Func<int, int, bool> nameTooLong = (n, l) => n > l;
	    int maxNameLength = int.Parse(Console.ReadLine());
	    List<string> names = Console.ReadLine().Split().ToList();
	    for (int i = 0; i < names.Count; i++)
	    {
		if (!nameTooLong(names[i].Length, maxNameLength))
		    Console.WriteLine(names[i]);
	    }
	}
    }
}
