using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomMinFunction
{
    class Program
    {
        static void Main(string[] args)
        {
	    Func<int, int, int> smallerNumber = (x,y) => (x < y) ? x : y;
	    List<int> numbers = Console.ReadLine().Split().Select(int.Parse).ToList();
	    int minNumber = numbers[0];
	    for (int i = 1; i < numbers.Count; i++)
		minNumber = smallerNumber(minNumber, numbers[i]);
	    Console.WriteLine(minNumber);
	}
    }
}
