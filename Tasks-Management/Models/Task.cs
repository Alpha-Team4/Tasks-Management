using System.Text;
using TasksManagement.Commands.Enums;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;

public abstract class Task : ITask
{
    private const int TitleMinLength = 10;
    private const int TitleMaxLength = 50;
    private const string TitleLengthErrorMessage = "Title length must be between {0} and {1} characters.";
    private const string TitleNullErrorMessage = "The title cannot be null.";
    private const int DescriptionMinLength = 10;
    private const int DescriptionMaxLength = 500;
    private const string DescriptionLengthErrorMessage = "Description length must be between {0} and {1} characters.";
    private const string DescriptionNullErrorMessage = "The description cannot be null.";

    private const string CommentAddedSuccessMessage = "Comment '{0}' was added.";
    private const string CommentDeletedSuccessMessage = "Comment '{0}' was deleted.";
    private const string CommentDeletedErrorMessage = "Comment doesn't exist.";

    private const string TitleChangedMessage = "Title changed from '{0}' to '{1}'";
    private const string DescriptionChangedMessage = "Description changed from '{0}' to '{1}'";

    private static int lastIssuedId = 0;
    protected bool isInitializing = false;
    private readonly int id;
    private string title;
    private string description;
    private readonly IList<IComment> comments = new List<IComment>();
    protected readonly IList<IEvent> eventsList = new List<IEvent>();

    public Task(string title, string description)
    {
        isInitializing = true;

        Title = title;
        Description = description;
        id = GetNextAvailableId();

        isInitializing = false;
    }

    public int Id => id;

    public string Title
    {
        get => title;
        set
        {
            if (title == value)
            {
                return;
            }

            var errorMessage = string.Format(TitleLengthErrorMessage, TitleMinLength, TitleMaxLength);
            Validator.ValidateNotNull(value, TitleNullErrorMessage);
            Validator.ValidateIntRange(value.Length, TitleMinLength, TitleMaxLength, errorMessage);

            if (!isInitializing)
            {
                var changeMessage = string.Format(TitleChangedMessage, title, value);
                AddEvent(changeMessage);
            }

            title = value;
        }
    }

    public string Description
    {
        get => description;
        set
        {
            if (description == value)
            {
                return;
            }

            var errorMessage = string.Format(DescriptionLengthErrorMessage, DescriptionMinLength, DescriptionMaxLength);
            Validator.ValidateNotNull(value, DescriptionNullErrorMessage);
            Validator.ValidateIntRange(value.Length, DescriptionMinLength, DescriptionMaxLength, errorMessage);

            if (!isInitializing)
            {
                var changeMessage = string.Format(DescriptionChangedMessage, description, value);
                AddEvent(changeMessage);
            }

            description = value;
        }
    }

    private int GetNextAvailableId()
    {
        return ++lastIssuedId;
    }

    public string AddComment(IComment comment)
    {
        comments.Add(comment);

        var successMessage = string.Format(CommentAddedSuccessMessage, comment.Content);
        AddEvent(successMessage);

        return successMessage;
    }

    public string DeleteComment(IComment comment)
    {
        if (!comments.Contains(comment))
        {
            throw new EntityNotFoundException(CommentDeletedErrorMessage);
        }

        comments.Remove(comment);

        var successMessage = string.Format(CommentDeletedSuccessMessage, comment.Content);
        AddEvent(successMessage);

        return successMessage;
    }

    public string ShowAllComments()
    {
        StringBuilder sb = new();

        foreach (var comment in comments)
        {
            sb.AppendLine(comment.ToString());
        }

        return sb.ToString().Trim();
    }

    protected void AddEvent(string message)
    {
        eventsList.Add(new Event(message));
    }

    public string ShowAllEvents()
    {
        StringBuilder sb = new();

        foreach (var evt in eventsList)
        {
            sb.AppendLine(evt.ToString());
        }

        return sb.ToString().Trim();
    }

    public abstract string GetCurrentStatus();

    public override string ToString()
    {
        return $"""
                {GetType().Name} (ID: #{Id})
                   Title: {Title}
                   Description: {Description}
                   Status: {GetCurrentStatus()}
                """;
    }
}