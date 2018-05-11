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
/*
public class ThirdSampleTest {
    private final Random random = new Random();

    @JIRATestKey(key = "WRONGKEY")
    @Test
    public void testMethod() {
        boolean r = random.nextBoolean();
        Assert.assertTrue(r);
*/
