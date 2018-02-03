using System;
using System.Linq;

namespace AppliedArithmetics
{
    class Program
    {
	private static int a;

	static void Main()
	{
	    Func<int[], int[]> add1ToAll = array => array = array.Select(a => a + 1).ToArray();
	    Func<int[], int[]> subtract1FromAll = array => array = array.Select(a => a - 1).ToArray();
	    Func<int[], int[]> multiplyBy2 = array => array = array.Select(a => a * 2).ToArray();
	    Action<int[]> print = array => Console.WriteLine(String.Join(" ", array));
	    int[] numbers = Console.ReadLine().Split().Select(int.Parse).ToArray();
	    string command;
	    while (!(command = Console.ReadLine().ToUpper()).Equals("END"))
	    {
		switch (command)
		{
		    case "ADD":
			numbers = add1ToAll(numbers);
			break;
		    case "SUBTRACT":
			numbers = subtract1FromAll(numbers);
			break;
		    case "MULTIPLY":
			numbers = multiplyBy2(numbers);
			break;
		    case "PRINT":
			print(numbers);
			break;
		}
	    }
	}
    }
}
