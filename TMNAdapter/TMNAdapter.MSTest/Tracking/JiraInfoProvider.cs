using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TMNAdapter.Core.Tracking;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TMNAdapter.MSTest.Tracking
{
    public class JiraInfoProvider : BaseJiraInfoProvider
    {
        private readonly TestContext _testContext;

        public JiraInfoProvider(TestContext testContext)
        {
            _testContext = testContext;
        }

        public IssueModel SaveAttachment(FileInfo file)
        {
            string issueKey = GetJiraIssueKey<JiraIssueKeyAttribute>();

            IssueModel issue = base.SaveAttachment(issueKey, file);

            _testContext.AddResultFile(issue.AttachmentFilePaths.FirstOrDefault());

            return issue;
        }

        public IssueModel SaveParameter<T>(string title, T value)
        {
            string issueKey = GetJiraIssueKey<JiraIssueKeyAttribute>();

            IssueModel issue = base.SaveParameter(issueKey, title, value);

            TestParameters testParameters = issue.Parameters.FirstOrDefault();
            if (testParameters != null)
            {
                _testContext.WriteLine($"{testParameters.Title}:{testParameters.Value}");
            }

            return issue;
        }

        public override IssueModel SaveStackTrace(string issueKey, string stackTrace)
        {
            IssueModel issue = base.SaveStackTrace(issueKey, stackTrace);

            _testContext.AddResultFile(issue.AttachmentFilePaths.FirstOrDefault());

            return issue;
        }
    }
}
