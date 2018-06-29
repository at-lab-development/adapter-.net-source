using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.Core.Common;

namespace TMNAdapter.Tracking.Attributes
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class GenerateTestReportForJIRAAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {

        }

        public void AfterTest(ITest test)
        {
            TestReporter.GenerateTestResultXml();
        }

        public ActionTargets Targets => ActionTargets.Suite;
    }
}
