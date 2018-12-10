using System.IO;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking;
using TechTalk.SpecFlow;
using TMNAdapter.Core.Common;
using System.Linq;

namespace TMNAdapter.SpecFlow.Common
{
    public class JiraInfoProvider : BaseJiraInfoProvider
    {
        public new static void SaveStackTrace(string issueKey, string stackTrace)
        {
            BaseJiraInfoProvider.SaveStackTrace(issueKey, stackTrace);
        }

        public static IssueModel SaveAttachment(FileInfo file, ScenarioContext context)
        {
            string issueKey = context.ScenarioInfo.Tags.Where(t => JiraTestMethodTagHooks.issueTemplate.IsMatch(t)).FirstOrDefault();

            IssueModel issue = SaveAttachment(issueKey, file);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public static IssueModel SaveParameter<T>(string title, T value, ScenarioContext context)
        {
            string issueKey = context.ScenarioInfo.Tags.Where(t => JiraTestMethodTagHooks.issueTemplate.IsMatch(t)).FirstOrDefault();

            IssueModel issue = SaveParameter(issueKey, title, value);

            IssueManager.AddIssue(issue);

            return issue;
        }
    }
}