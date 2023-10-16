using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Member : IMember
{
    private const int MemberNameMinLength = 5;
    private const int MemberNameMaxLength = 15;
    private string MemberNameErrorMessage = "Member name must be between {0} and {1} characters.";
    private string NoTasksFoundMessage = "--NO TASKS--";
    private string NoHistoryFoundMessage = "--NO HISTORY--";

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

    public string PrintAllTasks()
    {
        if (!tasks.Any())
        {
            return NoTasksFoundMessage;
        }

        return string.Join(Environment.NewLine,
            tasks.Select(task => task.ToString()));
    }

    public string PrintHistory()
    {
        if (!history.Any())
        {
            return NoHistoryFoundMessage;
        }

        return string.Join(Environment.NewLine,
            history.Select(evt => evt.ToString()));
    }

    public override string ToString()
    {
        return $"""
                Name: {this.Name}
                   Tasks:
                     {PrintAllTasks()}
                       History:
                       {PrintHistory()}
                """;
    }
}
