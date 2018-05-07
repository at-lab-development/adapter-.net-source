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

        public static string Save()
        {
            return null;
        }

        private static void WriteStackTrace()
        {

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

        private static long TimeInMillis() => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    
    }
}
