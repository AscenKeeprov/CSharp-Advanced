using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TraverseDirectory
{
    class Program
    {
	static void Main()
	{
	    Dictionary<string, Dictionary<string, double>> report =
		new Dictionary<string, Dictionary<string, double>>();
	    string directory = Console.ReadLine();
	    FileInfo[] dirContents = new DirectoryInfo(directory).GetFiles();
	    foreach (var file in dirContents)
	    {
		if (!report.ContainsKey(file.Extension))
		    report.Add(file.Extension, new Dictionary<string, double>());
		report[file.Extension].Add(file.Name, file.Length);
	    }
	    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
	    using (StreamWriter output = new StreamWriter($"{desktop}/report.txt"))
	    {
		foreach (var fileType in report.OrderByDescending(ft => ft.Value.Count()))
		{
		    output.WriteLine($"{fileType.Key}");
		    foreach (var file in fileType.Value.OrderBy(f => f.Key))
			output.WriteLine($"--{file.Key} - {(file.Value / 1024):0.###}kb");
		}
	    }
	}
    }
}
