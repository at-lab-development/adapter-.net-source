using System;
using System.Collections.Generic;
using System.Xml.Linq;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.MSTest
{
    class MSTestTRXParser
    {
        /// <summary>
        /// Parse TRXFile to test objects. 
        /// </summary>
        /// <returns>List of IssueModels for each test</returns>
        public List<IssueModel> ParseTRXToIssueModel()
        {
            IssueModel im = new IssueModel();
            List<IssueModel> tests = new List<IssueModel>();

            XDocument xdoc = XDocument.Load("ResultTMNFile.trx");//path to TRXFile
            XNamespace df = xdoc.Root.Name.Namespace;

            if (xdoc != null)
                foreach (var child in xdoc.Descendants(df + "Results").Elements())
                {
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
                                im.Status = Status.Failed;
                            break;
                        case "Passed":
                            im.Status = Status.Passed;
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

                    im.Key = nameAttribute;//todo: Temporary. Change name of test to Key attribute
                    im.Summary = string.Concat(message, stackTrace);
                    tests.Add(im);
                }
            return tests;
        }
    }
}
