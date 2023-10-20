using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Core.Contracts;
using TasksManagement.Core;
using TasksManagement.Exceptions;
using TasksManagement.Models.Contracts;
using TasksManagement.Commands.Abstracts;

namespace TasksManagement.Commands.AssignCommands
{
    public class UnassignBugCommand : BaseCommand
    {
        private const int ExpectedNumberOfArguments = 4;
        private const string BugNotAssignedErrorMessage = "Bug {0} is not assigned to member {1}.";
        private const string BugUnassignedMessage = "Bug {0} unassigned from member {1}.";
        public UnassignBugCommand(IList<string> commandParameters, IRepository repository)
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

            if (!member.Tasks.Contains(bug))
            {
                throw new EntityNotFoundException(BugNotAssignedErrorMessage);
            }

            member.RemoveTask(bug);
            bug.Assignee = null;
            return string.Format(BugUnassignedMessage, bug.Title, member.Name);
        }
    }
}
