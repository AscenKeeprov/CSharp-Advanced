using System;
using System.IO;

namespace OddLines
{
    class Program
    {
        static void Main()
        {
            using (StreamReader text = new StreamReader("text.txt"))
	    {
		int lineNumber = 0;
		string line;
		while ((line = text.ReadLine()) != null)
		{
		    if (lineNumber % 2 != 0) Console.WriteLine(line);
		    lineNumber++;
		}
	    }
        }
    }
}
