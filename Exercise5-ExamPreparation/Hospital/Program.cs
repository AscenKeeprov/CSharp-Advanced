using System;
using System.Collections.Generic;
using System.Linq;

namespace Hospital
{
    class Program
    {
	static void Main()
	{
	    Dictionary<string /*ward*/, List<string /*patient*/>> wards =
		new Dictionary<string, List<string>>();
	    Dictionary<string /*doctor*/, List<string /*patient*/>> doctors =
		new Dictionary<string, List<string>>();
	    string input = Console.ReadLine().Trim();
	    while (!input.ToUpper().Equals("OUTPUT"))
	    {
		string[] hospitalInfo = input.Split();
		string ward = hospitalInfo[0];
		string doctor = $"{hospitalInfo[1]} {hospitalInfo[2]}";
		string patient = hospitalInfo[3];
		if (!wards.ContainsKey(ward)) wards.Add(ward, new List<string>());
		if (wards[ward].Count < 60) wards[ward].Add(patient);
		if (!doctors.ContainsKey(doctor)) doctors.Add(doctor, new List<string>());
		doctors[doctor].Add(patient);
		input = Console.ReadLine().Trim();
	    }
	    string output = Console.ReadLine().Trim();
	    while (!output.ToUpper().Equals("END"))
	    {
		string[] printCommand = output.Split();
		if (printCommand.Length == 1)
		{
		    string ward = printCommand[0];
		    foreach (var patient in wards[ward]) Console.WriteLine(patient);
		}
		if (printCommand.Length == 2)
		{
		    if (int.TryParse(printCommand[1], out int roomNumber))
		    {
			string ward = printCommand[0];
			int patientsToSkip = 3 * (roomNumber - 1);
			foreach (var patient in wards[ward].Skip(patientsToSkip)
			    .Take(3).OrderBy(p => p)) Console.WriteLine(patient);
		    }
		    else
		    {
			string doctorName = output;
			foreach (var patient in doctors[doctorName]
			    .OrderBy(p => p)) Console.WriteLine(patient);
		    }
		}
		output = Console.ReadLine().Trim();
	    }
	}
    }
}
