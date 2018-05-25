using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TMNAdapter.Core.Entities
{
    [Serializable, XmlRoot("tests")]
    public class TestResult
    {
        [XmlElement("test")]
        public List<Issue> Issues { get; set; }

        public TestResult(List<Issue> issues) => Issues = issues;

        public TestResult()
        {
        }
    }

}
