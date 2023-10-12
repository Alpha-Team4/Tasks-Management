﻿namespace TasksManagement.Models.Contracts;
public interface ITask
{
    public int Id { get; }

    public string Title { get; }

    public string Description { get; }

    public string AddComment(IComment comment);

    public string DeleteComment(IComment comment);

    public string ShowAllComments();

    public string ShowAllEvents();
}
