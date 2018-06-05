using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TestResult = Microsoft.VisualStudio.TestTools.UnitTesting.TestResult;

namespace TMNAdapter.MSTest.Tracking
{
    public class ExecutionTracker
    {
        private static JiraInfoProvider _jiraInfoProvider;

        static ExecutionTracker()
        {
            _jiraInfoProvider = new JiraInfoProvider();
        }

        public static void SubmitTestResult(string key, TestResult testResult)
        {
            IssueModel issueModel = null;

            switch (testResult.Outcome)
            {
                case UnitTestOutcome.Failed:
                    issueModel = FailedTest(key, testResult);
                    break;
                case UnitTestOutcome.Passed:
                    issueModel = PassedTest(key, testResult);
                    break;
                default:
                    issueModel = SkippedTest(key, testResult);
                    break;
            }

            issueModel.Time = testResult.Duration.Milliseconds;
            issueModel.IsTestComplete = true;

            var debug = IssueManager.AddIssue(issueModel);
        }

        private static IssueModel FailedTest(string key, TestResult testResult)
        {
            string stackTrace = GetStackTrace(testResult.TestFailureException);

            _jiraInfoProvider.SaveStackTrace(key, stackTrace);

            return IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Status = Status.Failed,
                Summary = testResult.TestFailureException.Message
            });
        }

        private static IssueModel PassedTest(string key, TestResult testResult)
        {
            return IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Status = Status.Passed
            });
        }

        private static IssueModel SkippedTest(string key, TestResult testResult)
        {
            return IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Status = Status.Untested
            });
        }

        private static string GetStackTrace(Exception exception)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }

            return exception.StackTrace;
        }
    }
}
