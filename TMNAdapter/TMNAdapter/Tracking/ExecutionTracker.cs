using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Common;
using TMNAdapter.Common.Models;
using TMNAdapter.Entities;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TMNAdapter.Tracking
{
    public class ExecutionTracker
    {
        public static void SendTestResult(ITest test, string key, long time)
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

            issueModel.Summary = FormatSummary(TestContext.CurrentContext.Result.Message);
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

        public static void GenerateTestResultXml()
        {
            List<IssueModel> issueModels = IssueManager.GetIssues();

            if (!issueModels.Any())
            {
                return;
            }

            var testResult = new TestResult() {Issues = new List<Issue>()};
            foreach (var issueModel in issueModels)
            {
                testResult.Issues.Add(new Issue()
                {
                    IssueKey = issueModel.Key,
                    Summary = issueModel.Summary,
                    Status = issueModel.Status.ToString(),
                    Time = FormatTime(issueModel.Time),
                    Attachments = issueModel.AttachmentFilePaths,
                    Parameters = issueModel.Parameters
                });                
            }

            FileUtils.WriteXml(testResult, "tm-testng.xml");
        }

        private static string FormatTime(long? timeInMilliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(Convert.ToDouble(timeInMilliseconds));
            return $"{timeSpan.Minutes:D2}m:{timeSpan.Seconds:D2}s:{timeSpan.Milliseconds:D3}ms";
        }

        private static string FormatSummary(string message)
        {            
            return message.Replace("^", "").Replace("-", "");
        }
    }
}
