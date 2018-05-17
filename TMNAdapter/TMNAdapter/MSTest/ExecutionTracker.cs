using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Entities;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TMNAdapter.MSTest
{
    public class ExecutionTracker
    {
        private static List<Issue> issues = new List<Issue>();

        public static void SendTestResult(ITest test, string key, string time)
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

        static void FailedTest(ITest test, string key, string time)
        {
            JiraInfoProvider.SaveStackTrace(key, TestContext.CurrentContext.Result.StackTrace);

            Issue issue = new Issue(key, Status.Failed, time)
            {
                Summary = (TestContext.CurrentContext.Result.Message + '\n' +
                           TestContext.CurrentContext.Result.StackTrace)
            };

            issues.Add(issue);
        }

        static void PassedTest(ITest test, string key, string time)
        {
            Issue issue = new Issue(key, Status.Passed, time);
            issues.Add(issue);
        }

        static void SkippedTest(ITest test, string key, string time)
        {
            Issue issue = new Issue(key, Status.Untested, time);
            issues.Add(issue);
        }

        public static void GenerateTestResultXml()
        {
            //foreach (Issue issue in issues)
            //{
            //    List<string> attachments = JiraInfoProvider.GetIssueAttachments(issue.IssueKey);
            //    List<Entities.TestParameters> parameters = JiraInfoProvider.GetIssueParameters(issue.IssueKey);
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
