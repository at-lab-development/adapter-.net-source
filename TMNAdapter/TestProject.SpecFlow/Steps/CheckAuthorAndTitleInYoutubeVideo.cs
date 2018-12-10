using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TestProject.SpecFlow.BaseObject;
using TestProject.SpecFlow.Common;
using TMNAdapter.SpecFlow.Common;

namespace TestProject.SpecFlow.Steps
{
    [Binding]
    public class CheckAuthorAndTitleInYoutubeVideo
    {
        private readonly ScenarioContext scenarioContext;
        private YouTubePage page;

        public CheckAuthorAndTitleInYoutubeVideo(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;
            page = new YouTubePage(BrowserContainer.GetBrowser(scenarioContext.ScenarioInfo.Title));
        }

        [Given("I navigate to (.*)")]
        public void INavigateToMainPage(string mainPage)
        {
            JiraInfoProvider.SaveParameter("title", page.GetVideoTitle(), scenarioContext);
        }

        [Then("the (.*) should be correct")]
        public void TheAuthorNameShouldBeCorrect(string authorName)
        {
            JiraInfoProvider.SaveParameter("authorName", page.GetAuthorName(), scenarioContext);
            string name = page.GetAuthorName();
            Assert.AreEqual(name, authorName);
        }

        [Then("the (.*) should be wrong")]
        public void TheTitleNameNameShouldBeWrong(string titleName)
        {
            JiraInfoProvider.SaveAttachment(new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\..\..\Resources\jenkins-oops.jpg"), scenarioContext);
            JiraInfoProvider.SaveParameter("authorName", page.GetAuthorName(), scenarioContext);
            string name = page.GetVideoTitle();
            Assert.AreEqual(name, titleName);
        }
    }
}
