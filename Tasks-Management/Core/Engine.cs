using TasksManagement.Commands.Contracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Core;
public class Engine : IEngine
{
    private const string TerminationCommand = "exit";
    private const string EmptyCommandError = "Command cannot be empty.";
    private const string OperationSeparator = "--------------------";

    private const ConsoleColor ConsoleInputColor = ConsoleColor.Gray;
    private const ConsoleColor ConsoleOutputColor = ConsoleColor.DarkGray;
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

                var commandName = command.GetType().Name; // get command type to determine color output

                // different output color for success / list messages
                if (commandName.StartsWith("List") || commandName.StartsWith("Show"))
                {
                    PrintColoredLine(result.Trim(), ConsoleOutputColor);
                }
                else
                {
                    PrintColoredLine(result.Trim(), ConsoleSuccessColor);
                }
            }
            catch (InvalidUserInputException ex)
            {
                PrintColoredLine(ex.Message, ConsoleErrorColor);
            }
            catch (EntityNotFoundException ex)
            {
                PrintColoredLine(ex.Message, ConsoleErrorColor);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    PrintColoredLine(ex.Message, ConsoleErrorColor);
                }
                else
                {
                    PrintColoredLine(ex.Message, ConsoleErrorColor);
                }
            }
            finally
            {
                PrintColoredLine(OperationSeparator, ConsoleOutputColor);
            }
        }
    }

    static void PrintColoredLine(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }
}
