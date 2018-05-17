using System;
using NUnit.Framework;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

[assembly: AssemblyAction]
namespace TestProject
{
    [TestFixture]
    public class FirstSampleTests
    {
        [Test]
        [JiraIssueKey("EPMFARMATS-2464")]
        public void TestMethodWithRandomTestResult()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random, "Random bool parameter is false");
        }

        [Test]
        [JiraIssueKey("EPMFARMATS-2465")]
        public void TestMethod()
        {
            JiraInfoProvider.SaveParameter("Value1", "Sample");
            JiraInfoProvider.SaveParameter("Value2", "Sample");
            JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }

        [Test]
        [JiraIssueKey("EPMFARMATS-2465")]
        public void TestExeption()
        {
            string test = null;
            Assert.Throws(typeof(NullReferenceException), () => test.Substring(0, 4));
        }
    }
}
/*
@Listeners(ExecutionListener.class)
 */
