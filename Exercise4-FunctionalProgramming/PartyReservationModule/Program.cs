using System;
using System.Collections.Generic;
using System.Linq;

namespace PartyReservationModule
{
    class Program
    {
	static void Main()
	{
	    Func<List<string>, string, List<string>> filterStartsWith = (list, criterion)
		=> list.Where(item => !item.StartsWith(criterion)).ToList();
	    Func<List<string>, string, List<string>> filterEndsWith = (list, criterion)
		=> list.Where(item => !item.EndsWith(criterion)).ToList();
	    Func<List<string>, string, List<string>> filterByLength = (list, criterion)
		=> list.Where(item => !item.Length.Equals(int.Parse(criterion))).ToList();
	    Func<List<string>, string, List<string>> filterContains = (list, criterion)
		=> list.Where(item => !item.Contains(criterion)).ToList();
	    List<string> guests = Console.ReadLine().Split().ToList();
	    List<string> filters = new List<string>();
	    string input;
	    while (!(input = Console.ReadLine()).Equals("Print"))
	    {
		string[] command = input.Split(new string[] { "filter;", "with;", ";", " " },
		    StringSplitOptions.RemoveEmptyEntries).ToArray();
		string action = command[0];
		string filter = $"{command[1]}+{command[2]}";
		if (action == "Add") filters.Add(filter);
		if (action == "Remove") filters.RemoveAll(f => f == filter);
	    }
	    while (filters.Count != 0)
	    {
		string filterType = filters[0].Split(new char[] { '+' },
		    StringSplitOptions.RemoveEmptyEntries)[0];
		string criterion = filters[0].Split(new char[] { '+' },
		    StringSplitOptions.RemoveEmptyEntries)[1];
		switch (filterType)
		{
		    case "Starts":
			guests = filterStartsWith(guests, criterion);
			break;
		    case "Ends":
			guests = filterEndsWith(guests, criterion);
			break;
		    case "Length":
			guests = filterByLength(guests, criterion);
			break;
		    case "Contains":
			guests = filterContains(guests, criterion);
			break;
		}
		filters.RemoveAt(0);
	    }
	    Console.WriteLine(String.Join(" ", guests));
	}
    }
}
