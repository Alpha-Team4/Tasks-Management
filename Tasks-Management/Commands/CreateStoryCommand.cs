using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands;

public class CreateStoryCommand : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;

    public CreateStoryCommand(IList<string> commandParameters, IRepository repository) 
        : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        if (this.CommandParameters.Count < ExpectedNumberOfArguments)
        {
            throw new InvalidUserInputException($"Invalid number of arguments. Expected: {ExpectedNumberOfArguments}, Received: {this.CommandParameters.Count}");
        }

        var title = CommandParameters[0];
        var description = CommandParameters[1];
        var teamName = CommandParameters[2];
        var boardName = CommandParameters[3];

        var story = Repository.CreateStory(title, description, teamName, boardName);
        return $"Story with ID '{story.Id}' was created.";
    }
}