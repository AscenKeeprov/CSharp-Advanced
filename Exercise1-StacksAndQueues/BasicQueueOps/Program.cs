using System;
using System.Collections.Generic;
using System.Linq;

namespace BasicQueueOps
{
    class Program
    {
        static void Main()
        {
	    int[] parameters = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    Queue<int> queue = new Queue<int>(parameters[0]);
	    int[] numbers = Console.ReadLine()
		.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
		.Select(int.Parse).ToArray();
	    for (int n = 0; n < parameters[0]; n++) queue.Enqueue(numbers[n]);
	    int maxPop = Math.Min(parameters[1], queue.Count);
	    for (int s = 1; s <= maxPop; s++) queue.Dequeue();
	    if (queue.Count == 0) Console.WriteLine("0");
	    else if (queue.Contains(parameters[2])) Console.WriteLine("true");
	    else Console.WriteLine(queue.Min());
	}
    }
}
