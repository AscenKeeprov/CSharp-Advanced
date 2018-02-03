using System;
using System.Collections.Generic;
using System.Linq;

namespace PredicateParty
{
    class Program
    {
        static void Main()
        {
	    Func<List<string>, string, List<string>> filterStartsWith = (list, criterion)
		=> list.Where(g => g.StartsWith(criterion)).ToList();
	    Func<List<string>, string, List<string>> filterEndsWith = (list, criterion)
		=> list.Where(g => g.EndsWith(criterion)).ToList();
	    Func<List<string>, string, List<string>> filterByLength = (list, criterion)
		=> list.Where(g => g.Length.Equals(int.Parse(criterion))).ToList();
	    Func<List<string>, string> print = list => (list.Count == 0) 
	    ? "Nobody is going to the party!" : $"{String.Join(", ", list)} are going to the party!";
	    List<string> guestList = Console.ReadLine().Split().ToList();
	    List<string> filteredGuests = new List<string>();
	    string command;
	    while (!(command = Console.ReadLine()).Equals("Party!"))
	    {
		string action = command.Split().ToArray()[0];
		string filter = command.Split().ToArray()[1];
		string criterion = command.Split().ToArray()[2];
		switch (action)
		{
		    case "Double":
			switch (filter)
			{
			    case "StartsWith":
				filteredGuests = filterStartsWith(guestList, criterion);
				break;
			    case "EndsWith":
				filteredGuests = filterEndsWith(guestList, criterion);
				break;
			    case "Length":
				filteredGuests = filterByLength(guestList, criterion);
				break;
			}
			for (int g = 0; g < guestList.Count; g++)
			    if (filteredGuests.Contains(guestList[g]))
			    {
				guestList.Insert(g, guestList[g]);
				g++;
			    }
			break;
		    case "Remove":
			switch (filter)
			{
			    case "StartsWith":
				filteredGuests = filterStartsWith(guestList, criterion);
				break;
			    case "EndsWith":
				filteredGuests = filterEndsWith(guestList, criterion);
				break;
			    case "Length":
				filteredGuests = filterByLength(guestList, criterion);
				break;
			}
			guestList = guestList.Except(filteredGuests).ToList();
			break;
		}
	    }
	    Console.WriteLine(print(guestList));
        }
    }
}
