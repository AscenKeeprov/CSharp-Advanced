using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseAndExclude
{
    class Program
    {
	static void Main()
	{
	    Func<int, int, bool> nDivisibleByD = (n, d) => n % d == 0;
	    List<int> numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
	    numbers.Reverse();
	    int divisor = int.Parse(Console.ReadLine());
	    for (int n = 0; n < numbers.Count; n++)
	    {
		if (nDivisibleByD(numbers[n], divisor))
		{
		    numbers.RemoveAt(n);
		    n--;
		}
	    }
	    Console.WriteLine(String.Join(" ", numbers));
	}
    }
}
