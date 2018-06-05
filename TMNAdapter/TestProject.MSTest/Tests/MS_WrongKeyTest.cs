using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TMNAdapter.MSTest.Tracking.Attributes;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class MS_WrongKeyTest : BaseTest
    {
        [JiraTestMethod("WRONGKEY")]
        public void TestWrongKey()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random, "Random bool parameter is false");
        }
    }
}
