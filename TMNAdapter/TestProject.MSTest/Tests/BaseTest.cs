using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common;
using TMNAdapter.MSTest.Tracking;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected static JiraInfoProvider JiraInfoProvider { get; set; }

        [AssemblyInitialize]
        public static void AssemblyOneTimeSetUp(TestContext testContext)
        {
            JiraInfoProvider = new JiraInfoProvider();
        }

        [AssemblyCleanup]
        public static void AssemblyOneTimeCleanup()
        {
            TestReporter.GenerateTestResultXml();
        }
    }
}
