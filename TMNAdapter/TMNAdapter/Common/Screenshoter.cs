﻿using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TMNAdapter.Common.Models;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TMNAdapter.Common
{
    public class Screenshoter
    {
        private static readonly string SCREENSHOT_FILE = "scr_%s.png";
        private static IWebDriver driverInstance;

        public static void Initialize(IWebDriver driver)
        {
            driverInstance = driver;
        }

        public static bool IsInitialized()
        {
            return driverInstance != null;
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
            string fullScreenshotPath = TestContext.CurrentContext.WorkDirectory + relativeScreenshotPath;
            var screenshot = ((ITakesScreenshot)driverInstance).GetScreenshot();
            screenshot.SaveAsFile(fullScreenshotPath, ScreenshotImageFormat.Jpeg);

            string issueKey = JiraInfoProvider.GetJiraTestKey();
            IssueManager.AddIssue(new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() { relativeScreenshotPath }
            });
        }
    }
}