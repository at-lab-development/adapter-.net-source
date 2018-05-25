using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace TestProject.MSTest.Tests
{
    [TestClass]
    public class YouTubeTest
    {
        [TestInitialize]
        public void Initialize()
        {
            //Screenshoter.Initialize(Browser.Instance.GetDriver());
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2466")]
        public void AlwaysPassedTest()
        {
            YouTubePage page = new YouTubePage("https://www.youtube.com/watch?v=UKKYpdWPSL8");
            string author = page.GetAuthorName();

            //JiraInfoProvider.SaveParameter("Author", author);
            //JiraInfoProvider.SaveParameter("Title", page.GetVideoTitle());

            Assert.AreEqual("author", "EPAM Systems Global");
        }

        [TestMethod]
        //[JiraIssueKey("EPMFARMATS-2470")]
        public void AlwaysFailedTest()
        {
            YouTubePage page = new YouTubePage("https://www.youtube.com/watch?v=sU4i4DTr1HQ");
            string author = page.GetAuthorName();
            string title = page.GetVideoTitle();

            //JiraInfoProvider.SaveParameter("Author", author);
            //JiraInfoProvider.SaveParameter("Title", title);

            //Screenshoter.TakeScreenshot();

            Assert.AreEqual("Atlassian", author);
        }

        [TestCleanup]
        public void Close()
        {
            //Screenshoter.Initialize(null);
            Browser.Instance.Quit();
        }
    }
}


