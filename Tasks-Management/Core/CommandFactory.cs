﻿using TasksManagement.Commands.Contracts;
using TasksManagement.Core.Contracts;

namespace TasksManagement.Core;

public class CommandFactory : ICommandFactory
{
    private readonly IRepository repository;

    public CommandFactory(IRepository repository)
    {
        this.repository = repository;
    }

    public ICommand Create(string commandLine)
    {
        // RemoveEmptyEntries makes sure no empty strings are added to the result of the split operation.
        string[] arguments = commandLine.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        CommandType commandType = ParseCommandType(arguments[0]);
        List<string> commandParameters = ExtractCommandParameters(arguments);

        switch (commandType)
        {
            // TODO
        }
    }

    // Attempts to parse CommandType from a given string value.
    // If successful, returns the command enum value, otherwise returns null
    private CommandType ParseCommandType(string value)
    {
        Enum.TryParse(value, true, out CommandType result);
        return result;
    }

    // Receives a full line and extracts the parameters that are needed for the command to execute.
    // For example, if the input line is "FilterBy Assignee John",
    // the method will return a list of ["Assignee", "John"].
    private List<String> ExtractCommandParameters(string[] arguments)
    {
        List<string> commandParameters = new List<string>();

        for (int i = 1; i < arguments.Length; i++)
        {
            commandParameters.Add(arguments[i]);
        }

        return commandParameters;
    }
}