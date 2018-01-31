using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace HTTPServer
{
    class Program
    {
	static void Main()
	{
	    int port = 8080;
	    TcpListener server = new TcpListener(IPAddress.Any, port);
	    server.Start();
	    Console.WriteLine($"Listening on port [{port}] ..." + Environment.NewLine);
	    while (true)
	    {
		using (NetworkStream http = server.AcceptTcpClient().GetStream())
		{
		    byte[] request = new byte[server.Server.ReceiveBufferSize];
		    http.Read(request, 0, request.Length);
		    string requestHeaders = Encoding.UTF8.GetString(request).TrimEnd('\0');
		    Console.WriteLine(requestHeaders);
		    StringBuilder html = new StringBuilder();
		    html.Append("HTTP/1.1 200 OK" + Environment.NewLine + "Content-Type:text"
			+ Environment.NewLine + Environment.NewLine);
		    if (Regex.IsMatch(requestHeaders, @"^GET\s/\s.+"))
			html.Append(File.ReadAllText("index.html"));
		    else if (Regex.IsMatch(requestHeaders, @"^GET\s/info.+"))
			html.Append(File.ReadAllText("info.html"));
		    else html.Append(File.ReadAllText("error.html"));
		    byte[] response = Encoding.UTF8.GetBytes(html.ToString());
		    http.Write(response, 0, response.Length);
		}
	    }
	}
    }
}
