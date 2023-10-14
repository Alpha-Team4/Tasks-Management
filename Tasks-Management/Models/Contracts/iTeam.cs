namespace TasksManagement.Models.Contracts;
public interface ITeam
{
    string Name { get; }

    IList<IMember> Members { get; }

    IList<IBoard> Boards { get; }

    public void AddMember(IMember member);

    public void AddBoard(IBoard board);
}