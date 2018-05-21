using System;
using System.Diagnostics;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.Common.Validation;

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

        private Stopwatch _stopWatch;
        private long elapsedTime;

        

        /// <summary>
        /// Indicates whether <see cref="JiraIssueKeyAttribute"/> is applied 
        /// </summary>
        public bool Disabled { get; }

        /// <summary>
        /// Specifies the count of attempts to re-run test method on failure
        /// </summary>
        public int RetryCountOnFailure { get; }

        /// <summary>
        /// Indicates whether screenshots are allowed
        /// </summary>
        public bool DisableScreenshotOnFailure { get; }

        public ActionTargets Targets => ActionTargets.Test;

        /// <summary>
        /// Initializes a new instance of <see cref="JiraIssueKeyAttribute"/>
        /// </summary>
        /// <param name="key">JIRA issue key</param>
        /// <param name="disabled">Indicates whether <see cref="JiraIssueKeyAttribute"/> is applied</param>
        /// <param name="retryCountOnFailure">Specifies the count of attempts to re-run test method on failure</param>
        /// <param name="disableScreenshotOnFailure">Indicates whether screenshots are allowed</param>
        public JiraIssueKeyAttribute(string key, bool disabled = false, int retryCountOnFailure = 1, bool disableScreenshotOnFailure = false)
        {
            ValidationHelper.MatchPattern(key, nameof(key), @"((?<!([A-Za-z]{1,10})-?)[A-Z]+-\d+)");
            ValidationHelper.InRange(retryCountOnFailure, nameof(retryCountOnFailure), 1, 5);

            Key = key;
            Disabled = disabled;
            RetryCountOnFailure = retryCountOnFailure;
            DisableScreenshotOnFailure = disableScreenshotOnFailure;
        }

        public void BeforeTest(ITest test)
        {
            _stopWatch = Stopwatch.StartNew();
        }

        public void AfterTest(ITest test)
        {
            _stopWatch.Stop();
            elapsedTime = _stopWatch.ElapsedMilliseconds;
            ExecutionTracker.SendTestResult(test, Key, elapsedTime);
        }
    }
}
