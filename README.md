# Test Management NAdapter
This is a Testing Adapter based on NUnit Testing Framework, which gathers basic build information and useful artifacts:
*  test status;
*  test execution time
*  titled values;
*  user defined files;
*  screenshots;
*  stack traces;

For further processing by [Jenkins Test Management plugin](https://github.com/teo-rakan/test-management-jenkins-plugin).

# How To Use
1. **Download & Build** solution or **Download Ready to Use TMNAdapter** assembly
2. Make sure your **Test Project** is based on NUnit Testing Framework
3. Add **TMNAdapter** reference to your **Test Project**
4. Mark your test with `JiraTestMethod` attribute to link it with Jira issue
5. Use `JiraInfoProvider` to attach additional data for Jira issues
6. Add `[assembly: GenerateTestReportForJIRA]` line to any test class file in your **Test Project**
	```csharp
	[assembly: GenerateTestReportForJIRA]
	namespace TestProject
	{
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
	}
	```
7. **Build & Run** your tests with **Jenkins** or manual
8. Check **target** folder with **tm-testing.xml** file and **attachments** folder for **TM Jenkins Plugin**
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
9. Make sure [**TM Jenkins Plugin**](https://github.com/teo-rakan/test-management-jenkins-plugin) is installed and configured in Jenkins
10. Check issue in Jira

![jira-auto-comment](/uploads/25f8de4d7e0e854834c595d2cb4699f6/jira-auto-comment.jpg)
