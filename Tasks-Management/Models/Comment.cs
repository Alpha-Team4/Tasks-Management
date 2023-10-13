using System.Text;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Comment : IComment
{
    private const int CommentMinLength = 3;
    private const int CommentMaxLength = 300;
    private const string InvalidCommentError = "Content must be between 3 and 300 characters long!";

    private const string CommentHeader = "    **********";

    private string content;

    public Comment(string content, string author)
    {
        this.Content = content;
        this.Author = author;
    }

    public string Content
    {
        get
        {
            return this.content;
        }
        private set
        {
            Validator.ValidateIntRange(value.Length, CommentMinLength, CommentMaxLength, InvalidCommentError);
            this.content = value;
        }
    }

    public string Author { get; }

    public override string ToString()
    {
        var output = new StringBuilder();

        output.AppendLine(CommentHeader);
        output.AppendLine($"    {this.Content}");
        output.AppendLine($"      Member: {this.Author}");
        output.Append(CommentHeader);

        return output.ToString();
    }
}
