using System;
using System.IO;

namespace DirectoryList
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			try
			{
				string inPath = null;
				inPath = GetFilePath();
				if (string.IsNullOrEmpty(inPath))
				{
					throw new ArgumentNullException();
				}
				//Check this is a valid file path
				if (Directory.Exists(inPath))
				{
					Directory.SetCurrentDirectory(inPath);
					//Create a new file to write to
					using (StreamWriter sw = File.CreateText(inPath + "//txtList.txt"))
					{
						sw.WriteLine("Below is a list of all files in " + inPath);
						sw.WriteLine("");
						var txtFiles = Directory.EnumerateFiles(inPath);
						foreach (string strCurrentFile in txtFiles)
						{
							string strFileName = strCurrentFile.Substring(inPath.Length + 1);
							//Now get the file size in the correct size format
							string[] sizes = { "B", "KB", "MB", "GB", "TB" };
							double dblLen = new FileInfo(strCurrentFile).Length;
							int order = 0;
							while (dblLen >= 1024 && order<sizes.Length - 1) 
							{
    							order++;
    							dblLen = dblLen/1024;
							}
							string strResult = String.Format("{0:0.##}{1}", dblLen, sizes[order]);

							sw.WriteLine(strFileName + "  "+ strResult);
						}
					}
						
				}
				else
				{
					throw new DirectoryNotFoundException(inPath);
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			return;
			   
		}
		private static string GetFilePath()
		{
			string strFilePath = null;
			try
			{
				Console.WriteLine("Please enter a file path");
				strFilePath = Console.ReadLine();
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return strFilePath;
		}
	}
}
