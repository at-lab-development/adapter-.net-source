using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TMNAdapter.Entities
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

		public Issue(string issueKey, TestResult status)
		{
			IssueKey = issueKey;
			Status = status.ToString();
		}

		public Issue(string issueKey, TestResult status, string time)
		{
			IssueKey = issueKey;
			Status = status.ToString();
			Time = time;
		}


	}
}
