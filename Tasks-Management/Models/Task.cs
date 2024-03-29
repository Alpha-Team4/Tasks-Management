﻿using System.Linq;
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

    private const string CommentAddedSuccessMessage = "'{0}' added a comment '{1}'";
    private const string CommentDeletedSuccessMessage = "Comment '{0}' was deleted.";
    private const string CommentDeletedErrorMessage = "Comment doesn't exist.";

    private const string TitleChangedMessage = "Title changed from '{0}' to '{1}'";
    private const string TitleSameMessage = "The new title is the same as the current title.";
    private const string DescriptionChangedMessage = "Description changed from '{0}' to '{1}'";
    private const string DescriptionSameMessage = "The new description is the same as the current description.";

    private const string NoCommentsHeader = "--NO COMMENTS--";
    private const string CommentsSeparator = "----------";
    private const string CommentsHeader = "--COMMENTS--";

    private static int lastIssuedId = 0;
    protected bool isInitializing = false;
    private readonly int id;
    private string title;
    private string description;
    protected readonly IList<IComment> comments = new List<IComment>();
    protected readonly IList<IEvent> history = new List<IEvent>();

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
                throw new InvalidUserInputException(TitleSameMessage);
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
                throw new InvalidUserInputException(DescriptionSameMessage);
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
    public IList<IEvent> History => new List<IEvent>(history);

    public IList<IComment> Comments => new List<IComment>(comments);

    private int GetNextAvailableId()
    {
        return ++lastIssuedId;
    }

    public void AddComment(IComment comment)
    {
        comments.Add(comment);

        var successMessage = string.Format(CommentAddedSuccessMessage, comment.Author, comment.Content);
        AddEvent(successMessage);
    }

    public void RemoveComment(IComment comment)
    {
        if (!comments.Contains(comment))
        {
            throw new EntityNotFoundException(CommentDeletedErrorMessage);
        }

        comments.Remove(comment);

        var successMessage = string.Format(CommentDeletedSuccessMessage, comment.Content);
        AddEvent(successMessage);
    }

    public string ShowAllComments()
    {
        if (!comments.Any())
        {
            return NoCommentsHeader;
        }

        StringBuilder sb = new();

        sb.AppendLine(CommentsHeader);

        foreach (var comment in comments)
        {
            sb.AppendLine(CommentsSeparator);
            sb.AppendLine(comment.ToString());
            sb.AppendLine(CommentsSeparator);
        }

        sb.AppendLine(CommentsHeader);

        return sb.ToString().Trim();
    }

    public void AddEvent(string message)
    {
        history.Add(new Event(message));
    }

    public string PrintTaskActivity()
    {
        return string.Join(Environment.NewLine,
                history.Select(evt => evt.ToString())
            );
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