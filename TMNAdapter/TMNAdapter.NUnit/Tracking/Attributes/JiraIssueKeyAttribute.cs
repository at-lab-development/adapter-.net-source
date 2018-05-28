using System;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.Core.Common.Validation;

namespace TMNAdapter.Tracking.Attributes
{
    /// <summary>
    /// Represents an attribute, which marks test method, to be linked with
    /// JIRA issue, using issue key
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class JiraIssueKeyAttribute : Attribute, ITestAction
    {
        /// <summary>
        /// Gets JIRA issue key
        /// </summary>
        public string Key { get; }

        public ActionTargets Targets => ActionTargets.Test;

        private Stopwatch _stopWatch;

        /// <summary>
        /// Initializes a new instance of <see cref="JiraIssueKeyAttribute"/>
        /// </summary>
        /// <param name="key">JIRA issue key</param>
        public JiraIssueKeyAttribute(string key)
        {
            ValidationHelper.MatchPattern(key, nameof(key), @"((?<!([A-Za-z]{1,10})-?)[A-Z]+-\d+)");

            Key = key;
        }

        public void BeforeTest(ITest test)
        {
            _stopWatch = Stopwatch.StartNew();
        }

        public void AfterTest(ITest test)
        {
            _stopWatch.Stop();
            ExecutionTracker.SendTestResult(Key, _stopWatch.ElapsedMilliseconds);
        }
    }
}
