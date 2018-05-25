using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Core.Common
{
    public static class IssueManager
    {
        private static List<IssueModel> _issues = new List<IssueModel>();

        public static void AddIssue(IssueModel partialIssueModel)
        {
            IssueModel storedIssueModel = _issues.Find(x => x.Key == partialIssueModel.Key);

            if (storedIssueModel == null)
            {
                _issues.Add(partialIssueModel);
                return;
            }

            if (storedIssueModel.IsTestComplete.HasValue && storedIssueModel.IsTestComplete.Value)
            {
                return;
            }

            if (partialIssueModel.AttachmentFilePaths != null && partialIssueModel.AttachmentFilePaths.Any())
            {
                storedIssueModel.AttachmentFilePaths.AddRange(partialIssueModel.AttachmentFilePaths);
            }

            if (partialIssueModel.Parameters != null && partialIssueModel.Parameters.Any())
            {
                storedIssueModel.Parameters.AddRange(partialIssueModel.Parameters);
            }
            
            IssueModel mergedIssueModel = new IssueModel()
            {
                Key = !string.IsNullOrWhiteSpace(partialIssueModel.Key) ? partialIssueModel.Key : storedIssueModel.Key,
                Status = partialIssueModel.Status != Status.None ? partialIssueModel.Status : storedIssueModel.Status,
                Summary = !string.IsNullOrWhiteSpace(partialIssueModel.Summary) ? partialIssueModel.Summary: storedIssueModel.Summary,
                Time = partialIssueModel.Time ?? storedIssueModel.Time,
                AttachmentFilePaths = storedIssueModel.AttachmentFilePaths,
                Parameters = storedIssueModel.Parameters,
                IsTestComplete = partialIssueModel.IsTestComplete ?? storedIssueModel.IsTestComplete
            };

            _issues.Remove(storedIssueModel);
            _issues.Add(mergedIssueModel);
        }

        public static List<IssueModel> GetIssues()
        {
            return _issues;
        }
    }
}
