using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands.ShowCommands;
public class ShowTeamsCommand : BaseCommand
{
    private const string NoTeamsMessage = "No teams found.";

    public ShowTeamsCommand(IRepository repository)
        : base(repository)
    {
    }

    public override string Execute()
    {
        if (!Repository.Teams.Any())
        {
            throw new EntityNotFoundException(NoTeamsMessage);
        }

        return string.Join(Environment.NewLine,
            Repository.Teams.Select(team => team.Name));
    }
}

