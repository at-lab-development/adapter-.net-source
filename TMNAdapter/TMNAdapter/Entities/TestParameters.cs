using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TMNAdapter.Entities
{
	[Serializable]
	public class TestParameters
	{

		[XmlElement("title")] public string Title { get; set; }

		[XmlElement("value")] public string Value { get; set; }

		public TestParameters(String title, String value)
		{
			this.Title = title;
			this.Value = value;
		}

	}
}
