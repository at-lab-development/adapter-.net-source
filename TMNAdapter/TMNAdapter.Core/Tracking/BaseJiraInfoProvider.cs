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
        protected virtual string GetJiraIssueKey<TAttribute>() where TAttribute : BaseJiraIssueKeyAttribute
        {
            return AnnotationTracker.GetAttributeInCallStack<TAttribute>()?.Key;
        }

        public virtual IssueModel SaveAttachment(string issueKey, FileInfo file)
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
                bool isOutOfAttachmentsDir = !filePath.StartsWith(currentDirectory + FileUtils.GetAttachmentsDir());

                targetFilePath = isOutOfAttachmentsDir ?
                                   FileUtils.SaveFile(file) :
                                   filePath.Replace(currentDirectory, String.Empty);
            }
            catch (AttachmentSavingException exception)
            {
                SaveStackTrace(issueKey, exception.StackTrace); //do we need it?
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

        public virtual IssueModel SaveParameter<T>(string issueKey, string title, T value)
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

        public virtual IssueModel SaveStackTrace(string issueKey, string stackTrace)
        {
            string fileName = $"stacktrace_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.txt";
            try
            {
                string targetFilePath = FileUtils.WriteStackTrace(stackTrace, fileName);
                return new IssueModel()
                {
                    Key = issueKey,
                    AttachmentFilePaths = new List<string>() { targetFilePath }
                };
            }
            catch (AttachmentSavingException exception)
            {
                return new IssueModel()
                {
                    Key = issueKey,
                    Summary = exception.Message
                };
            }
        }
    }
}
