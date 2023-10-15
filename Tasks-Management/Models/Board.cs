using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Board : IBoard
{
    private const int BoardNameMinLength = 5;
    private const int BoardNameMaxLength = 10;
    private string BoardNameErrorMessage = "Board name must be between {0} and {1} characters.";

    private string name;
    private IList<IEvent> history = new List<IEvent>();
    private IList<ITask> tasks = new List<ITask>();

    public Board(string name) 
    {
        this.Name = name;
    }  
    public string Name
    {
        get { return this.name; }
        private set
        {
            Validator.ValidateStringLength(value, BoardNameMinLength, BoardNameMaxLength,
                string.Format(BoardNameErrorMessage, BoardNameMinLength, BoardNameMaxLength));
            this.name = value;
        }
    }

    public IList<IEvent> History
    {
        get { return new List<IEvent>(this.history); }
    }

    public IList<ITask> Tasks
    {
        get { return new List<ITask>(this.tasks); }
    }

    public void AddEvent(IEvent eventToAdd)
    {
        this.history.Add(eventToAdd);
    }

    public void AddTask(ITask taskToAdd)
    {
        this.tasks.Add(taskToAdd);
    }
}
