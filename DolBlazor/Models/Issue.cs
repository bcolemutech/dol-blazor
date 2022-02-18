namespace DolBlazor.Models;

public class Issue
{
    public Issue(IssueType issueType, string description)
    {
        IssueType = issueType;
        Description = description;
    }

    public string Description { get; set; }
    public IssueType IssueType { get; set; }
}
