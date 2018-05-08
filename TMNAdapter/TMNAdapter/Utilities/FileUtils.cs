using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TMNAdapter.Entities;

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

        /// Copy and save file to the attachments default directory.If file in the
        /// default attachments directory already exists the file will be created in
        /// child directory with name contains current time in milliseconds using
        /// TimeInMillis method.
        /// <param name="file">the file to save</param>  
        /// <param name="newFilePath">the path relative to attachments dir</param>  
        ///  <returns>the path where file was actually saved </returns> 
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

        /// Parse xml file using XmlSerializer. The entities for serialization are the same as in
        /// Test Management Jira plugin.
        /// <param name="result"> the list of issues for writing</param>
        /// <param name="filePath">the path to output file</param>
        public static void WriteXml(TestResult result, String filePath)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(TestResult));
            using (FileStream fs = new FileStream("." + TARGET_DIR + filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, result);
            }
        }

        static string GetTargetDir() => TARGET_DIR;

        public static string GetAttachmentsDir() => ATTACHMENTS_DIR;

        private static long TimeInMillis() => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}
