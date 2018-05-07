using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMNAdapter.Entities;

namespace TMNAdapter.Utilities
{
  public class JiraInfoProvider
  {
    private static Dictionary<string, List<TestParameters>> jiraKeyParameters = new Dictionary<string, List<TestParameters>>();
    private static Dictionary<string, List<string>> jiraKeyAttachments = new Dictionary<string, List<string>>();

    //TODO: to write methods SaveFile() and FindJIRATestKey();
    private static string FindJiraTestKey()
    {
      //TODO: to write realization
      return null;
    }


    public static void SaveFile(string filePath)
    {

      //TODO: to write realization

      //using ()
      //{

      //}
    }

    public static void SaveValue(string title, string value)
    {
      string key = FindJiraTestKey();
      if (key != null)
      {
        TestParameters parameters = new TestParameters(); // TestParameters parameters = new TestParameters(title, value != null ? value : "null");
        if (jiraKeyParameters.ContainsKey(key))
        {
          jiraKeyParameters[key].Add(parameters);
        }
        else
        {
          List <TestParameters> newTestParameters = new List<TestParameters>();
          newTestParameters.Add(parameters);
          jiraKeyParameters.Add(title, newTestParameters);
        }
      }
    }

    public static void SaveValue(string title, int value)
    {
      SaveValue(title, value.ToString());
    }

    public static void SaveValue(string title, double value)
    {
      SaveValue(title, value.ToString());
    }

    public static void SaveValue(string title, bool value)
    {
      SaveValue(title, value.ToString());
    }

    public static void SaveValue(string title, object value)
    {
      SaveValue(title, value?.ToString());
    }

    public static void CleanFor(string issueKey)
    {
      if (jiraKeyParameters.ContainsKey(issueKey))
      {
        jiraKeyParameters.Remove(issueKey);
      }
      if (jiraKeyAttachments.ContainsKey(issueKey))
      {
        jiraKeyAttachments.Remove(issueKey);
      }
    }
    
    public static List<TestParameters> GetIssueParameters(string issueKey)
    {
      return jiraKeyParameters[issueKey] ?? null;
    }

    public static List<string> GetIssueAttachments(string issueKey)
    {
      return jiraKeyAttachments[issueKey] ?? null;
    }
  }
}
