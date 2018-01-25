using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicStackOps
{
    class Program
    {
        static void Main()
        {
	    int[] parameters = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    Stack<int> stack = new Stack<int>(parameters[0]);
	    int[] numbers = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    for (int n = 0; n < parameters[0]; n++) stack.Push(numbers[n]);
	    int maxPop = Math.Min(parameters[1], stack.Count);
	    for (int s = 1; s <= maxPop; s++) stack.Pop();
	    if (stack.Count == 0) Console.WriteLine("0");
	    else if (stack.Contains(parameters[2])) Console.WriteLine("true");
	    else Console.WriteLine(stack.Min());
	}
    }
}
