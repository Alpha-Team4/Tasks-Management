namespace TasksManagement.Models.Contracts;
public interface IHasTasks
{
    public IList<ITask> Tasks { get; }

    public void AddTask(ITask task);
}
