# Test Management NAdapter
This is a Testing Adapter based on NUnit Testing Framework, which gathers basic build information and useful artifacts:
*  test status;
*  test execution time
*  titled values;
*  user defined files;
*  screenshots;
*  stack traces;

For further processing by [Jenkins Test Management plugin](https://github.com/at-lab-development/jenkins-test-management-plugin).

# How To Use With NUnit
1. **Download & Build** solution or **Download Ready to Use TTMNAdapter.NUnit** assembly
2. Make sure your **Test Project** is based on NUnit Testing Framework
3. Add **TMNAdapter.NUnit** and **TMNAdapter.Core** references to your **Test Project**
4. Mark your test with `JiraTestMethod` attribute to link it with Jira issue
5. Use `JiraInfoProvider` to attach additional data for Jira issues
6. Add `[assembly: GenerateTestReportForJIRA]` line to any test class file in your **Test Project**
	```csharp
	[assembly: GenerateTestReportForJIRA]
	namespace TestProject
	{
		[TestFixture]
		public class TestClass
		{
			// use Screenshoter to attach screenshots to the Jira issue
			// or you can add screenshot file with JiraInfoProvider
			[SetUp]
			public void Initialize()
			{
				Screenshoter.Instance.Initialize(Browser.Instance.GetDriver());
			}

			[Test]
			[JiraTestMethod("ISSUE-KEY")]
			public void AwesomeTestMethod()
			{
				// use this anywhere in your tests...

				// attach parameter to the Jira issue
				JiraInfoProvider.SaveParameter("string", "awesome-string");
				JiraInfoProvider.SaveParameter("bool", true);
				JiraInfoProvider.SaveParameter("int", 42);

				// attach file or screenshot to the Jira issue
				var fileInfo = new FileInfo("file-path");
				JiraInfoProvider.SaveAttachment(fileInfo);

				// assert's message also will be recorded
				Assert.Fail("You Shall Not Pass! ¯\_(ツ)_/¯");
			}

			//if you were using Screenshoter
			[TearDown]
    		public void Cleanup()
    		{
       			Screenshoter.Instance.CloseScreenshoter();
	    		}
		}
	}
	```
7. **Build & Run** your tests with **Jenkins** or manual
8. Check **target** folder with **jira-tm-report.xml** file and **attachments** folder for **TM Jenkins Plugin**
	```xml
	<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
	<tests>
		[...]
	    <test>
		    <key>EPMFARMATS-2447</key>
			<status>Failed</status>
		    <summary>Testing failed test behavior</summary>
		    <time>00m:00s:023ms</time>
		    <attachments>
			    <attachment>\target\attachments\jenkins-oops.jpg</attachment
			    <attachment>\target\attachments\1527068327885\jenkins-oops.jpg</attachment>
			    <attachment>\target\attachments\stacktrace_2018-05-23T12-38-47.897.txt</attachment>
			</attachments>
			<parameters>
				<parameter>
					<title>Random number</title>
					<value>617832599</value>
				</parameter>
				<parameter>
					<title>Random boolean</title>
			        <value>0</value>
				</parameter>
				<parameter>
					<title>Some static string</title>
					<value>Hello, world!</value>
				</parameter>
			</parameters>
		</test>
		[...]
	</tests>
	```
9. Make sure [**TM Jenkins Plugin**](https://github.com/at-lab-development/jenkins-test-management-plugin) is installed and configured in Jenkins
10. Check issue in Jira

![jira-auto-comment](/images/jira-auto-comment.jpg)

# How To Use With MSTest
1. **Download & Build** solution or **Download Ready to Use TMNAdapter.MSTest** assembly
2. Make sure your **Test Project** is based on MSTest Testing Framework
3. Add **TMNAdapter.MSTest** and **TMNAdapter.Core** references to your **Test Project**
4. Use `JiraTestMethod` instead of standart `TestMethod` attribute to link tests with Jira issues
5. Use `JiraInfoProvider` to attach additional data for Jira issues
6. You need to invoke `TestReporter.GenerateTestResultXml()` method in `AssemblyCleanup` to gather all test results:
7. Your **Test Project** should look something like this:
	```csharp
	namespace TestProject
	{
		[TestClass]
		public class TestClass
		{
			// use Screenshoter to attach screenshots to the Jira issue
			// or you can add screenshot file with JiraInfoProvider
			[TestInitialize]
			public void Initialize()
			{
        			Screenshoter.Initialize(Browser.Instance.GetDriver()); 
    		}

    		[JiraTestMethod("ISSUE-KEY")]
    		public void AwesomeTestMethod()
    		{
        		// use this anywhere in your tests...

       	 		// attach parameter to the Jira issue
       	 		JiraInfoProvider.SaveParameter("string", "awesome-string");
       	 		JiraInfoProvider.SaveParameter("bool", true);
       			JiraInfoProvider.SaveParameter("int", 42);

        			// attach file to the Jira issue
        			var fileInfo = new FileInfo("file-path");
        			JiraInfoProvider.SaveAttachment(fileInfo);
		
				// attach a screenshot to the Jira issue
				Screenshoter.Instance.TakeScreenshot();

        			// assert's message also will be recorded
        			Assert.Fail("You Shall Not Pass! ¯\_(ツ)_/¯");
	    		}
	
			//if you were using Screenshoter
			[TestCleanup]
    		public void Cleanup()
    		{
       			Screenshoter.Instance.CloseScreenshoter();
	    		}
		
			//gathering all test results
			[AssemblyCleanup]
			public static void AssemblyOneTimeCleanup()
			{
				TestReporter.GenerateTestResultXml();
			}
		}
	}
	```
7. **Build & Run** your tests with **Jenkins** or manual
8. Check **target** folder with **jira-tm-report.xml** file and **attachments** folder for **TM Jenkins Plugin**
	```xml
	<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
	<tests>
		[...]
	    <test>
		    <key>EPMFARMATS-2447</key>
			<status>Failed</status>
		    <summary>Testing failed test behavior</summary>
		    <time>00m:00s:023ms</time>
		    <attachments>
			    <attachment>\target\attachments\jenkins-oops.jpg</attachment
			    <attachment>\target\attachments\1527068327885\jenkins-oops.jpg</attachment>
			    <attachment>\target\attachments\stacktrace_2018-05-23T12-38-47.897.txt</attachment>
			</attachments>
			<parameters>
				<parameter>
					<title>Random number</title>
					<value>617832599</value>
				</parameter>
				<parameter>
					<title>Random boolean</title>
			        <value>0</value>
				</parameter>
				<parameter>
					<title>Some static string</title>
					<value>Hello, world!</value>
				</parameter>
			</parameters>
		</test>
		[...]
	</tests>
	```
9. Make sure [**TM Jenkins Plugin**](https://github.com/at-lab-development/jenkins-test-management-plugin) is installed and configured in Jenkins
10. Check issue in Jira

![jira-auto-comment](/images/jira-auto-comment.jpg)
