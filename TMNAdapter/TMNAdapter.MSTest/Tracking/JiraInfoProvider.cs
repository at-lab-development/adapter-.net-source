using System;
using System.IO;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking;
using TMNAdapter.Core.Tracking.Attributes;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TMNAdapter.MSTest.Tracking
{
    public class JiraInfoProvider : BaseJiraInfoProvider
    {
        static JiraInfoProvider()
        {
            string currentJobDir = AppDomain.CurrentDomain.BaseDirectory;
            
            if (currentJobDir.Contains("\\workspace\\"))
            {
                string upperDir = Path.GetDirectoryName(currentJobDir);
                if (!upperDir.EndsWith("\\workspace"))
                {
                    do
                    {
                        currentJobDir = upperDir;
                        upperDir = Path.GetDirectoryName(currentJobDir);
                    }
                    while (!upperDir.EndsWith("\\workspace"));
                }
            }

            FileUtils.Solution_dir = currentJobDir;
        }

        public static IssueModel SaveAttachment(FileInfo file)
        {
            string issueKey = AnnotationTracker.GetAttributeInCallStack<JiraTestMethodAttribute>()?.Key;

            IssueModel issue = SaveAttachment(issueKey, file);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public static IssueModel SaveParameter<T>(string title, T value)
        {
            string issueKey = AnnotationTracker.GetAttributeInCallStack<JiraTestMethodAttribute>()?.Key;

            IssueModel issue = SaveParameter(issueKey, title, value);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public new static void SaveStackTrace(string issueKey, string stackTrace)
        {
            BaseJiraInfoProvider.SaveStackTrace(issueKey, stackTrace);
        }
    }
}
