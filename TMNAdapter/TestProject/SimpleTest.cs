using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TestProject
{
    [TestClass]
    public class SimpleTest
    {
        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2447")]
        public void CheckArtifacts()
        {
            Random random = new Random();

            JiraInfoProvider.SaveParameter("Random number", Convert.ToString(random.Next()));
            JiraInfoProvider.SaveParameter("Random boolean", Convert.ToString(random.Next(0, 1)));
            JiraInfoProvider.SaveParameter("Some static string", "Hello, world!");
            JiraInfoProvider.SaveAttachment(new FileInfo("..\\..\\Resourses\\jenkins-oops.jpg"));
            JiraInfoProvider.SaveAttachment(new FileInfo("..\\..\\Resourses\\jenkins-oops.jpg"));

            Assert.Fail("Testing failed test behavior");
        }

        [TestMethod]
        [Ignore]
        [JiraIssueKey("EPMFARMATS-2447")]
        public void UntestedTest()
        {
            Assert.IsTrue(true);
        }
    }
}
/*
@Listeners(com.epam.jira.testng.ExecutionListener.class)
public class SimpleTest {}
 */
