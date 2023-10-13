using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManagement.Models.Contracts;
public interface IMember : IHasHistory, IHasTasks
{
    public string Name { get; }    
}
