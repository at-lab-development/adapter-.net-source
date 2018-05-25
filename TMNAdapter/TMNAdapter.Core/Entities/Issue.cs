using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace TMNAdapter.Core.Entities
{
    [Serializable, XmlRoot("test")]
    public class Issue
    {
        [XmlElement("key")]
        public string IssueKey { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("summary")]
        public string Summary { get; set; }

        [XmlElement("time")]
        public string Time { get; set; }

        [XmlArray("attachments"), XmlArrayItem("attachment")]
        public List<String> Attachments { get; set; }

        [XmlArray("parameters"), XmlArrayItem("parameter")]
        public List<TestParameters> Parameters { get; set; }

        public Issue(string issueKey, Status status)
        {
            IssueKey = issueKey;
            Status = status.ToString();
        }

        public Issue(string issueKey, Status status, string time)
        {
            IssueKey = issueKey;
            Status = status.ToString();
            Time = time;
        }

        public Issue()
        {
        }
    }
}
