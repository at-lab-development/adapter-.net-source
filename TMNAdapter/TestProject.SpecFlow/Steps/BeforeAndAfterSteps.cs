using System;
using TechTalk.SpecFlow;
using TestProject.SpecFlow.BaseObject;
using TestProject.SpecFlow.Common;

namespace TestProject.SpecFlow.Steps
{
    [Binding]
    public class BeforeAndAfterSteps
    {
        private readonly ScenarioContext scenarioContext;

        public BeforeAndAfterSteps(ScenarioContext scenarioContext)
        {
            if (scenarioContext == null) throw new ArgumentNullException("scenarioContext");
            this.scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void ScenarioInit()
        {
            Browser _browser = BrowserContainer.InitDriver(scenarioContext.ScenarioInfo.Title);
            _browser.Maximise();
            _browser.NavigateTo(Configuration.StartUrl);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            BrowserContainer.DeleteBrowser(scenarioContext.ScenarioInfo.Title);
        }
    }
}
