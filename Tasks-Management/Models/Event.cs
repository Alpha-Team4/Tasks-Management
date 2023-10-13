using TasksManagement.Models.Contracts;

namespace TasksManagement.Models;
public class Event : IEvent
{
    private readonly DateTime time;
    private readonly string description;

    public Event(string description)
    {
        this.description = description;

        time = DateTime.Now;
    }

    public DateTime Time => time;

    public string Description => description;

    public override string ToString()
    {
        return $"[{time.ToString("dd-MMM-yy, hh:mm:ss tt")}] {description}";
    }
}
