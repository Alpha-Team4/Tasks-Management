using TasksManagement.Commands.Contracts;
using TasksManagement.Core.Contracts;

namespace TasksManagement.Core;
internal class ShowActivityBoardCommand : ICommand
{
    private List<string> commandParameters;
    private IRepository repository;

    public ShowActivityBoardCommand(List<string> commandParameters, IRepository repository)
    {
        this.commandParameters = commandParameters;
        this.repository = repository;
    }
}