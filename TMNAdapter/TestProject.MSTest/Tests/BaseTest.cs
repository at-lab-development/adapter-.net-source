using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TMNAdapter.MSTest.Tracking;

namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class BaseTest
    {
        protected JiraInfoProvider JiraInfoProvider { get; set; }
        protected TestContext TestContext { get; set; }

        [ClassInitialize]
        public void OneTimeSetUp()
        {
            JiraInfoProvider = new JiraInfoProvider(TestContext);
        }
    }
}
