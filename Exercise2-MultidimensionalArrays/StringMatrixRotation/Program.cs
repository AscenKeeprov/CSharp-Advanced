using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace StringMatrixRotation
{
    class Program
    {
	static void Main()
	{
	    int degrees = int.Parse(Regex.Match(Console.ReadLine(), @"Rotate\((\d+)\)").Groups[1].Value);
	    int rotations = (degrees / 90) % 4;
	    List<string> lines = new List<string>();
	    int matrixWidth = 0;
	    string line;
	    while ((line = Console.ReadLine()) != "END")
	    {
		lines.Add(line);
		if (line.Length > matrixWidth) matrixWidth = line.Length;
	    }
	    char[,] matrix = new char[lines.Count, matrixWidth];
	    for (int r = 0; r < matrix.GetLength(0); r++)
		for (int c = 0; c < matrix.GetLength(1); c++)
		{
		    if (c < lines[r].Length) matrix[r, c] = lines[r][c];
		    else matrix[r, c] = ' ';
		}
	    int rotatedHeight = matrix.GetLength(0);
	    int rotatedWidth = matrix.GetLength(1);
	    if (rotations % 2 != 0)
	    {
		rotatedHeight = matrix.GetLength(1);
		rotatedWidth = matrix.GetLength(0);
	    }
	    char[,] rotatedMatrix = new char[rotatedHeight, rotatedWidth];
	    if (rotations != 0)
	    {
		Rotate(matrix, rotations, rotatedMatrix);
		Print(rotatedMatrix);
	    }
	    else Print(matrix);
	}

	private static void Rotate(char[,] matrix, int rotations, char[,] rotatedMatrix)
	{
	    int height = rotatedMatrix.GetLength(0);
	    int width = rotatedMatrix.GetLength(1);
	    switch (rotations)
	    {
		case 1:
		    for (int h = 0; h < height; h++)
			for (int w = 0; w < width; w++)
			    rotatedMatrix[h, w] = matrix[width - 1 - w, h];
		    break;
		case 2:
		    for (int h = 0; h < height; h++)
			for (int w = 0; w < width; w++)
			    rotatedMatrix[h, w] = matrix[height - 1 - h, width - 1 - w];
		    break;
		case 3:
		    for (int h = 0; h < height; h++)
			for (int w = 0; w < width; w++)
			    rotatedMatrix[h, w] = matrix[w, height - 1 - h];
		    break;
	    }
	}

	private static void Print(char[,] rotatedMatrix)
	{
	    for (int r = 0; r < rotatedMatrix.GetLength(0); r++)
	    {
		for (int c = 0; c < rotatedMatrix.GetLength(1); c++)
		    Console.Write(rotatedMatrix[r, c]);
		Console.WriteLine();
	    }
	}
    }
}
