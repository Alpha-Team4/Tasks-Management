namespace TasksManagement.Models.Contracts;
public interface IHasHistory
{
    public IList<IEvent> History { get; }

    public void AddEvent(IEvent @event);
}
