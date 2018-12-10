using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TechTalk.SpecFlow;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Validation;
using TMNAdapter.SpecFlow.Common;

namespace TMNAdapter.SpecFlow
{
    [Binding]
    public class JiraTestMethodTagHooks
    {
        public static Regex issueTemplate = new Regex("[A-Z]+-[0-9]+");

        [BeforeTestRun]
        public static void WorkingDirectorySetup()
        {
            FileUtils.Solution_dir = Environment.CurrentDirectory;
        }

        [BeforeScenario]
        public static void JiraScenarioInit(ScenarioContext scenarioContext)
        {
            string issueTag = scenarioContext.ScenarioInfo.Tags.Where(t => issueTemplate.IsMatch(t)).FirstOrDefault();
            if (issueTag != null)
            {
                ValidationHelper.MatchPattern(issueTag, nameof(issueTag), issueTemplate.ToString());

                ExecutionTracker.AddTracked(issueTag, scenarioContext.ScenarioInfo.Title);
            }
        }

        [AfterScenario]
        public static void JiraScenarioCleanUp(ScenarioContext scenarioContext)
        {
            string issueTag = scenarioContext.ScenarioInfo.Tags.Where(t => issueTemplate.IsMatch(t)).FirstOrDefault();
            if (issueTag != null) ExecutionTracker.SendTestResult(issueTag, scenarioContext);
        }

        [AfterTestRun]
        public static void SaveReportBeforeShutDown()
        {
            TestReporter.GenerateTestResultXml();
        }
    }
}
