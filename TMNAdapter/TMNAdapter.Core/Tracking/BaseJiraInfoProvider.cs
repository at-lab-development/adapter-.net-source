using System;
using System.Collections.Generic;
using System.IO;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TMNAdapter.Core.Tracking.Attributes;

namespace TMNAdapter.Core.Tracking
{
    public class BaseJiraInfoProvider
    {
        public virtual string GetJiraTestKey()
        {
            return AnnotationTracker.GetAttributeInCallStack<BaseJiraIssueKeyAttribute>()?.Key;
        }

        public virtual IssueModel SaveAttachment(FileInfo file)
        {
            string key = GetJiraTestKey();

            if (key == null || !file.Exists)
            {
                return null;
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

            return new IssueModel()
            {
                Key = key,
                AttachmentFilePaths = new List<string>() {targetFilePath}
            };
        }

        public virtual IssueModel SaveParameter<T>(string title, T value)
        {
            string key = GetJiraTestKey();

            if (key == null)
            {
                return null;
            }

            return new IssueModel()
            {
                Key = key,
                Parameters = new List<TestParameters>()
                {
                    new TestParameters(title, value != null ? value.ToString() : "null")
                }
            };
        }

        public virtual IssueModel SaveStackTrace(string issueKey, string stackTrace)
        {
            string fileName = $"stacktrace_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.txt";
            string targetFilePath = FileUtils.WriteStackTrace(stackTrace, fileName);

            return new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() {targetFilePath}
            };
        }
    }
}
