﻿namespace TasksManagement.Models.Contracts;
public interface IMember : IHasHistory, IHasTasks
{
    public string Name { get; }    
}
