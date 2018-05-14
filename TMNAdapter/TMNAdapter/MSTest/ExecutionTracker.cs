using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Entities;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;
using static TMNAdapter.Utilities.JiraInfoProvider;

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
            Issue issue = new Issue("Key_value_0", Status.Failed, DateTime.Now.ToString());
            issue.Summary = "Summary text message";
            issue.Attachments = new List<string>()
            {
                "dasdasd",
                "123sfsdf"
            };
            issues.Add(issue);
        }

        static void PassedTest()
        {
            string key = AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
            if (key != null)
            {
                Issue issue = new Issue(key, Status.Passed);
                issues.Add(issue);
            } 
            else
            {
                Issue issue = new Issue(key + "_key not found", Status.Passed);
                issues.Add(issue);
            }

        }

        static void SkippedTest()
        {
            string key = GetJiraTestKey();
            if (key != null)
            {
                Issue issue = new Issue(key, Status.Untested);
                issues.Add(issue);
            }

        }

        //This method must be invoked explicitly after test run completion
        public static void GenerateTestResultXml()
        {
            //foreach (Issue issue in issues)
            //{
            //    List<string> attachments = JiraInfoProvider.GetIssueAttachments(issue.IssueKey);
            //    List<TestParameters> parameters = JiraInfoProvider.GetIssueParameters(issue.IssueKey);
            //    if (attachments != null)
            //    {
            //        issue.Attachments = attachments;
            //    }
            //    if (parameters != null)
            //    {
            //        issue.Parameters = parameters;
            //    }
            //}

            if (issues.Any())
            {
                FileUtils.WriteXml(new Entities.TestResult(issues), "tm-testng.xml");
            }
        }
    }
}
