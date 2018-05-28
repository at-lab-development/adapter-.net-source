using System;
using NUnit.Framework;
using TMNAdapter.Tracking.Attributes;

namespace TestProject.NUnit.Tests
{
    [TestFixture]
    public class WrongKeyTest : BaseTest
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