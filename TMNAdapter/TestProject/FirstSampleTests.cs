using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TestProject
{
    [TestClass]
    public class FirstSampleTests
    {
        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2464")]
        public void TestMethodWithRandomTestResult()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random, "Random bool parameter is false");
        }

        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2465")]
        public void TestMethod()
        {
            JiraInfoProvider.SaveParameter("Value1", "Sample");
            JiraInfoProvider.SaveParameter("Value2", "Sample");
            JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }

        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2465")]
        [ExpectedException(typeof(Exception))]
        public void TestExeption()
        {
            throw new Exception();
        }
    }
}
/*
@Listeners(ExecutionListener.class)
 */
