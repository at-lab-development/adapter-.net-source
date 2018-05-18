using System;
using System.Collections.Generic;
using System.IO;
using TMNAdapter.Common;
using TMNAdapter.Common.Models;
using TMNAdapter.Entities;
using TMNAdapter.Tracking.Attributes;
using TMNAdapter.Utilities;

namespace TMNAdapter.Tracking
{
    public class JiraInfoProvider
    {
        private static Dictionary<string, List<TestParameters>> jiraKeyParameters = new Dictionary<string, List<TestParameters>>();
        private static Dictionary<string, List<string>> jiraKeyAttachments = new Dictionary<string, List<string>>();

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

        public static void CleanFor(string issueKey)
        {
            if (jiraKeyParameters.ContainsKey(issueKey))
            {
                jiraKeyParameters.Remove(issueKey);
            }

            if (jiraKeyAttachments.ContainsKey(issueKey))
            {
                jiraKeyAttachments.Remove(issueKey);
            }
        }

        public static List<TestParameters> GetIssueParameters(string issueKey)
        {
            return jiraKeyParameters.ContainsKey(issueKey) ? jiraKeyParameters[issueKey] : null;
        }

        public static List<string> GetIssueAttachments(string issueKey)
        {
            return jiraKeyAttachments.ContainsKey(issueKey) ? jiraKeyAttachments[issueKey] : null;
        }
    }
}
