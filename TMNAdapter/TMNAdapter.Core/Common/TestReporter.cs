using System;
using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Core.Common
{
    public static class TestReporter
    {
        public static void GenerateTestResultXml()
        {
            List<IssueModel> issueModels = IssueManager.GetIssues();

            if (!issueModels.Any())
            {
                return;
            }

            var testResult = new TestResult() { Issues = new List<Issue>() };
            foreach (var issueModel in issueModels)
            {
                testResult.Issues.Add(new Issue()
                {
                    IssueKey = issueModel.Key,
                    Summary = FormatSummary(issueModel.Summary),
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
            return string.IsNullOrWhiteSpace(message) ? null : message.Replace("\"", "");
        }
    }
}
