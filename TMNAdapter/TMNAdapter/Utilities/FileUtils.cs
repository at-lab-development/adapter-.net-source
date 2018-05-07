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

        public static string Save(Exception ex)
        {
            string message = null;
            if (ex != null)
            {
                DateTime time = new DateTime();
                string filePath = string.Format("stacktrace_%s.txt", time.ToShortTimeString().Replace(":", "-"));
                string exceptionMessage = ex.ToString();
                if (exceptionMessage.Contains("\n"))
                    exceptionMessage = exceptionMessage.Substring(0, exceptionMessage.IndexOf('\n'));
                WriteStackTrace(ex, filePath);
                message = "Failed due to: " + ex.Data.ToString() + ": " + exceptionMessage
                        + ".\nFull stack trace attached as " + filePath;
            }
            return message;
        }

        private static void WriteStackTrace(Exception ex, string filePath)
        {
            try
            {
                FileInfo file = new FileInfo(ATTACHMENTS_DIR + "\\stacktrace.tmp");
                StreamWriter writer = File.CreateText(ATTACHMENTS_DIR + "\\stacktrace.tmp");
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
    
        public static void WriteXml(Issues issues, String filePath)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Issues));          

            using (FileStream fs = new FileStream("." + TARGET_DIR + filePath, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, issues);
            }

        }

        static string GetTargetDir() => TARGET_DIR;
        public static string GetAttachmentsDir() => ATTACHMENTS_DIR;

        private static long TimeInMillis() => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

    }
}
