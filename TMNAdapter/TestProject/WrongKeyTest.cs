using System;
using NUnit.Framework;
using TMNAdapter.Tracking;

namespace TestProject
{
    [TestFixture]
    public class WrongKeyTest
    {
        [Test]
        [JiraIssueKey("WRONGKEY")]
        public void TestWrongKey()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random, "Random bool parameter is false");
        }
    }
}