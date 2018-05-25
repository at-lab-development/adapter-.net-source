using System.Collections.Generic;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Core.Common.Models
{
    public class IssueModel
    {
        public string Key { get; set; }
        public Status Status { get; set; }
        public string Summary { get; set; }
        public long? Time { get; set; }
        public List<string> AttachmentFilePaths { get; set; }
        public List<TestParameters> Parameters { get; set; }
        public bool? IsTestComplete { get; set; }

        public IssueModel()
        {
            AttachmentFilePaths = new List<string>();
            Parameters = new List<TestParameters>();
        }
    }
}
