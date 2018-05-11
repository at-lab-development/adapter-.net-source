using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestProject.Common;
using TMNAdapter.Common;
using TMNAdapter.Tracking;
using TMNAdapter.Utilities;

namespace TestProject
{
    [TestClass]
    public class YouTubeTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Screenshoter.Initialize(Browser.Driver);
        }

        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2466")]
        public void AlwaysPassedTest()
        {
            YouTubePage page = new YouTubePage("https://www.youtube.com/watch?v=UKKYpdWPSL8");
            string author = page.GetAuthorName();

            JiraInfoProvider.SaveParameter("Author", author);
            JiraInfoProvider.SaveParameter("Title", page.GetVideoTitle());

            Assert.AreEqual(author, "EPAM Systems Global");
        }

        [TestMethod]
        [JiraIssueKey("EPMFARMATS-2466")]
        public void AlwaysFailedTest()
        {
            YouTubePage page = new YouTubePage("https://www.youtube.com/watch?v=sU4i4DTr1HQ");
            string author = page.GetAuthorName();
            string title = page.GetVideoTitle();

            JiraInfoProvider.SaveParameter("Author", author);
            JiraInfoProvider.SaveParameter("Title", title);

            Assert.AreEqual("Atlassian", author);
        }

        [TestCleanup]
        public void Close()
        {
            Screenshoter.Initialize(null);
            Browser.Instance.Quit();
        }
    }
}
/*
@Listeners(com.epam.jira.testng.ExecutionListener.class)
}*/