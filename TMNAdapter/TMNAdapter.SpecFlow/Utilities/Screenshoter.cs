using System;
using System.IO;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TMNAdapter.Core.Common;

namespace TMNAdapter.SpecFlow.Utilities
{
    public class Screenshoter
    {
        private readonly ScenarioContext _context;
        private readonly IWebDriver _driver;

        public Screenshoter(ScenarioContext context, IWebDriver driver)
        {
            _context = context;
            _driver = driver;
        }

        protected string GetIssue()
        {
            return _context.ScenarioInfo.Tags.
                FirstOrDefault(t => JiraTestMethodTagHooks.issueTemplate.IsMatch(t));
        }

        public void GetScreenshot()
        {
            var screenshotName = $"screenshot_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.jpeg";
            var relativeScreenshotPath = Constants.ATTACHMENTS_DIR + "\\" + screenshotName;
            var fullScreenshotPath = FileUtils.Solution_dir + relativeScreenshotPath;

            FileUtils.CheckOrCreateDir(Path.GetDirectoryName(fullScreenshotPath));

            var screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
            screenshot.SaveAsFile(fullScreenshotPath, ScreenshotImageFormat.Jpeg);

            var issueKey = GetIssue();

            IssueManager.SetAttachments(issueKey, relativeScreenshotPath);
        }
    }
}