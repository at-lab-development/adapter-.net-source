using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.MSTest;

namespace TMNAdapter.Tracking.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class AssemblyActionAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {

        }

        public void AfterTest(ITest test)
        {
            ExecutionTracker.GenerateTestResultXml();
        }

        public ActionTargets Targets => ActionTargets.Suite;
    }
}
