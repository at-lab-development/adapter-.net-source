using System;
using NUnit.Framework;
using TMNAdapter.Tracking;
using TMNAdapter.Tracking.Attributes;
using TMNAdapter.Utilities;

[assembly: AssemblyAction]
namespace TestProject
{
    [TestFixture]
    public class FirstSampleTests
    {
        [Test]
        [JiraIssueKey("EPMFARMATS-2464")]
        public void TestMethod()
        {
            JiraInfoProvider.SaveParameter("Value1", "Sample");
            JiraInfoProvider.SaveParameter("Value2", "Sample");
            JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }

        [Test]
        [JiraIssueKey("EPMFARMATS-2472")]
        public void TestExeptionInTest()
        {
            throw new Exception("Testing test with exeption");
        }

    }
}
/*
@Listeners(ExecutionListener.class)
 */
