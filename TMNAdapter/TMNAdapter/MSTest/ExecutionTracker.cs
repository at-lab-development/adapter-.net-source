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

        public static void SendTestResult(ITest test, string key)
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Failed:
                    FailedTest(test, key);
                    break;
                case TestStatus.Passed:
                    PassedTest(test, key);
                    break;
                default:
                    SkippedTest(test, key);
                    break;
            }
        }

        static void FailedTest(ITest test, string key)
        {
            Issue issue = new Issue(key, Status.Failed, JiraIssueKeyAttribute.ElapsedTime.ToString())
            {
                Summary = (TestContext.CurrentContext.Result.Message + '\n' +
                           TestContext.CurrentContext.Result.StackTrace)
            };
            issues.Add(issue);
        }

        static void PassedTest(ITest test, string key)
        {
            Issue issue = new Issue(key, Status.Passed);
            issues.Add(issue);
        }

        static void SkippedTest(ITest test, string key)
        {
            Issue issue = new Issue(key, Status.Untested);
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
