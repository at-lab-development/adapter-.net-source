using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TMNAdapter.Tracking;

namespace TestProject.NUnit.Tests
{
    [TestFixture]
    public class BaseTest
    {
        protected JiraInfoProvider JiraInfoProvider { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            JiraInfoProvider = new JiraInfoProvider();
        }
    }
}
