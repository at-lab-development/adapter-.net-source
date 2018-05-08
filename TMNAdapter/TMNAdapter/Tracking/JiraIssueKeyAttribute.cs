using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMNAdapter.Common.Validation;

namespace TMNAdapter.Tracking
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class JiraIssueKeyAttribute : Attribute
    {
        public string Key { get; }
        public bool Disabled { get; }
        public int RetryCountOnFailure { get; }
        public bool DisableScreenshotOnFailure { get; }

        public JiraIssueKeyAttribute(string key, bool disabled = false, int retryCountOnFailure = 1, bool disableScreenshotOnFailure = false)
        {
            ValidationHelper.MatchPattern(key, nameof(key), @"((?<!([A-Za-z]{1,10})-?)[A-Z]+-\d+)");
            ValidationHelper.InRange(retryCountOnFailure, nameof(retryCountOnFailure), 1, 5);
            
            Key = key;
            Disabled = disabled;
            RetryCountOnFailure = retryCountOnFailure;
            DisableScreenshotOnFailure = disableScreenshotOnFailure;
        }
    }
}
