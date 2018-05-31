using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.MSTest.Tracking;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class BaseTest
    {
        public TestContext TestContext { get; set; }
        protected static JiraInfoProvider JiraInfoProvider { get; set; }

        [AssemblyInitialize]
        public static void AssemblyOneTimeSetUp(TestContext testContext)
        {
            JiraInfoProvider = new JiraInfoProvider();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            JiraInfoProvider.SubmitTestResults(TestContext);
        }
    }
}
