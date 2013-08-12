using System;
using JetBrains.ActionManagement;
using JetBrains.Application.DataContext;
using JetBrains.ReSharper.Features.Common.Occurences.ExecutionHosting;
using JetBrains.ReSharper.Features.Finding.NavigateFromHere;

namespace ReTrack.GoToIssue
{
  [ActionHandler(ID)]
  public class GoToIssue : ContextSearchActionBase<IssueSearchProvider>
  {
    private const string ID = "ReTrack.GoToIssue";
  }

  public class IssueSearchProvider : IContextSearchProvider
  {
    public Action GetSearchesExecution(IDataContext dataContext, INavigationExecutionHost host)
    {
      return () =>
      {
        {
        }
      };
    }
  }
}