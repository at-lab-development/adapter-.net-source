﻿using System;
using System.Diagnostics;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Core.Common
{
    // FileUtils is a util class which provides useful methods for file writing
    public class FileUtils
    {
        private const string TARGET_DIR = "\\target";
        private const string ATTACHMENTS_DIR = TARGET_DIR + "\\attachments";
        private static string solution_dir;

        public static string Solution_dir { get => solution_dir; set => solution_dir = value; }

        /// <summary>
        /// Writes stack trace in temporary file and save it to attachments directory
        /// </summary>
        /// <param name="stackTrace">Text contents about stacktrace of exception</param>
        /// <param name="fileName"> The name for output file </param>
        /// <exception cref="IOException">Is thrown, when I/O error occurs</exception>
        public static string WriteStackTrace(string stackTrace, string fileName)
        {
            try
            {
                string tempfilePath = Path.GetTempFileName();
                var file = new FileInfo(tempfilePath);

                string newFilePath = tempfilePath.Replace(file.Name, fileName);
                file.MoveTo(newFilePath);

                StreamWriter writer = File.CreateText(newFilePath);
                writer.WriteLine(stackTrace);
                writer.Close();
                string targetFilePath = SaveFile(file);
                writer.Dispose();

                return targetFilePath;

            }
            catch (IOException e)
            {
                Debug.WriteLine($"Message: {e.Message}\n " +
                                $"StackTrace: {e.StackTrace}");
                return null;
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
                string copyPath = solution_dir + relativeFilePath;

                CheckOrCreateDir(copyPath);

                if (File.Exists(Path.Combine(copyPath, fileName)))
                {
                    string newDirectory = TimeInMillis().ToString();
                    copyPath = Path.Combine(copyPath, newDirectory);
                    relativeFilePath = Path.Combine(relativeFilePath, newDirectory);

                    Directory.CreateDirectory(copyPath);
                }

                file.CopyTo(Path.Combine(copyPath, fileName), true);

                return Path.Combine(relativeFilePath, fileName);
            }
            catch (IOException exception)
            {
                Debug.WriteLine($"{exception.Message}\n " +
                                $"{exception.StackTrace}");
                return null;
            }
        }

        // Parse xml file using XmlSerializer. The entities for serialization are the same as in
        // Test Management Jira plugin.
        /// <param name="result"> The list of issues for writing </param>
        /// <param name="relativefilePath"> The path to output file </param>
        public static void WriteXml(TestResult result, string relativefilePath)
        {
            string testResultDir = solution_dir + TARGET_DIR;
            CheckOrCreateDir(testResultDir);

            var doc = new XmlDocument();
            StringWriter writer = new StringWriter();
            XmlSerializer formatter = new XmlSerializer(typeof(TestResult));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");

            formatter.Serialize(writer, result, ns);
            doc.Load(new StringReader(writer.ToString()));

            if (doc.FirstChild is XmlDeclaration dec)
            {
                dec.Encoding = "UTF-8";
                dec.Standalone = "yes";
            }

            doc.Save(Path.Combine(testResultDir, relativefilePath));
        }

        private static void CheckOrCreateDir(string Dir)
        {
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
        }

        static string GetTargetDir() => TARGET_DIR;

        public static string GetAttachmentsDir() => ATTACHMENTS_DIR;

        private static long TimeInMillis() => (long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }
}
