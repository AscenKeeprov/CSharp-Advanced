using System;
using System.Collections.Generic;

namespace SequenceWithQueue
{
    class Program
    {
        static void Main()
        {
	    long n = long.Parse(Console.ReadLine());
	    Queue<long> queue = new Queue<long>();
	    Queue<long> buffer = new Queue<long>();
	    buffer.Enqueue(n);
	    for (int i = 1; i <= 17; i++)
	    {
		buffer.Enqueue(buffer.Peek() + 1);
		buffer.Enqueue(2 * buffer.Peek() + 1);
		buffer.Enqueue(buffer.Peek() + 2);
		queue.Enqueue(buffer.Dequeue());
	    }
	    while (queue.Count != 50)
		queue.Enqueue(buffer.Dequeue());
	    while (queue.Count != 0)
		Console.Write(queue.Dequeue() + " ");
	    Console.WriteLine();
	}
    }
}
