using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TMNAdapter.MSTest.Tracking.Attributes
{
    /// <summary>
    /// Represents an attribute, which marks test method, to be linked with
    /// JIRA issue, using issue key
    /// MSTest implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class JiraTestMethodAttribute : TestMethodAttribute
    {
        public string Key { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="JiraTestMethodAttribute"/>
        /// </summary>
        /// <param name="key">JIRA issue key</param>
        public JiraTestMethodAttribute(string key)
        {
            Key = key;
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            TestResult[] results = base.Execute(testMethod);
            TestResult result = results.FirstOrDefault();

            Debug.WriteLine(
                $"{result.Outcome}\n" +
                $"{result.DisplayName} {testMethod.TestMethodName}\n" +
                $"{result.Duration}\n" +
                $"{result.LogError}\n" +
                $"{result.LogOutput}\n" +
                $"{result.TestContextMessages}\n" +
                $"{result.TestFailureException.Message}\n" +
                $"{result.TestFailureException.StackTrace}\n");

            return results;
        }
    }
}
