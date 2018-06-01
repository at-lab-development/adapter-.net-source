using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class MS_AttachmentsTest : BaseTest
    {
        [JiraTestMethod("EPMFARMATS-2447")]
        public void TestAttachmentsAdding()
        {
            var random = new Random();

            JiraInfoProvider.SaveParameter("Random number", Convert.ToString(random.Next()));
            JiraInfoProvider.SaveParameter("Random boolean", Convert.ToString(random.Next(0, 1)));
            JiraInfoProvider.SaveParameter("Some static string", "Hello, world!");

            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\Resources\jenkins-oops.jpg"));
            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\Resources\jenkins-oops.jpg"));

            Assert.Fail("Testing failed test behavior");
        }

        [JiraTestMethod("EPMFARMATS-2471")]
        public void IgnoredTest()
        {
            JiraInfoProvider.SaveParameter("Email", "1@gmail.com");

            Assert.IsTrue(true);
        }
    }
}
