using System.Collections.Generic;
using TechTalk.SpecFlow;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;
using System.Diagnostics;

namespace TMNAdapter.SpecFlow.Common
{
    public class ExecutionTracker
    {
        private static Dictionary<string, string> trackedIssues = new Dictionary<string, string>();
        private static Dictionary<string, Stopwatch> testTime = new Dictionary<string, Stopwatch>();

        public static void AddTracked(string epamJiraTag, string title)
        {
            trackedIssues.Add(epamJiraTag, title);
            testTime.Add(epamJiraTag, new Stopwatch());
            testTime[epamJiraTag].Start();
        }

        public static void SendTestResult(string epamJiraTag, ScenarioContext context)
        {
            testTime[epamJiraTag].Stop();
            var issueModel = new IssueModel()
            {
                Key = epamJiraTag,
                Time = testTime[epamJiraTag].ElapsedMilliseconds,
                IsTestComplete = true
            };

            switch (context.ScenarioExecutionStatus)
            {
                case ScenarioExecutionStatus.TestError:
                    FailedTest(issueModel, context);
                    issueModel.IsTestComplete = false;
                    break;
                case ScenarioExecutionStatus.OK:
                    PassedTest(issueModel, context);
                    break;
                case ScenarioExecutionStatus.UndefinedStep:
                    MissingStep(issueModel, context);
                    issueModel.IsTestComplete = false;
                    break;
                default:
                    SkippedTest(issueModel, context);
                    issueModel.IsTestComplete = false;
                    break;
            }
        }

        public static void MissingStep(IssueModel issueModel, ScenarioContext context)
        {
            issueModel.Status = Status.Failed;

            IssueManager.AddIssue(issueModel);
        }

        private static void FailedTest(IssueModel issueModel, ScenarioContext context)
        {
            JiraInfoProvider.SaveStackTrace(issueModel.Key, context.TestError.StackTrace);

            issueModel.Summary = context.TestError.Message;
            issueModel.Status = Status.Failed;

            IssueManager.AddIssue(issueModel);
        }

        private static void PassedTest(IssueModel issueModel, ScenarioContext context)
        {
            issueModel.Status = Status.Passed;

            IssueManager.AddIssue(issueModel);
        }

        private static void SkippedTest(IssueModel issueModel, ScenarioContext context)
        {
            issueModel.Status = Status.Untested;

            IssueManager.AddIssue(issueModel);
        }
    }


}
