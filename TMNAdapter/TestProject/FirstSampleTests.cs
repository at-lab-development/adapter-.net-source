using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;
using static TMNAdapter.MSTest.ExecutionTracker;

namespace TestProject
{
    [TestClass]
    public class FirstSampleTests
    {
        private TestContext testContextInstance;

        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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

        [TestCleanup]
        public void TestCleanup()
        {
            SendTestResult(TestContext);
        }

        [AssemblyCleanup]
        public static void ClassCleanup()
        {
            GenerateTestResultXml();
        }
    }
}
/*
@Listeners(ExecutionListener.class)
 */
