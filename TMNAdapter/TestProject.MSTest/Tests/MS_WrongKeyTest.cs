using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class MS_WrongKeyTest
    {
        [TestMethod]
        //[JiraIssueKey("WRONGKEY")]
        public void TestWrongKey()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random, "Random bool parameter is false");
        }
    }
}
