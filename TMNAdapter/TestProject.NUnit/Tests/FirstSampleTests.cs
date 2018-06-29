using System;
using NUnit.Framework;
using TMNAdapter.Tracking;
using TMNAdapter.Tracking.Attributes;

[assembly: GenerateTestReportForJIRA]
namespace TestProject.NUnit.Tests
{
    [TestFixture]
    public class FirstSampleTests
    {
        [Test]
        [JiraTestMethod("EPMFARMATS-2464")]
        public void TestMethod()
        {
            JiraInfoProvider.SaveParameter("Value1", "Sample");
            JiraInfoProvider.SaveParameter("Value2", "Sample");
            JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }

        [Test]
        [JiraTestMethod("EPMFARMATS-2472")]
        public void TestExceptionInTest1()
        {
            throw new Exception("Testing test with exception");
        }


        [Test]
        [JiraTestMethod("EPMFARMATS-2472")]
        public void TestExceptionInTest2()
        {
            JiraInfoProvider.SaveParameter("Value4", "Sample4");
            throw new Exception("Testing test with exception");
        }
    }
}
