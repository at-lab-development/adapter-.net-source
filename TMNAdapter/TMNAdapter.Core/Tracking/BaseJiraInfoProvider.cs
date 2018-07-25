using System;
using System.Collections.Generic;
using System.IO;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TMNAdapter.Core.Tracking.Attributes;
using TMNAdapter.Core.Tracking.Interfaces;

namespace TMNAdapter.Core.Tracking
{
    public class BaseJiraInfoProvider
    {
        protected static string GetJiraIssueKey<TAttribute>() where TAttribute : IJiraIssueKeyAttribute
        {
            return AnnotationTracker.GetAttributeInCallStack<TAttribute>()?.Key;
        }

        protected static IssueModel SaveAttachment(string issueKey, FileInfo file)
        {
            if (issueKey == null)
            {
                return null;
            }

            string currentDirectory = FileUtils.Solution_dir;
            string targetFilePath = null;

            try
            {
                string filePath = file.FullName;
                bool isOutOfAttachmentsDir = !filePath.StartsWith(currentDirectory + Constants.ATTACHMENTS_DIR);

                targetFilePath = isOutOfAttachmentsDir ?
                                   FileUtils.SaveFile(file) :
                                   filePath.Replace(currentDirectory, String.Empty);
            }
            catch (SaveAttachmentException exception)
            {
                return new IssueModel()
                {
                    Key = issueKey,
                    Summary = exception.Message
                };
            }
            catch (IOException exception)
            {
                Console.Write(exception.StackTrace);
            }

            return new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() { targetFilePath }
            };
        }

        protected static IssueModel SaveParameter<T>(string issueKey, string title, T value)
        {
            if (issueKey == null)
            {
                return null;
            }

            return new IssueModel()
            {
                Key = issueKey,
                Parameters = new List<TestParameters>()
                {
                    new TestParameters(title, value != null ? value.ToString() : "null")
                }
            };
        }

        protected static void SaveStackTrace(string issueKey, string stackTrace)
        {
            IssueModel issue = null;
            string fileName = $"stacktrace_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.txt";
            try
            {
                string targetFilePath = FileUtils.WriteStackTrace(stackTrace, fileName);
                issue = new IssueModel()
                {
                    Key = issueKey,
                    AttachmentFilePaths = new List<string>() { targetFilePath }
                };
            }
            catch (SaveAttachmentException exception)
            {
                issue = new IssueModel()
                {
                    Key = issueKey,
                    Summary = exception.Message
                };
            }

            IssueManager.AddIssue(issue);
        }
    }
}
