using System;
using TMNAdapter.Core.Common.Validation;
using TMNAdapter.Core.Tracking.Interfaces;

namespace TMNAdapter.Core.Tracking.Attributes
{
    /// <summary>
    /// Represents an attribute, which marks test method, to be linked with
    /// JIRA issue, using issue key
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class BaseJiraTestMethodAttribute : Attribute, IJiraIssueKeyAttribute
    {
        /// <summary>
        /// Gets JIRA issue key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseJiraTestMethodAttribute"/>
        /// </summary>
        /// <param name="key">JIRA issue key</param>
        public BaseJiraTestMethodAttribute(string key)
        {
            ValidationHelper.MatchPattern(key, nameof(key), @"((?<!([A-Za-z]{1,10})-?)[A-Z]+-\d+)");

            Key = key;
        }
    }
}
