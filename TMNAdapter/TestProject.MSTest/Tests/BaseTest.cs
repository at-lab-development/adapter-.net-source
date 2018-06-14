using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.Core.Common;
using TMNAdapter.MSTest.Tracking;
using TMNAdapter.MSTest.Utilities;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected static JiraInfoProvider JiraInfoProvider { get; set; }
        protected static TestContext _testContext;

        [AssemblyInitialize]
        public static void AssemblyOneTimeSetUp(TestContext testContext)
        {
            _testContext = testContext;
            JiraInfoProvider = new JiraInfoProvider(testContext);
        }

        [AssemblyCleanup]
        public static void AssemblyOneTimeCleanup()
        {
            TestReporter.GenerateTestResultXml();
        }
    }
}
