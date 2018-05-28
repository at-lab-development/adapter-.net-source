using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Tracking
{
    public class ExecutionTracker
    {
        public static void SendTestResult(string key, long time)
        {
            var issueModel = new IssueModel()
            {
                Key = key,
                Time = time,
                IsTestComplete = true
            };

            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Failed:
                    FailedTest(issueModel);
                    break;
                case TestStatus.Passed:
                    PassedTest(issueModel);
                    break;
                default:
                    SkippedTest(issueModel);
                    break;
            }
        }

        private static void FailedTest(IssueModel issueModel)
        {
            JiraInfoProvider.SaveStackTrace(issueModel.Key, TestContext.CurrentContext.Result.StackTrace);

            issueModel.Summary = TestContext.CurrentContext.Result.Message;
            issueModel.Status = Status.Failed;

            IssueManager.AddIssue(issueModel);
        }

        private static void PassedTest(IssueModel issueModel)
        {
            issueModel.Status = Status.Passed;

            IssueManager.AddIssue(issueModel);
        }

        private static void SkippedTest(IssueModel issueModel)
        {
            issueModel.Status = Status.Untested;

            IssueManager.AddIssue(issueModel);
        }

    }
}
