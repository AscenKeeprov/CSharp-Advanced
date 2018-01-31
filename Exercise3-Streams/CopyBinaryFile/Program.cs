using System.IO;

namespace CopyBinaryFile
{
    class Program
    {
	static void Main()
	{
	    using (FileStream image = new FileStream("original.png", FileMode.Open))
	    {
		using (FileStream imageCopy = new FileStream("copy.png", FileMode.Create))
		{
		    byte[] buffer = new byte[image.Length];
		    int imageBytes = image.Read(buffer, 0, buffer.Length);
		    imageCopy.Write(buffer, 0, buffer.Length);
		}
	    }
	}
    }
}
