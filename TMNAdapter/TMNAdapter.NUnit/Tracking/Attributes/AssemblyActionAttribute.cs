using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.Core.Common;

namespace TMNAdapter.Tracking.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class AssemblyActionAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            FileUtils.Solution_dir = TestContext.CurrentContext.WorkDirectory;
        }

        public void AfterTest(ITest test)
        {
            TestReporter.GenerateTestResultXml();
        }

        public ActionTargets Targets => ActionTargets.Suite;
    }
}
