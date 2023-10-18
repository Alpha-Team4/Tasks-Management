using System.Text;
using TasksManagement.Commands.Enums;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models;
public class Comment : IComment
{
    private const int CommentMinLength = 3;
    private const int CommentMaxLength = 300;
    private const string InvalidCommentError = "Content must be between 3 and 300 characters long!";

    private const string CommentHeader = "    ----------";

    private string content;

    public Comment(string content, string author)
    {
        Content = content;
        Author = author;
    }

    public string Content
    {
        get => content;
        private set
        {
            Validator.ValidateIntRange(value.Length, CommentMinLength, CommentMaxLength, InvalidCommentError);
            content = value;
        }
    }

    public string Author { get; }

    public override string ToString()
    {
        return $"""
                {CommentHeader}
                  {Content}
                    Member: {Author}
                {CommentHeader}
                """;
    }
}
