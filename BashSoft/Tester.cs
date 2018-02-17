using System;
using System.IO;

namespace BashSoft
{
    public static class Tester	    /* JUDGE SIMULATOR */
    {
	public static void CompareContents(string userOutputPath, string expectedOutputPath)
	{
	    string userOutputFile = IOManager.ExtractFileName(userOutputPath);
	    string expectedOutputFile = IOManager.ExtractFileName(expectedOutputPath);
	    userOutputPath = IOManager.BuildAbsolutePath(userOutputPath);
	    expectedOutputPath = IOManager.BuildAbsolutePath(expectedOutputPath);
	    Console.WriteLine("Reading files...");
	    try
	    {
		string[] actualOutputLines = File.ReadAllLines($"{userOutputPath}\\{userOutputFile}");
		string[] expectedOutputLines = File.ReadAllLines($"{expectedOutputPath}\\{expectedOutputFile}");
		Console.WriteLine("Files read!");
		bool hasMismatch;
		string[] mismatches = GetLinesWithPossibleMismatches(
		    actualOutputLines, expectedOutputLines, out hasMismatch);
		Console.WriteLine("Comparison results:");
		Console.WriteLine($"░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
		string mismatchPath = $"{expectedOutputPath}\\mismatches.txt";
		PrintOutput(mismatches, hasMismatch, mismatchPath);
		Console.WriteLine($"░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░");
	    }
	    catch (Exception exception)
	    {
		if (exception is DirectoryNotFoundException || exception is FileNotFoundException)
		    IOManager.DisplayAlert(Exceptions.InvalidPath);
	    }
	}

	private static string[] GetLinesWithPossibleMismatches(
	    string[] actualOutputLines, string[] expectedOutputLines, out bool hasMismatch)
	{
	    hasMismatch = false;
	    string output = String.Empty;
	    Console.WriteLine("Comparing files...");
	    int minOutputLines = actualOutputLines.Length;
	    if (actualOutputLines.Length != expectedOutputLines.Length)
	    {
		hasMismatch = true;
		minOutputLines = Math.Min(actualOutputLines.Length, expectedOutputLines.Length);
		IOManager.DisplayAlert(Exceptions.InevitableMismatch);
	    }
	    string[] mismatches = new string[minOutputLines];
	    for (int index = 0; index < minOutputLines; index++)
	    {
		string actualLine = actualOutputLines[index];
		string expectedLine = expectedOutputLines[index];
		if (!actualLine.Equals(expectedLine))
		{
		    output = $"■ Mismatch at line {index} ─ Expected: \"{expectedLine}\"" +
			$" <=> Actual: \"{actualLine}\"{Environment.NewLine}";
		    hasMismatch = true;
		}
		else output = $"{actualLine}{Environment.NewLine}";
		mismatches[index] = output;
	    }
	    return mismatches;
	}

	private static void PrintOutput(string[] mismatches, bool hasMismatch, string mismatchPath)
	{
	    if (hasMismatch)
	    {
		foreach (var line in mismatches) Console.WriteLine($"░ {line}░");
		File.WriteAllLines(mismatchPath, mismatches);
		return;
	    }
	    else Console.WriteLine("Files are identical. There are no mismatches.");
	}
    }
}
