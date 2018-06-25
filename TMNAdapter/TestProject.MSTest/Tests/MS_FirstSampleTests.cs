﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TMNAdapter.MSTest.Tracking;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class MS_FirstSampleTests 
    {
        [JiraTestMethod("EPMFARMATS-2464")]
        public void TestMethod()
        {
            JiraInfoProvider.SaveParameter("Value1", "Sample");
            JiraInfoProvider.SaveParameter("Value2", "Sample");
            JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }

        [JiraTestMethod("EPMFARMATS-2472")]
        public void TestExceptionInTest1()
        {
            throw new Exception("Testing test with exception");
        }

        [JiraTestMethod("EPMFARMATS-2472")]
        public void TestExceptionInTest2()
        {
            JiraInfoProvider.SaveParameter("Value4", "Sample4");
            throw new Exception("Testing test with exception");
        }
    }
 }
