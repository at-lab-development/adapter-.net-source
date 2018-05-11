using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TMNAdapter.Entities
{
    [Serializable, XmlRoot("tests")]
	public class TestResult
	{
        [XmlElement("test")]
        public List<Issue> Issues { get; set; }
    }

}
