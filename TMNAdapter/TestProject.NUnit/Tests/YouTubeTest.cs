using NUnit.Framework;
using TestProject.NUnit.Common;
using TMNAdapter.Tracking.Attributes;
using TMNAdapter.NUnit.Utilities;

namespace TestProject.NUnit.Tests
{
    [TestFixture]
    public class YouTubeTest : BaseTest
    {
        [SetUp]
        public void Initialize()
        {
            Screenshoter.Initialize(Browser.Instance.GetDriver());
        }

        [Test]
        [JiraIssueKey("EPMFARMATS-2466")]
        public void AlwaysPassedTest()
        {
            YouTubePage page = new YouTubePage("https://www.youtube.com/watch?v=UKKYpdWPSL8");
            string author = page.GetAuthorName();

            JiraInfoProvider.SaveParameter("Author", author);
            JiraInfoProvider.SaveParameter("Title", page.GetVideoTitle());

            Assert.AreEqual(author, "EPAM Systems Global");
        }

        [Test]
        [JiraIssueKey("EPMFARMATS-2470")]
        public void AlwaysFailedTest()
        {
            YouTubePage page = new YouTubePage("https://www.youtube.com/watch?v=sU4i4DTr1HQ");
            string author = page.GetAuthorName();
            string title = page.GetVideoTitle();

            JiraInfoProvider.SaveParameter("Author", author);
            JiraInfoProvider.SaveParameter("Title", title);

            Screenshoter.TakeScreenshot();

            Assert.AreEqual("Atlassian", author);
        }

        [TearDown]
        public void Close()
        {
            Screenshoter.Initialize(null);
            Browser.Instance.Quit();
        }
    }
}
