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

namespace TMNAdapter.MSTest
{
    public class ExecutionTracker
    {
        private static List<Issue> issues = new List<Issue>();

        public static void SendTestResult(ITest test, string key, long time)
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Failed:
                    FailedTest(test, key, time);
                    break;
                case TestStatus.Passed:
                    PassedTest(test, key, time);
                    break;
                default:
                    SkippedTest(test, key, time);
                    break;
            }
        }

        static void FailedTest(ITest test, string key, long time)
        {
            JiraInfoProvider.SaveStackTrace(key, TestContext.CurrentContext.Result.StackTrace);

            IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Summary = $"{TestContext.CurrentContext.Result.Message}\n TestContext.CurrentContext.Result.StackTrace",
                Status = Status.Failed,
                Time = time,
                IsTestComplete = true
            });
        }

        static void PassedTest(ITest test, string key, long time)
        {
            IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Status = Status.Passed,
                Time = time,
                IsTestComplete = true
            });
        }

        static void SkippedTest(ITest test, string key, long time)
        {
            IssueManager.AddIssue(new IssueModel()
            {
                Key = key,
                Status = Status.Untested,
                Time = time,
                IsTestComplete = true
            });
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
                    Time = issueModel.Time.ToString(),
                    Attachments = issueModel.AttachmentFilePaths,
                    Parameters = issueModel.Parameters
                });
            }

            FileUtils.WriteXml(testResult, "tm-testng.xml");
        }
    }
}
