using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMNAdapter.Core.Common;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.MSTest.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            
        }

        /// <summary>
        /// Parse TRXFile to test objects. 
        /// </summary>
        /// <returns>List of IssueModels for each test</returns>
        public static void ParseTRXToIssueModel()
        {
            XDocument xdoc = XDocument.Load("ResultTMNFile.trx");//path to TRXFile
            XNamespace df = xdoc.Root.Name.Namespace;

            if (xdoc != null)
                foreach (var child in xdoc.Descendants(df + "Results").Elements())
                {
                    IssueModel issue = new IssueModel();
                    string nameAttribute = child.Attribute("testName").Value;

                    string durationAttribute = child.Attribute("duration").Value;
                    DateTime dt = Convert.ToDateTime(durationAttribute);
                    long time = dt.TimeOfDay.Milliseconds;

                    string statusAttribute = child.Attribute("outcome").Value;
                    string message = string.Empty;
                    string stackTrace = string.Empty;

                    switch (statusAttribute)
                    {
                        case "Failed":
                            issue.Status = Status.Failed;
                            break;
                        case "Passed":
                            issue.Status = Status.Passed;
                            break;
                        default:
                            break;
                    }

                    foreach (var item in child.Descendants(df + "Message"))
                    {
                        message = item.Value;
                    }

                    foreach (var item in child.Descendants(df + "StackTrace"))
                    {
                        stackTrace = item.Value;
                    }

                    issue.Key = nameAttribute;//todo: Temporary. Change name of test to Key attribute
                    issue.Summary = string.Concat(message, stackTrace);
                    issue.Time = time;

                    IssueManager.AddIssue(issue);
                }

            TestReporter.GenerateTestResultXml();
        }
    }
}
