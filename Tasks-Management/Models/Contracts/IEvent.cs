namespace TasksManagement.Models.Contracts;
public interface IEvent
{
    public string Description { get; }

    public DateTime Time { get; }
}
