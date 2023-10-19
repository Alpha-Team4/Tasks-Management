using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;

namespace TasksManagement.Commands.ListCommands;
public class ListFeedbackCommand : BaseCommand
{
    public ListFeedbackCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
    {
    }
}
