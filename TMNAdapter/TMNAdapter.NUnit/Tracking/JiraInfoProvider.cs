using System;
using System.Collections.Generic;
using System.IO;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TMNAdapter.Tracking.Attributes;

namespace TMNAdapter.Tracking
{
    public class JiraInfoProvider
    {
        internal static string GetJiraTestKey()
        {
            return AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
        }

        public static void SaveAttachment(FileInfo file)
        {
            string key = GetJiraTestKey();

            if (key == null || !file.Exists)
            {
                return;
            }

            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string targetFilePath = null;

            try
            {
                string filePath = file.FullName;
                bool isOutOfAttachmentsDir = !filePath.StartsWith(currentDirectory + FileUtils.GetAttachmentsDir());

                targetFilePath = isOutOfAttachmentsDir ?
                                   FileUtils.SaveFile(file) :
                                   filePath.Replace(currentDirectory, String.Empty);
            }
            catch (IOException ex)
            {
                Console.Write(ex.StackTrace);
            }

            IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                AttachmentFilePaths = new List<string>() { targetFilePath }
            });
        }

        public static void SaveParameter<T>(string title, T value)
        {
            string key = GetJiraTestKey();

            if (key == null)
            {
                return;
            }

            IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Parameters = new List<TestParameters>()
                {
                    new TestParameters(title, value != null ? value.ToString() : "null")
                }
            });
        }

        public static void SaveStackTrace(string issueKey, string stackTrace)
        {
            string fileName = $"stacktrace_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.txt";
            string targetFilePath = FileUtils.WriteStackTrace(stackTrace, fileName);

            IssueManager.AddIssue(new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() {targetFilePath}
            });
        }
    }
}
