using System;
using System.Collections.Generic;
using System.IO;
using TMNAdapter.Entities;
using TMNAdapter.Tracking;

namespace TMNAdapter.Utilities
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

            string currentDirectory = Directory.GetCurrentDirectory();
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

            if (jiraKeyAttachments.ContainsKey(key))
            {
                if (!jiraKeyAttachments[key].Contains(targetFilePath))
                {
                    jiraKeyAttachments[key].Add(targetFilePath);
                }
            }
            else
            {
                List<string> files = new List<string>();
                files.Add(targetFilePath);
                jiraKeyAttachments.Add(key, files);
            }
        }

        public static void SaveParameter<T>(string title, T value)
        {
            string key = GetJiraTestKey();

            if (key == null)
            {
                return;
            }

            TestParameters parameters = new TestParameters(title, value != null ? value.ToString() : "null");

            if (jiraKeyParameters.ContainsKey(key))
            {
                jiraKeyParameters[key].Add(parameters);
            }
            else
            {
                List<TestParameters> newTestParameters = new List<TestParameters>();
                newTestParameters.Add(parameters);
                jiraKeyParameters.Add(title, newTestParameters);
            }
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
            return jiraKeyParameters[issueKey] ?? null;
        }

        public static List<string> GetIssueAttachments(string issueKey)
        {
            return jiraKeyAttachments[issueKey] ?? null;
        }
    }
}
