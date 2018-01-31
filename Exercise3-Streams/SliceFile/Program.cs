using System;
using System.Collections.Generic;
using System.IO;

namespace SliceFile
{
    class Program
    {
	static void Main()
	{
	    List<string> slices = new List<string>();
	    int parts = int.Parse(Console.ReadLine());
	    string sourceDir = "./";
	    string sourceFile = "../original.mp4";
	    string outputDir = "./slices";
	    Cleanup(outputDir);
	    Slice(sourceFile, outputDir, parts);
	    ListSlices(outputDir, slices);
	    Assemble(slices, outputDir, sourceDir);
	}

	private static void Cleanup(string outputDir)
	{
	    if (Directory.Exists(outputDir))
	    {
		FileInfo[] contents = new DirectoryInfo(outputDir).GetFiles();
		foreach (FileInfo file in contents) file.Delete();
		Directory.Delete(outputDir);
	    }
	    Directory.CreateDirectory(outputDir);
	}

	private static void Slice(string sourceFile, string outputDir, int parts)
	{
	    using (FileStream file = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
	    {
		string fileExtension = sourceFile.Substring(sourceFile.LastIndexOf('.'));
		int bufferSize = (int)Math.Ceiling((double)file.Length / parts);
		byte[] buffer = new byte[bufferSize];
		for (int s = 0; s < parts; s++)
		{
		    string sliceName = $"{outputDir}/Part-{s}{fileExtension}";
		    using (FileStream fileSlices = new FileStream(sliceName, FileMode.Create))
		    {
			int slice = file.Read(buffer, 0, bufferSize);
			fileSlices.Write(buffer, 0, slice);
		    }
		}
	    }
	}

	private static void ListSlices(string outputDir, List<string> files)
	{
	    FileInfo[] contents = new DirectoryInfo(outputDir).GetFiles();
	    foreach (FileInfo file in contents) files.Add(file.Name);
	}

	private static void Assemble(List<string> slices, string outputDir, string sourceDir)
	{
	    string fileExtension = slices[0].Substring(slices[0].LastIndexOf('.'));
	    using (FileStream assembledFile = new FileStream($"{sourceDir}assembled{fileExtension}", FileMode.Create))
	    {
		foreach (string slice in slices)
		{
		    using (FileStream filePart = new FileStream($"{outputDir}/{slice}", FileMode.Open))
		    {
			byte[] buffer = new byte[filePart.Length];
			int part = filePart.Read(buffer, 0, buffer.Length);
			assembledFile.Write(buffer, 0, part);
		    }
		}
	    }
	}
    }
}
