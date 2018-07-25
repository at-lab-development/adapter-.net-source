using System;
using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Core.Common
{
    public static class TestReporter
    {
        private static readonly string ResultFileName = Constants.RESULT_FILE_NAME;

        public static void GenerateTestResultXml()
        {
            List<IssueModel> issueModels = IssueManager.GetIssues();

            if (!issueModels.Any())
            {
                return;
            }

            FileUtils.WriteXml(GetTestResult(issueModels), ResultFileName);
        }

        private static TestResult GetTestResult(List<IssueModel> issueModels)
        {
            var testResult = new TestResult() { Issues = new List<Issue>() };
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
            return testResult;
        }

        private static string FormatTime(long? timeInMilliseconds)
        {
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(Convert.ToDouble(timeInMilliseconds));
            return $"{timeSpan.Minutes:D2}m:{timeSpan.Seconds:D2}s:{timeSpan.Milliseconds:D3}ms";
        }
    }
}
