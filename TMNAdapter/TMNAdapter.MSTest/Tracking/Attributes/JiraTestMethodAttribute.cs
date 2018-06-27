using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common.Validation;
using TMNAdapter.Core.Tracking.Interfaces;

namespace TMNAdapter.MSTest.Tracking.Attributes
{
    /// <summary>
    /// Represents an attribute, which marks test method, to be linked with
    /// JIRA issue, using issue key
    /// MSTest implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class JiraTestMethodAttribute : TestMethodAttribute, IJiraIssueKeyAttribute
    {
        public string Key { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="JiraTestMethodAttribute"/>
        /// </summary>
        /// <param name="key">JIRA issue key</param>
        public JiraTestMethodAttribute(string key)
        {
            ValidationHelper.MatchPattern(key, nameof(key), @"((?<!([A-Za-z]{1,10})-?)[A-Z]+-\d+)");

            Key = key;
        }

        public override TestResult[] Execute(ITestMethod testMethod)
        {
            TestResult[] results = base.Execute(testMethod);
            TestResult result = results.FirstOrDefault();

            ExecutionTracker.SendTestResult(Key, result);

            return results;
        }
    }
}
