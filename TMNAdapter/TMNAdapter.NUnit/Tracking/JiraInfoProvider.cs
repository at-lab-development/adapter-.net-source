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
        public JiraInfoProvider()
        {
            FileUtils.Solution_dir = TestContext.CurrentContext.WorkDirectory;
        }

        public IssueModel SaveAttachment(FileInfo file)
        {
            string issueKey = GetJiraIssueKey<JiraTestMethodAttribute>();

            IssueModel issue = base.SaveAttachment(issueKey, file);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public IssueModel SaveParameter<T>(string title, T value)
        {
            string issueKey = GetJiraIssueKey<JiraTestMethodAttribute>();

            IssueModel issue = base.SaveParameter(issueKey, title, value);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public override IssueModel SaveStackTrace(string issueKey, string stackTrace)
        {
            IssueModel issue = base.SaveStackTrace(issueKey, stackTrace);

            IssueManager.AddIssue(issue);

            return issue;
        }
    }
}
