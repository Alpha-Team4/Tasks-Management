namespace TasksManagement.Models.Contracts;
public interface ITask : IHasHistory, ICommentable
{
    public int Id { get; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string ShowAllComments();

    public string PrintTaskActivity();

    public string GetCurrentStatus();
}
