using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FullDirectoryTraversal
{
    class Program
    {
	static void Main()
	{
	    Dictionary<string, Dictionary<string, double>> report =
		new Dictionary<string, Dictionary<string, double>>();
	    string path = new DirectoryInfo(Console.ReadLine()).FullName;
	    string[] subdirs = Directory.GetDirectories(path, "*", SearchOption.AllDirectories);
	    string[] directories = new string[subdirs.Length + 1];
	    directories[0] = path;
	    Array.ConstrainedCopy(subdirs, 0, directories, 1, subdirs.Length);
	    foreach (var directory in directories)
	    {
		FileInfo[] files = new DirectoryInfo(directory).GetFiles();
		foreach (var file in files)
		{
		    if (!report.ContainsKey(file.Extension.ToLower()))
			report.Add(file.Extension.ToLower(), new Dictionary<string, double>());
		    if (!report[file.Extension.ToLower()].ContainsKey(file.Name))
			report[file.Extension.ToLower()].Add(file.Name, file.Length);
		}
	    }
	    string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
	    using (StreamWriter output = new StreamWriter($"{desktop}/report.txt"))
	    {
		foreach (var fileType in report.OrderByDescending(ft => ft.Value.Count()))
		{
		    output.WriteLine($"{fileType.Key}");
		    foreach (var file in fileType.Value.OrderBy(f => f.Key))
		    {
			output.WriteLine($"--{file.Key} - {(file.Value / 1024):0.###}kb");
		    }
		}
	    }
	}
    }
}
