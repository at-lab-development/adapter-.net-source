using System;
using System.Xml.Serialization;

namespace TMNAdapter.Core.Entities
{
    [Serializable]
    public class TestParameters
    {
        [XmlElement("title")] public string Title { get; set; }

        [XmlElement("value")] public string Value { get; set; }

        public TestParameters(String title, String value)
        {
            Title = title;
            Value = value;
        }

        public TestParameters()
        {
        }
    }
}
