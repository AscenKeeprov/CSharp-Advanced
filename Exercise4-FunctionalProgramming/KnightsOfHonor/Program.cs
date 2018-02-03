using System;

namespace KnightsOfHonor
{
    class Program
    {
        static void Main()
        {
	    Action<string> dubKnight = name => Console.WriteLine($"Sir {name}");
	    string[] names = Console.ReadLine().Split();
	    foreach (string name in names) dubKnight(name);
	}
    }
}
