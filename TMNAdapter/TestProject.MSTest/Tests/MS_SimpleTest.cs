using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    class MS_SimpleTest
    {

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2464")]
        public void TestParametersSetting()
        {
            //JiraInfoProvider.SaveParameter("Name", "John");
            //JiraInfoProvider.SaveParameter("Surname", "Smith");
            //JiraInfoProvider.SaveParameter("Email", "Mr_Smith@gmail.com");

            Assert.IsTrue(true);
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2472")]
        public void TestExeptionInTest_1()
        {
            throw new Exception("Testing test with exception");
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2472")]
        public void TestExeptionInTest_2()
        {
            //JiraInfoProvider.SaveParameter("Value4", "Sample4");
            throw new Exception("Testing test with exception");
        }
    }
}

