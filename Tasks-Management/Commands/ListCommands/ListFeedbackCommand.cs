using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.ListCommands;
public class ListFeedbackCommand : BaseCommand
{
    public ListFeedbackCommand(IList<string> commandParameters, IRepository repository) : base(commandParameters, repository)
    {

    }

    public override string Execute()
    {
        var feedback = Repository.FindAllTasks().OfType<IFeedback>().ToList();

    }
}
