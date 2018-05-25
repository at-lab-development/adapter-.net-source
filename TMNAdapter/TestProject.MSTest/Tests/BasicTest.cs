using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Tracking;

namespace TestProject.MSTest
{
    [TestClass]
    public class BasicTest
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2447")]
        public void CheckArtifacts()
        {
            var random = new Random();

            JiraInfoProvider.SaveParameter("Random number", Convert.ToString(random.Next()));
            JiraInfoProvider.SaveParameter("Random boolean", Convert.ToString(random.Next(0, 1)));
            JiraInfoProvider.SaveParameter("Some static string", "Hello, world!");

            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\Resources\jenkins-oops.jpg"));
            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}..\..\Resources\jenkins-oops.jpg"));

            TestContext.AddResultFile("");

            Assert.Fail("Testing failed test behavior");
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2471")]
        public void UntestedTest()
        {
            Assert.IsTrue(true);
        }
    }
}
