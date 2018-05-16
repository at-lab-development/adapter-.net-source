using NUnit.Framework;
using System.Collections.Generic;
using TMNAdapter.Common;
using TMNAdapter.Entities;
using TMNAdapter.Tracking;

namespace TMNAdapter.MSTest
{
    public class ExecutionTracker
    {
        private static List<Issue> issues = new List<Issue>();

        //must be invoked explicitly after each test completion
        public static void SendTestResult(TestContext testContext)
        {
            switch (testContext.Result.Outcome.Status)
            {
                case NUnit.Framework.Interfaces.TestStatus.Failed:
                    FailedTest(testContext);
                    break;
                case NUnit.Framework.Interfaces.TestStatus.Passed:
                    PassedTest(testContext);
                    break;
                default:
                    SkippedTest();
                    break;
            }
        }

        static void FailedTest(TestContext testContext)
        {
			string key = AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
			if (key != null)
			{
				Issue issue = new Issue(key, Status.Failed, Screenshoter.TakeScreenshot(), testContext.Result.Message);
				issues.Add(issue);
			}
		}

		static void PassedTest(TestContext testContext)
        {
			string key = AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
			if (key != null)
			{
				Issue issue = new Issue(key, Status.Passed);
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
		}

        //some other methods

        //must be invoked explicitly after test run completion
        static void GenerateTestResultXml()
        {

        }
    }
}
