
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Commands.AssignCommands
{
    public class AssignBugCommand : BaseCommand
    {
        private const int ExpectedNumberOfArguments = 4;
        private const string BugAlreadyAssignedErrorMessage = "Invalid input. Bug {0} is already assigned.";
        private const string BugAddedToMemberMessage = "Bug {0} assigned to member {1}.";
        public AssignBugCommand(IList<string> commandParameters, IRepository repository)
            : base(commandParameters, repository)
        {
        }

        public override string Execute()
        {
            if (CommandParameters.Count != ExpectedNumberOfArguments)
            {
                throw new InvalidUserInputException($"Invalid number of arguments. " +
                    $"Expected: {ExpectedNumberOfArguments}, Received: {CommandParameters.Count}");
            }

            var team = Repository.FindTeamByName(CommandParameters[0]);
            var board = Repository.FindBoardByName(CommandParameters[1], team);
            var bug = Repository.FindTaskByTitle<IBug>(CommandParameters[2], board);
            var member = Repository.FindMemberByName(CommandParameters[3]);

            if (bug.Assignee != null)
            {
                throw new InvalidUserInputException(string.Format(BugAlreadyAssignedErrorMessage, bug.Title));
            }

            member.AddTask(bug);
            bug.Assignee = member;
            return string.Format(BugAddedToMemberMessage, bug.Title, member.Name);
        }
    }
}
