using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TMNAdapter.Entities
{
	[Serializable, XmlRoot("tests")]
	public class TestResult
	{
		[XmlElement("test")]
		public Issue[] Issues { get; set; }
	}

}
