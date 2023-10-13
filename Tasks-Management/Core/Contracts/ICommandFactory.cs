using TasksManagement.Commands.Contracts;

namespace TasksManagement.Core.Contracts;

public interface ICommandFactory
{
    ICommand Create(string commandLine);
}
