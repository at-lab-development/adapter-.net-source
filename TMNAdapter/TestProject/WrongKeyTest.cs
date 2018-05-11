using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Tracking;

namespace TestProject
{
    [TestClass]
    public class WrongKeyTest
    {
        [TestMethod]
        [JiraIssueKey("WRONGKEY")]
        public void TestWrongKey()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random, "Random bool parameter is false");
        }
    }
}