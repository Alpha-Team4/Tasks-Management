﻿namespace TasksManagement.Models.Contracts;
public interface IComment
{
    string Content { get; }

    string Author { get; }
}
