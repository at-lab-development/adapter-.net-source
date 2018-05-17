using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Serialization;
using TMNAdapter.Entities;

namespace TMNAdapter.Utilities
{
    // FileUtils is a util class which provides useful methods for file writing
    public class FileUtils
	{
		private readonly static string TARGET_DIR = "\\target";
		private readonly static string ATTACHMENTS_DIR = TARGET_DIR + "\\attachments";		
		  
		// Generate name of file with unique exception stack trace			
		/// <returns> Returns exception message </returns>
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

        // Writes stack trace in temporary file and save it to attachments directory
        ///<exception cref="Exception"> The exception ex for getting stacktrace </exception>  
        ///<param name="filePath"> The path for output file </param> 
        private static void WriteStackTrace(Exception ex, string filePath)
		{
			try
			{
				FileInfo file = new FileInfo(filePath);
				StreamWriter writer = File.CreateText(filePath);
				writer.WriteLine(ex.StackTrace.ToString());
				writer.Close();
				SaveFile(file);
				writer.Dispose();
			}
			catch (IOException e)
			{
				e.StackTrace.ToString();
			}
		}

        // Copy and save file to the attachments default directory.If file in the
        // default attachments directory already exists the file will be created in
        // child directory with name contains current time in milliseconds using
        // TimeInMillis method.
        /// <param name="file"> The file to save </param>  
        /// <returns> The path where file was actually saved </returns>
        /// <exception cref="IOException"></exception>
        public static string SaveFile(FileInfo file)
        {
            try
            {
                string fileName = file.Name;
                string relativeFilePath = ATTACHMENTS_DIR;
                string currentDirectory = Directory.GetCurrentDirectory();
                string copyPath = currentDirectory + relativeFilePath;

                if (File.Exists(Path.Combine(copyPath, fileName)))
                {
                    string newDirectory = TimeInMillis().ToString();
                    copyPath = Path.Combine(copyPath, newDirectory);
                    relativeFilePath = Path.Combine(relativeFilePath, newDirectory);

                    Directory.CreateDirectory(Path.Combine(copyPath));
                }

                file.CopyTo(Path.Combine(copyPath, fileName), true);

                return Path.Combine(relativeFilePath, fileName);
            }
            catch (IOException exception)
            {
                Debug.WriteLine($"{exception.Message} \n {exception.StackTrace}");
                return null;
            }
        }

        // Parse xml file using XmlSerializer. The entities for serialization are the same as in
        // Test Management Jira plugin.
        /// <param name="result"> The list of issues for writing </param>
        /// <param name="relativefilePath"> The path to output file </param>
        public static void WriteXml(TestResult result, String relativefilePath)
        {
            string testResultDir = Path.Combine(TestContext.CurrentContext.WorkDirectory, TARGET_DIR);            
            if (!Directory.Exists(testResultDir))
            {
                Directory.CreateDirectory(testResultDir);
            }

            XmlSerializer formatter = new XmlSerializer(typeof(TestResult));
            using (FileStream fs = new FileStream(Path.Combine(testResultDir, relativefilePath), FileMode.Create))
            {
                formatter.Serialize(fs, result);
            }
        }

        static string GetTargetDir() => TARGET_DIR;

        public static string GetAttachmentsDir() => ATTACHMENTS_DIR;

        private static long TimeInMillis() => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}
