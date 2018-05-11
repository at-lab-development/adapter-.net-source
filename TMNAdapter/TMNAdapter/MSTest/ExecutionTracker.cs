using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Entities;
using TMNAdapter.Utilities;

namespace TMNAdapter.MSTest
{
    public class ExecutionTracker
    {
        private static List<Issue> issues = new List<Issue>();

        //This method must be invoked explicitly after each test completion
        public static void SendTestResult(TestContext testContext)
        {
            switch (testContext.CurrentTestOutcome)
            {
                case UnitTestOutcome.Failed:
                    FailedTest();
                    break;
                case UnitTestOutcome.Passed:
                    PassedTest();
                    break;
                default:
                    SkippedTest();
                    break;
            }
        }

        static void FailedTest()
        {

        }

        static void PassedTest()
        {

        }

        static void SkippedTest()
        {

        }

        //This method must be invoked explicitly after test run completion
        static void GenerateTestResultXml()
        {
            foreach (Issue issue in issues)
            {
                List<string> attachments = JiraInfoProvider.GetIssueAttachments(issue.IssueKey);
                List<TestParameters> parameters = JiraInfoProvider.GetIssueParameters(issue.IssueKey);
                if (attachments != null)
                {
                    issue.Attachments = attachments;
                }
                if (parameters != null)
                {
                    issue.Parameters = parameters;
                }
            }

            if (issues.Any())
            {
                FileUtils.WriteXml(new Entities.TestResult(issues), "tm-testng.xml");
            }
        }
    }
}
