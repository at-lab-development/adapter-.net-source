using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMNAdapter.Utilities
{
	public class FileUtils
	{
		private readonly static string TARGET_DIR = "\\target\\";
		private readonly static string ATTACHMENTS_DIR = TARGET_DIR + "attachments\\";
		
		  
		 //Generate name of file with unique exception stack trace
			
		/// <returns> Returns exception message  </returns>
		public static string GetExceptionMessage(Exception ex)
		{
			string message = string.Empty;
			if (ex != null)
			{
				string filePath = ($"stacktrace_{DateTime.Now.ToString("yyyy-MM-ddTHH-mm-ss.fff")}.txt");
				string exceptionMessage = ex.ToString();
				if (exceptionMessage.Contains("\n"))
					exceptionMessage = exceptionMessage.Substring(0, exceptionMessage.IndexOf('\n'));
				WriteStackTrace(ex, filePath);
				message = "Failed due to: "  + exceptionMessage
						+ ".\nFull stack trace attached as " + filePath;
			}
			return message;
		}

	 /*
     * Writes stack trace in temporary file and save it to attachments directory
     * Exception ex The exception for getting stacktrace
     * filePath The path for output file
     */

		private static void WriteStackTrace(Exception ex, string filePath)
		{
			try
			{
				FileInfo file = new FileInfo("stacktrace.tmp");
				StreamWriter writer = File.CreateText("stacktrace.tmp");
				writer.WriteLine(ex.StackTrace.ToString());
				writer.Close();
				SaveFile(file, filePath);
				writer.Dispose();
			}
			catch (IOException e)
			{
				e.StackTrace.ToString();
			}
		}

		public static string SaveFile(FileInfo file, string newFilePath)
		{
			try
			{
				string relativeFilePath = ATTACHMENTS_DIR;
				string copyPath = "." + relativeFilePath + newFilePath;
				FileInfo copy = new FileInfo(copyPath);
				if (copy.Exists)
				{
					relativeFilePath += TimeInMillis() + "\\";
					copyPath = "." + relativeFilePath + newFilePath;
					copy = new FileInfo(copyPath);
				}
				file.CopyTo(copyPath, true);
				return relativeFilePath + newFilePath;
			}
			catch (IOException)
			{
				return null;
			}
		}

		public static void WriteXml()
		{

		}

		static string GetTargetDir() => TARGET_DIR;
		public static string GetAttachmentsDir() => ATTACHMENTS_DIR;
	}
}
