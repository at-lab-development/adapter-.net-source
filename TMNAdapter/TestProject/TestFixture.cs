using NUnit.Framework;
using TMNAdapter.MSTest;

namespace TestProject
{
    [SetUpFixture]
    class TestFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            ExecutionTracker.GenerateTestResultXml();
        }
    }
}
