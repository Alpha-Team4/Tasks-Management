using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;

namespace TasksManagement.Commands.TaskCommands;
public class ChangeStorySize : BaseCommand
{
    public const int ExpectedNumberOfArguments = 4;

    public ChangeStorySize(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
    {
    }

    public override string Execute()
    {
        
    }
}
