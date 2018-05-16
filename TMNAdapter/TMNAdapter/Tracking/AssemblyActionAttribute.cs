using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using TMNAdapter.MSTest;

namespace TMNAdapter.Tracking
{
    [AttributeUsage(AttributeTargets.Assembly, Inherited = false, AllowMultiple = false)]
    public class AssemblyActionAttribute : Attribute, ITestAction
    {
        public void BeforeTest(ITest test)
        {
            Debug.WriteLine("BeforeAssembly invoked");
        }

        public void AfterTest(ITest test)
        {
            Debug.WriteLine("AfterAssembly invoked");
            ExecutionTracker.GenerateTestResultXml();
        }

        public ActionTargets Targets => ActionTargets.Suite;
    }
}
