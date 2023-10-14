using TasksManagement.Commands.Contracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Core;
public class Engine : IEngine
{
    private const string TerminationCommand = "exit";
    private const string EmptyCommandError = "Command cannot be empty.";

    private const ConsoleColor ConsoleInputColor = ConsoleColor.Gray;
    private const ConsoleColor ConsoleErrorColor = ConsoleColor.DarkRed;
    private const ConsoleColor ConsoleSuccessColor = ConsoleColor.DarkGreen;

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
                var inputLine = Console.ReadLine().Trim();

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
                var result = command.Execute();

                Console.ForegroundColor = ConsoleSuccessColor;
                Console.WriteLine(result.Trim());
                Console.ForegroundColor = ConsoleInputColor;
            }
            catch (InvalidUserInputException ex)
            {
                Console.ForegroundColor = ConsoleErrorColor;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleInputColor;
            }
            catch (EntityNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleErrorColor;
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleInputColor;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    Console.ForegroundColor = ConsoleErrorColor;
                    Console.WriteLine(ex.Message);
                    Console.ForegroundColor = ConsoleInputColor;
                }
                else
                {
                    Console.ForegroundColor = ConsoleErrorColor;
                    Console.WriteLine(ex);
                    Console.ForegroundColor = ConsoleInputColor;
                }
            }
        }
    }
}
