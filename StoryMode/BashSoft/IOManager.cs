using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace BashSoft
{
    public static class IOManager	    /* PROCESSES INPUT/OUTPUT DATA */
    {
	public static void DisplayWelcome()
	{
	    Console.Write(Environment.NewLine);
	    Console.WriteLine(File.ReadAllText("resources/welcome.txt"));
	    Console.Write(Environment.NewLine);
	}

	public static void DisplayHelp()
	{
	    Console.Clear();
	    Console.WriteLine(File.ReadAllText("resources/help.txt"));
	}

	public static void DisplayAlert(string message, string input = null)
	{
	    ConsoleColor oldForegroundColor = Console.ForegroundColor;
	    Console.ForegroundColor = ConsoleColor.Red;
	    if (input == null) Console.WriteLine(message);
	    else Console.WriteLine($"{input}{message}");
	    Console.ForegroundColor = oldForegroundColor;
	}

	public static void DisplayGoodbye()
	{
	    Console.WriteLine($" Thank you for using BashSoft!");
	    Console.Write(Environment.NewLine);
	    Thread.Sleep(4000);
	    Environment.Exit(0);
	}

	public static void TraverseDirectory(int depth)
	{
	    string startDir = Directory.GetCurrentDirectory();
	    int startDirLevel = startDir.Split('\\').Length;
	    Queue<string> dirTree = new Queue<string>();
	    dirTree.Enqueue(startDir);
	    while (dirTree.Count != 0)
	    {
		string currentDir = dirTree.Dequeue();
		int currentDirLevel = currentDir.Split('\\').Length;
		if (currentDirLevel - startDirLevel == depth + 1) break;
		try
		{
		    string[] currentDirSubdirs = Directory.GetDirectories(currentDir);
		    string[] currentDirFiles = Directory.GetFiles(currentDir);
		    if (currentDirSubdirs.Length == 0 && currentDirFiles.Length == 0)
			Console.WriteLine($"{new string(' ', currentDirLevel - startDirLevel)}{currentDir}");
		    else
		    {
			Console.WriteLine($"┌{new string('─', currentDirLevel - startDirLevel)}{currentDir}");
			DisplayDirectorySubdirectories(currentDirSubdirs, currentDirFiles, dirTree);
			DisplayDirectoryFiles(currentDirFiles);

		    }
		}
		catch (UnauthorizedAccessException)
		{
		    IOManager.DisplayAlert(Exceptions.UnauthorizedAccess);
		}
	    }
	}

	private static void DisplayDirectorySubdirectories(
	    string[] subdirs, string[] currentDirFiles, Queue<string> dirTree)
	{
	    for (int s = 0; s < subdirs.Length; s++)
	    {
		dirTree.Enqueue(subdirs[s]);
		int subDirLevel = subdirs[s].LastIndexOf('\\');
		string subDirName = subdirs[s].Substring(subDirLevel);
		string indentation = $"├{new string('─', subDirLevel)}";
		if (s == subdirs.Length - 1)
		{
		    if (currentDirFiles.Length > 0)
			indentation = $"├{new string('─', subDirLevel)}";
		    else indentation = $"└{new string('─', subDirLevel)}";
		}
		Console.WriteLine($"{indentation}{subDirName}");
	    }
	}

	private static void DisplayDirectoryFiles(string[] files)
	{
	    for (int f = 0; f < files.Length; f++)
	    {
		int fileLevel = files[f].LastIndexOf('\\');
		string fileName = files[f].Substring(fileLevel + 1);
		string indentation = $"├{new string('─', fileLevel)}";
		if (f == files.Length - 1) indentation = $"└{new string('─', fileLevel)}";
		Console.WriteLine($"{indentation}{fileName}");
	    }
	}

	public static string BuildAbsolutePath(string path)
	{
	    string currentDir = Directory.GetCurrentDirectory();
	    string absolutePath = path.Replace('/', '\\').Trim('\\');
	    int pathEnd;
	    if (!String.IsNullOrEmpty(ExtractFileName(absolutePath)))
	    {
		pathEnd = absolutePath.LastIndexOf('\\');
		if (pathEnd == -1) absolutePath = currentDir;
		else absolutePath = absolutePath.Substring(0, pathEnd);
	    }
	    if (!absolutePath.Contains(':'))
	    {
		if (Regex.IsMatch(absolutePath, @"(?:\.{1,2}\\*)+"))
		{
		    string[] currentPath = currentDir.Split('\\');
		    string targetDir = String.Empty;
		    if (absolutePath.Contains('\\'))
		    {
			int targetDirIndex = Math.Min(absolutePath.LastIndexOf('.') + 2, absolutePath.Length);
			targetDir = absolutePath.Substring(targetDirIndex);
		    }
		    if (!String.IsNullOrEmpty(targetDir)) absolutePath = absolutePath.Replace(targetDir, String.Empty);
		    int levelsUp = absolutePath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries).Length;
		    if (absolutePath == ".") levelsUp = 0;
		    if (levelsUp < currentPath.Length)
			absolutePath = $"{String.Join("\\", currentPath.Take(currentPath.Length - levelsUp))}\\{targetDir}";
		    else absolutePath = $"{currentPath[0]}\\{targetDir}";
		}
		else absolutePath = $"{currentDir}\\{absolutePath}";
	    }
	    return absolutePath;
	}

	public static string ExtractFileName(string path)
	{
	    string pattern = @"[^\/\s\\]+\.[^\/\s\\]+(?:\.[^\/\s\\]+)*";
	    string fileName = String.Empty;
	    if (Regex.IsMatch(path, pattern))
		fileName = Regex.Match(path, pattern).ToString();
	    return fileName;
	}

	public static void PrintDatabaseReport(Dictionary<string, Dictionary<string, List<int>>> report, string order)
	{
	    Console.WriteLine($"Generating report...");
	    Console.WriteLine($"░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
	    foreach (var record in report)
	    {
		string course = record.Key;
		Console.WriteLine($"░{course}:");
		Dictionary<string, List<int>> studentData = record.Value;
		if (order.Equals("ASCENDING"))
		{
		    foreach (var student in studentData.OrderBy(s => s.Value.Average()))
		    {
			string username = student.Key;
			List<int> scores = student.Value;
			Console.WriteLine($"░ {username} ─ {String.Join("·", scores)} │ Average: {scores.Average():F0}");
		    }
		}
		else if (order.Equals("DESCENDING"))
		{
		    foreach (var student in studentData.OrderByDescending(s => s.Value.Average()))
		    {
			string username = student.Key;
			List<int> scores = student.Value;
			Console.WriteLine($"░ {username} ─ {String.Join("·", scores)} │ Average: {scores.Average():F0}");
		    }
		}
	    }
	    Console.WriteLine($"░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
	}
    }
}
