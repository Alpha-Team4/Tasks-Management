namespace TasksManagement.Models.Contracts;
public interface IBoard : IHasHistory, IHasTasks
{
    public string Name { get; }
}