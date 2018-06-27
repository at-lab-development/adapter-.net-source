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
        public static void SendTestResult(string key, TestResult testResult)
        {
            var issueModel = new IssueModel()
            {
                Key = key,
                Time = testResult.Duration.Milliseconds,
                IsTestComplete = true
            };

            switch (testResult.Outcome)
            {
                case UnitTestOutcome.Failed:
                    FailedTest(key, testResult, issueModel);
                    break;
                case UnitTestOutcome.Passed:
                    PassedTest(key, testResult, issueModel);
                    break;
                default:
                    SkippedTest(key, testResult, issueModel);
                    break;
            }
        }

        private static void FailedTest(string key, TestResult testResult, IssueModel issue)
        {
            JiraInfoProvider.SaveStackTrace(key, GetStackTrace(testResult.TestFailureException));

            issue.Summary = testResult.TestFailureException.Message;
            issue.Status = Status.Failed;

            IssueManager.AddIssue(issue);
        }

        private static void PassedTest(string key, TestResult testResult, IssueModel issue)
        {
            issue.Status = Status.Passed;

            IssueManager.AddIssue(issue);
        }

        private static void SkippedTest(string key, TestResult testResult, IssueModel issue)
        {
            issue.Status = Status.Untested;

            IssueManager.AddIssue(issue);
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
