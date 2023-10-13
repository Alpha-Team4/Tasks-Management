namespace TasksManagement.Models.Contracts
{
    public interface ICommentable
    {
        IList<IComment> Comments { get; }

        void AddComment(IComment comment);

        void RemoveComment(IComment comment);
    }
}
