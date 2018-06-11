using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class MS_SimpleTest : BaseTest
    {
        [JiraTestMethod("EPMFARMATS-2464")]
        public void TestParametersSetting()
        {
            JiraInfoProvider.SaveParameter("Name", "John");
            JiraInfoProvider.SaveParameter("Surname", "Smith");
            JiraInfoProvider.SaveParameter("Email", "Mr_Smith@gmail.com");

            Assert.IsTrue(true);
        }

        [JiraTestMethod("EPMFARMATS-2472")]
        public void TestExceptionInTest_1()
        {
            throw new Exception("Testing test with exception");
        }

        [JiraTestMethod("EPMFARMATS-2472")]
        public void TestExceptionInTest_2()
        {
            JiraInfoProvider.SaveParameter("Value4", "Sample4");
            throw new Exception("Testing test with exception");
        }
    }
}
