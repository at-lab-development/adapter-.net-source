using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking;

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
            var issueKey =
                context.ScenarioInfo.Tags.FirstOrDefault(t => JiraTestMethodTagHooks.issueTemplate.IsMatch(t));

            var issue = SaveAttachment(issueKey, file);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public static IssueModel SaveParameter<T>(string title, T value, ScenarioContext context)
        {
            var issueKey =
                context.ScenarioInfo.Tags.FirstOrDefault(t => JiraTestMethodTagHooks.issueTemplate.IsMatch(t));

            var issue = SaveParameter(issueKey, title, value);

            IssueManager.AddIssue(issue);

            return issue;
        }
    }
}