using System;
using System.IO;
using NUnit.Framework;
using TMNAdapter.Tracking;
using TMNAdapter.Tracking.Attributes;
using TMNAdapter.Utilities;

namespace TestProject.Tests
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
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

        [Test]
        [Ignore("Test ignored tests behavior")]
        [JiraIssueKey("EPMFARMATS-2471")]
        public void UntestedTest()
        {
            Assert.IsTrue(true);
        }
    }
}
