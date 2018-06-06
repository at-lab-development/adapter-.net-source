using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
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
            FileUtils.Solution_dir = Path.GetDirectoryName(Path.GetDirectoryName(testContext.TestDir));

            JiraInfoProvider = new JiraInfoProvider();
        }

        [AssemblyCleanup]
        public static void AssemblyOneTimeCleanup()
        {
            TestReporter.GenerateTestResultXml();
        }
    }
}
