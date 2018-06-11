using System;
using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking;
using TMNAdapter.Core.Tracking.Attributes;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TMNAdapter.MSTest.Tracking
{
    public class JiraInfoProvider : BaseJiraInfoProvider
    {
        internal JiraInfoProvider()
        {

        }

        public JiraInfoProvider(TestContext testContext)
        {
            FileUtils.Solution_dir = testContext.TestDir + @"\..\..";
        }

        public IssueModel SaveAttachment(FileInfo file)
        {
            string issueKey = AnnotationTracker.GetAttributeInCallStack<JiraTestMethodAttribute>()?.Key;

            IssueModel issue = base.SaveAttachment(issueKey, file);

            IssueManager.AddIssue(issue);

            return issue;
        }

        public IssueModel SaveParameter<T>(string title, T value)
        {
            string issueKey = AnnotationTracker.GetAttributeInCallStack<JiraTestMethodAttribute>()?.Key;

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
    }
}
