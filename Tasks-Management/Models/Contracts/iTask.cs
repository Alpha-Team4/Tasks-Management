namespace TasksManagement.Models.Contracts;
public interface ITask : IHasHistory
{
    public int Id { get; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string AddComment(IComment comment);

    public string DeleteComment(IComment comment);

    public string ShowAllComments();

    public string PrintTaskActivity();

    public string GetCurrentStatus();
}
