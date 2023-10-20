using TasksManagement.Commands.Enums;
using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models;
public class Feedback : Task, IFeedback
{
    private const int RatingMinValue = 0;
    private const int RatingMaxValue = 5;
    private const string RatingChangeMessage = "Feedback '{0}' rating changed to '{1}'.";
    private const string StatusChangeMessage = "Feedback '{0}' status changed to '{1}'.";

    private Rating rating;
    private StatusFeedback status;

    public Feedback(string title, string description)
        : base(title, description)
    {
        rating = Rating.NoRating;
        status = StatusFeedback.New;
    }

    public Rating Rating
    {
        get => rating;
        set
        {
            if (!isInitializing)
            {
                var changeMessage = string.Format(RatingChangeMessage, Title, rating, value);
                AddEvent(changeMessage);
            }

            rating = value;
        }
    }

    public StatusFeedback Status
    {
        get => status;
        set
        {
            var changeMessage = string.Format(StatusChangeMessage, Title, status, value);
            AddEvent(changeMessage);

            status = value;
        }
    }

    public override string GetCurrentStatus()
    {
        return Status.ToString();
    }

    public override string ToString()
    {
        return $"""
                Feedback (ID: {Id})
                  Title: {Title}
                  Description: {Description}
                  Status: {status}
                  Rating: {rating}
                    {ShowAllComments}
                """;
    }
}
