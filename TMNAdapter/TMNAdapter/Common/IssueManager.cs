using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMNAdapter.Common.Models;

namespace TMNAdapter.Common
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

            if (storedIssueModel.IsTestComplete)
            {
                return;
            }

            // AutoMapper is needed
            IssueModel mergedIssueModel = storedIssueModel + partialIssueModel;
            _issues.Remove(storedIssueModel);
            _issues.Add(mergedIssueModel);
        }

        public static List<IssueModel> GetIssues()
        {
            return _issues;
        }
    }
}
