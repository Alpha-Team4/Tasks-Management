using TasksManagement.Core;
using TasksManagement.Core.Contracts;

namespace TasksManagement;

class Startup
{
    static void Main()
    {
        IRepository repository = new Repository();
        ICommandFactory commandFactory = new CommandFactory(repository);
        IEngine engine = new Engine(commandFactory);
        engine.Start();
    }
}
