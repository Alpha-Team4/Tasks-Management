using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Commands.Abstracts;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;

namespace TasksManagement.Commands;
public class ShowTeamsCommand : BaseCommand
{
    private const string NoTeamsMessage = "No teams found.";

    public ShowTeamsCommand(IRepository repository) 
        : base(repository)
    {
    }

    public override string Execute()
    {
        if (this.Repository.Teams.Count > 0)
        {
            var sb = new StringBuilder();
            foreach (var team in this.Repository.Teams)
            {
                sb.AppendLine(team.Name);
            }

            return sb.ToString().TrimEnd();
        }

        throw new EntityNotFoundException(NoTeamsMessage);
    }
}

