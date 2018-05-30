using System;
using TMNAdapter.Core.Tracking.Attributes;

namespace TMNAdapter.MSTest.Tracking.Attributes
{
    /// <summary>
    /// Represents an attribute, which marks test method, to be linked with
    /// JIRA issue, using issue key
    /// MSTest implementation
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class JiraIssueKeyAttribute : BaseJiraIssueKeyAttribute
    {
        /// <summary>
        /// Initializes a new instance of <see cref="JiraIssueKeyAttribute"/>
        /// </summary>
        /// <param name="key">JIRA issue key</param>
        public JiraIssueKeyAttribute(string key) : base(key)
        {
        }
    }
}
