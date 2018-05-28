using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.MSTest.Tracking;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class BasicTest : BaseTest
    {
        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2447")]
        public void CheckArtifacts()
        {
            var random = new Random();

            JiraInfoProvider.SaveParameter("Random number", Convert.ToString(random.Next()));
            JiraInfoProvider.SaveParameter("Random boolean", Convert.ToString(random.Next(0, 1)));
            JiraInfoProvider.SaveParameter("Some static string", "Hello, world!");

            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\Resources\jenkins-oops.jpg"));
            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\Resources\jenkins-oops.jpg"));

            Assert.Fail("Testing failed test behavior");
        }

        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2471")]
        public void UntestedTest()
        {
            Assert.IsTrue(true);
        }
    }
}
