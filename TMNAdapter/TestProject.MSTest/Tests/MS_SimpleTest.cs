﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    class MS_SimpleTest
    {

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2464")]
        public void TestMethod()
        {
            //JiraInfoProvider.SaveParameter("Value1", "Sample");
            //JiraInfoProvider.SaveParameter("Value2", "Sample");
            //JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2472")]
        public void TestExeptionInTest1()
        {
            throw new Exception("Testing test with exeption");
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2472")]
        public void TestExeptionInTest2()
        {
            //JiraInfoProvider.SaveParameter("Value4", "Sample4");
            throw new Exception("Testing test with exeption");
        }
    }
}

