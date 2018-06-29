using NUnit.Framework;
using System.IO;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking;
using TMNAdapter.Tracking.Attributes;

namespace TMNAdapter.Tracking
{
    public class JiraInfoProvider : BaseJiraInfoProvider
    {
        static JiraInfoProvider()
        {
            FileUtils.Solution_dir = TestContext.CurrentContext.WorkDirectory;
        }

        public static IssueModel SaveAttachment(FileInfo file)
        {
            string issueKey = GetJiraIssueKey<JiraTestMethodAttribute>();

            IssueModel issue = SaveAttachment(issueKey, file);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public static IssueModel SaveParameter<T>(string title, T value)
        {
            string issueKey = GetJiraIssueKey<JiraTestMethodAttribute>();

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
