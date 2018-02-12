using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CryptoBlockchain
{
    class Program
    {
	static void Main()
	{
	    string pattern = @"(?:(?<symbol>{)|\[)(?:.*?(?<digits>\d{3,}).*?)(?:(?(symbol)}|\]))";
	    StringBuilder chain = new StringBuilder();
	    int lines = int.Parse(Console.ReadLine());
	    for (int l = 1; l <= lines; l++) chain.Append(Console.ReadLine());
	    MatchCollection matches = Regex.Matches(chain.ToString(), pattern);
	    StringBuilder result = new StringBuilder();
	    foreach (Match match in matches)
	    {
		int blockLength = match.Length;
		string digits = match.Groups["digits"].Value;
		if (digits.Length % 3 == 0)
		{
		    int digitsTaken = 0;
		    while (digitsTaken < digits.Length)
		    {
			int code = int.Parse(String.Join("", digits.Skip(digitsTaken).Take(3)));
			digitsTaken += 3;
			code -= blockLength;
			char symbol = (char)code;
			result.Append(symbol);
		    }
		}
	    }
	    Console.WriteLine(result.ToString());
	}
    }
}
