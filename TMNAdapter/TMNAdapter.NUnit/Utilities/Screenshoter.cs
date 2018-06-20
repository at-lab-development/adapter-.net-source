using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Tracking.Attributes;
using TMNAdapter.Core.Utilities;
using TMNAdapter.Tracking.Attributes;

namespace TMNAdapter.Utilities
{
    public class Screenshoter : BaseScreenshoter
    {
        private static Screenshoter screenshoter;

        public static Screenshoter Instance
        {
            get
            {
                return screenshoter = screenshoter ?? (screenshoter = new Screenshoter());
            }
        }

        protected override string GetIssue()
        {
            return AnnotationTracker.GetAttributeInCallStack<JiraIssueKeyAttribute>()?.Key;
        }
    }
}
