# Test Management NAdapter
This is an analog of adapter written in [Java](https://github.com/teo-rakan/test-management-adapter), which gathers basic build information and useful artifacts (titled values, user defined files, screenshoots, stack traces) for further processing by Jenkins Test Management plugin.

# How To Use
1. **Download & Build** solution or **Download Ready to Use NAdapter** assembly
2. Add NAdapter reference to your **Test Project**
3. Use `JiraIssueKey` attribute to mark your tests and `JiraInfoProvider` to attach additional data for Jira issues
	```csharp
	[JiraIssueKey("ISSUE-KEY")]
	public void AwesomeTestMethod()
	{
		// use this anywhere ...

		// attach parameter or value to the Jira issue
		JiraInfoProvider.SaveParameter("Title", "string");

		// attach file to the Jira issue
		var fileInfo = new FileInfo("file-path");
		JiraInfoProvider.SaveAttachment(fileInfo);

		// use this anywhere ...
	}
	```
4. **Build & Run** your tests with **Jenkins** or manual
5. Check **target** folder with **tm-testing.xml** file and **attachments** folder for **TM Jenkins Plugin**
	```xml
	<?xml version="1.0" encoding="UTF-8" standalone="yes"?>
	<tests>
		[...]
	    <test>
	        <key>EPMFARMATS-828</key>
	        <status>Failed</status>
	        <summary>Assertion failed: expected [true] but found [false]</summary>
	        <time>0 ms</time>
	    </test>
	    <test>
	        <key>EPMFARMATS-1010</key>
	        <status>Failed</status>
	        <summary>Failed due to: / by zero. Full stack trace attached as stacktrace_2018-05-02T16-48-16.509.txt</summary>
	        <time>0.014 s</time>
	        <attachments>
	            <attachment>\target\attachments\stacktrace_2018-05-02T16-48-16.509.txt</attachment>
	            <attachment>\target\attachments\276658525531180\jenkins-oops.jpg</attachment>
	            <attachment>\target\attachments\276658534421313\jenkins-oops.jpg</attachment>
	        </attachments>
	        <parameters>
	            <parameter>
	                <title>Random number</title>
	                <value>575273659</value>
	            </parameter>
	            <parameter>
	                <title>Random boolean</title>
	                <value>true</value>
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

# Architecture
![Scheme](https://github.com/teo-rakan/test-management-adapter/blob/master/images/readme_scheme.jpg)