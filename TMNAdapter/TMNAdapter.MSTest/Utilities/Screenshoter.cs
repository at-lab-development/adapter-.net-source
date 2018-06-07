using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking.Attributes;
using TMNAdapter.Core.Utilities;
using TMNAdapter.Tracking.Attributes;

namespace TMNAdapter.MSTest.Utilities
{
    public class Screenshoter : BaseScreenshoter
    {
        static TestContext testContext;

        public Screenshoter(TestContext _testContext)
        {
            testContext = _testContext;
        }

        public static void TakeScreenshot()
        {
            if (!IsInitialized()) return;

            if (!(driverInstance is RemoteWebDriver))
            {
                Console.WriteLine("Unsupported driver type: " + driverInstance.GetType());
                return;
            }

            string screenshotName = $"screenshot_{DateTime.Now:yyyy-MM-ddTHH-mm-ss.fff}.jpeg";
            string relativeScreenshotPath = FileUtils.GetAttachmentsDir() + "\\" + screenshotName;
            string fullScreenshotPath = FileUtils.Solution_dir + relativeScreenshotPath;
            var screenshot = ((ITakesScreenshot)driverInstance).GetScreenshot();
            screenshot.SaveAsFile(fullScreenshotPath, ScreenshotImageFormat.Jpeg);

            var a = Assembly.GetCallingAssembly().GetName().Name;
            Type classType = Type.GetType($"{testContext.FullyQualifiedTestClassName}, {a}");
            string issueKey = AnnotationTracker.GetAttributeByMethodName<JiraIssueKeyAttribute>(classType, testContext.TestName).Key;
            IssueManager.AddIssue(new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() { relativeScreenshotPath }
            });
        }
    }
}
