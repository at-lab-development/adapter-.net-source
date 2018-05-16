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

        public static void SendTestResult()
        {
            switch (TestContext.CurrentContext.Result.Outcome.Status)
            {
                case TestStatus.Failed:
                    FailedTest();
                    break;
                case TestStatus.Passed:
                    PassedTest();
                    break;
                default:
                    SkippedTest();
                    break;
            }
        }

        static void FailedTest()
        {
            string key = AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
            if (key != null)
            {
                Issue issue = new Issue(key, Status.Failed, DateTime.Now.ToShortTimeString())
                {
                    Summary = TestContext.CurrentContext.Result.Message
                };
                issues.Add(issue);
            }
            else
            {
                Issue issue = new Issue("key_not_found", Status.Failed, DateTime.Now.ToShortTimeString())
                {
                    Summary = TestContext.CurrentContext.Result.Message
                };
                issues.Add(issue);
            }
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
                Issue issue = new Issue("key_not_found", Status.Passed);
                issues.Add(issue);
            }
        }

        static void SkippedTest()
        {
            string key = AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
            if (key != null)
            {
                Issue issue = new Issue(key, Status.Untested);
                issues.Add(issue);
            }
            else
            {
                Issue issue = new Issue("key_not_found", Status.Untested, DateTime.Now.ToShortTimeString());
                issues.Add(issue);
            }
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
                FileUtils.WriteXml(new TestResult(issues), "tm-testng.xml");
            }

        }
    }
}
