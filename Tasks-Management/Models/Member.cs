using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Member : IMember
{
    private const int MemberNameMinLength = 5;
    private const int MemberNameMaxLength = 15;
    private string MemberNameErrorMessage = "Member name must be between {0} and {1} characters.";

    private string name;
    private IList<IEvent> history = new List<IEvent>();
    private IList<ITask> tasks = new List<ITask>(); 

    public Member(string name)
    {

        this.Name = name;
    } 
    public string Name
    {
        get { return name; }
        private set
        { 
            Validator.ValidateStringLength(value, MemberNameMinLength, MemberNameMaxLength, 
                string.Format(MemberNameErrorMessage, MemberNameMinLength, MemberNameMaxLength));
            name = value; 
        }
    }

    public IList<IEvent> History
    {
        get { return new List<IEvent>(history); }   
    } 

    public IList<ITask> Tasks
    {
        get { return new List<ITask>(tasks);}
    }

    public void AddEvent(IEvent eventToAdd)
    {
        this.history.Add(eventToAdd);
    }

    public void AddTask(ITask task)
    {
        this.tasks.Add(task);
    }
}
