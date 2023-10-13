using TasksManagement.Models.Contracts;
using TasksManagement.Models.Enums;

namespace TasksManagement.Models;
internal class Feedback : Task, IFeedback
{
    private const int RatingMinValue = 0;
    private const int RatingMaxValue = 5;
    private const string RatingChangeMessage = "Feedback rating changed from {0} to {1}.";
    private const string RatingErrorMessage = "Feedback rating must be between {0} and {1}.";

    private const string StatusChangeMessage = "Feedback status changed from {0} to {1}.";

    private int rating;
    private FeedbackStatus status;

    public Feedback(string title, string description, int rating)
        : base(title, description)
    {
        isInitializing = true;

        Rating = rating;
        status = FeedbackStatus.New;

        isInitializing = false;
    }

    public int Rating
    {
        get => rating;
        set
        {
            var errorMessage = string.Format(RatingErrorMessage, RatingMinValue, RatingMaxValue);
            Validator.ValidateIntRange(value, RatingMinValue, RatingMaxValue, errorMessage);

            if (!isInitializing)
            {
                var changeMessage = string.Format(RatingChangeMessage, rating, value);
                AddEvent(changeMessage);
            }

            rating = value;
        }
    }

    public FeedbackStatus Status
    {
        get => status;
        set
        {
            var changeMessage = string.Format(StatusChangeMessage, status, value);
            AddEvent(changeMessage);

            status = value;
        }
    }
}
