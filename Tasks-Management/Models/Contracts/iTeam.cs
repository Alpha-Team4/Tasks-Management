namespace TasksManagement.Models.Contracts;
public interface ITeam
{
    string Name { get; }

    IList<IMember> Members { get; }

    IList<IBoard> Borders { get; }

}
