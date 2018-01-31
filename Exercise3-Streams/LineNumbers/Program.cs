using System;
using System.IO;

namespace LineNumbers
{
    class Program
    {
        static void Main()
        {
            using (StreamReader text = new StreamReader("text.txt"))
	    {
		using (StreamWriter output = new StreamWriter("output.txt"))
		{
		    int lineNumber = 1;
		    string line;
		    while ((line = text.ReadLine()) != null)
		    {
			line = "Line " + lineNumber + ": " + line;
			output.Write(line + Environment.NewLine);
			lineNumber++;
		    }
		}
	    }
        }
    }
}
