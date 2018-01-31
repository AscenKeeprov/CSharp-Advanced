using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace ZipFileSlices
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
	    SliceAndZip(sourceFile, outputDir, parts);
	    ListSlices(outputDir, slices);
	    UnzipAndAssemble(slices, outputDir, sourceDir);
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

	private static void SliceAndZip(string sourceFile, string outputDir, int parts)
	{
	    using (FileStream file = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
	    {
		string fileExtension = sourceFile.Substring(sourceFile.LastIndexOf('.'));
		int bufferSize = (int)Math.Ceiling((double)file.Length / parts);
		byte[] buffer = new byte[bufferSize];
		for (int s = 0; s < parts; s++)
		{
		    using (FileStream fileSlices = new FileStream($"{outputDir}/Part-{s}{fileExtension}.gz", FileMode.Create))
		    {
			using (GZipStream zip = new GZipStream(fileSlices, CompressionLevel.Fastest))
			{
			    int slice = file.Read(buffer, 0, bufferSize);
			    zip.Write(buffer, 0, slice);
			}
		    }
		}
	    }
	}

	private static void ListSlices(string outputDir, List<string> files)
	{
	    FileInfo[] contents = new DirectoryInfo(outputDir).GetFiles("*.gz");
	    foreach (FileInfo file in contents) files.Add(file.Name);
	}

	private static void UnzipAndAssemble(List<string> slices, string outputDir, string sourceDir)
	{
	    using (FileStream assembledFile = new FileStream($"{sourceDir}assembled.mp4", FileMode.Create))
	    {
		foreach (string slice in slices)
		{
		    using (FileStream filePart = new FileStream($"{outputDir}/{slice}", FileMode.Open))
		    {
			using (GZipStream unzip = new GZipStream(filePart, CompressionMode.Decompress))
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
}
