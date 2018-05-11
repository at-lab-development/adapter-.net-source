using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TestProject
{
    [TestClass]
    public class FirstSampleTests
    {
        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2464")]
        public void TestMethod()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));
            Assert.IsTrue(random);
        }

        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2465")]
        public void testMethod()
        {
            bool random = Convert.ToBoolean(new Random().Next(0, 2));

            JiraInfoProvider.SaveParameter("Value1", "Sample");
            JiraInfoProvider.SaveParameter("Value2", "Sample");
            JiraInfoProvider.SaveParameter("Value3", "Sample");

            Assert.IsTrue(true);
        }
    }
}
/*
@Listeners(ExecutionListener.class)
public class FirstSampleTest {
    private final Random random = new Random();


    @Test ()
    public void fakeMethod() {
        Assert.assertTrue(false);

    }

    @Test (dependsOnMethods = "fakeMethod")
    @JIRATestKey(key = "EPMFARMATS-826")
    public void testMethod() {
        boolean r = random.nextBoolean();
        Assert.assertTrue(r);

    }
    
    @JIRATestKey(key = "EPMFARMATS-827")
    @Test ()
    public void testMethod() throws InterruptedException {
        boolean r = random.nextBoolean();
        TimeUnit.SECONDS.sleep(2);

        JiraInfoProvider.saveValue("Value1", "Sample");
        JiraInfoProvider.saveValue("Value2", "Sample");
        JiraInfoProvider.saveValue("Value3", "Sample");
        Assert.assertTrue(true);

 */
