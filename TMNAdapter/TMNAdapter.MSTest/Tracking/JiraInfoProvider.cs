using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using TMNAdapter.Core.Tracking;
using TMNAdapter.Core.Tracking.Attributes;
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

            IssueManager.AddIssue(issue);

            return issue;
        }

        public IssueModel SaveParameter<T>(string title, T value)
        {
            string issueKey = GetJiraIssueKey<JiraIssueKeyAttribute>();

            IssueModel issue = base.SaveParameter(issueKey, title, value);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public override IssueModel SaveStackTrace(string issueKey, string stackTrace)
        {
            IssueModel issue = base.SaveStackTrace(issueKey, stackTrace);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public void SubmitTestResults()
        {
            var a = Assembly.GetCallingAssembly().GetName().Name;
            Type classType = Type.GetType($"{_testContext.FullyQualifiedTestClassName}, {a}");
            string issueKey = AnnotationTracker.GetAttributeByMethodName<JiraIssueKeyAttribute>(classType, _testContext.TestName).Key;

            IssueModel issueModel = IssueManager.GetIssue(issueKey);

            string serializedIssue = TestReporter.IssueToJson(issueModel);
            _testContext.WriteLine(serializedIssue);
        }
    }
}
