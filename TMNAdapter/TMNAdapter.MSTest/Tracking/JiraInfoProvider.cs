using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TMNAdapter.Core.Tracking;

namespace TMNAdapter.MSTest.Tracking
{
    public class JiraInfoProvider : BaseJiraInfoProvider
    {
        private readonly TestContext _testContext;

        public JiraInfoProvider(TestContext testContext)
        {
            _testContext = testContext;
        }

        public override IssueModel SaveAttachment(FileInfo file)
        {
            IssueModel issue = base.SaveAttachment(file);

            _testContext.AddResultFile(issue.AttachmentFilePaths.FirstOrDefault());

            return issue;
        }

        public override IssueModel SaveParameter<T>(string title, T value)
        {
            IssueModel issue = base.SaveParameter(title, value);

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
