using TasksManagement.Commands.Contracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Core;

public class Engine : IEngine
{
    private const string TerminationCommand = "exit";
    private const string EmptyCommandError = "Command cannot be empty.";

    private readonly ICommandFactory commandFactory;

    public Engine(ICommandFactory commandFactory)
    {
        this.commandFactory = commandFactory;
    }

    public void Start()
    {
        while (true)
        {
            try
            {
                string inputLine = Console.ReadLine().Trim();

                if (inputLine == string.Empty)
                {
                    Console.WriteLine(EmptyCommandError);
                    continue;
                }

                if (inputLine.ToLower() == TerminationCommand)
                {
                    break;
                }

                ICommand command = commandFactory.Create(inputLine);
                string result = command.Execute();
                Console.WriteLine(result.Trim());
            }
            catch (InvalidUserInputException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (EntityNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
