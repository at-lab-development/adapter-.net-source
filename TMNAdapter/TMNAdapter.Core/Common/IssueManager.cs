using System.Collections.Generic;
using System.Linq;
using TMNAdapter.Core.Common.Models;
using TMNAdapter.Core.Entities;

namespace TMNAdapter.Core.Common
{
    public static class IssueManager
    {
        private static List<IssueModel> _issues = new List<IssueModel>();

        public static IssueModel AddIssue(IssueModel partialIssueModel)
        {
            if (partialIssueModel == null)
            {
                return null;
            }

            IssueModel storedIssueModel = _issues.Find(x => x.Key == partialIssueModel.Key);

            if (storedIssueModel == null)
            {
                _issues.Add(partialIssueModel);
                return partialIssueModel;
            }

            if (storedIssueModel.IsTestComplete.HasValue && storedIssueModel.IsTestComplete.Value)
            {
                return storedIssueModel;
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
                Summary = UpdateSummary(storedIssueModel.Summary, partialIssueModel.Summary),
                Time = partialIssueModel.Time ?? storedIssueModel.Time,
                AttachmentFilePaths = storedIssueModel.AttachmentFilePaths,
                Parameters = storedIssueModel.Parameters,
                IsTestComplete = partialIssueModel.IsTestComplete ?? storedIssueModel.IsTestComplete
            };

            _issues.Remove(storedIssueModel);
            _issues.Add(mergedIssueModel);

            return mergedIssueModel;
        }

        public static List<IssueModel> GetIssues()
        {
            return _issues;
        }

        private static string UpdateSummary(string storedSummary, string updatingSummary)
        {
            if (string.IsNullOrWhiteSpace(updatingSummary))
            {
                return storedSummary;
            }

            if (string.IsNullOrWhiteSpace(storedSummary))
            {
                return updatingSummary;
            }
            else
            {
                return storedSummary + " " + updatingSummary;
            }
        }

        public static void SetAttachments(string issueKey, string relativePath)
        {
            AddIssue(new IssueModel()
            {
                Key = issueKey,
                AttachmentFilePaths = new List<string>() { relativePath }
            });
        }
    }
}
