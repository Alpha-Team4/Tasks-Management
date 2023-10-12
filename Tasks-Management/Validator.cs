using System.Text.RegularExpressions;
using TasksManagement.Exceptions;

namespace TasksManagement;
public static class Validator
{
    public static void ValidateIntRange(int value, int min, int max, string message)
    {
        if (value < min || value > max)
        {
            throw new InvalidUserInputException(message);
        }
    }

    public static void ValidateDecimalRange(decimal value, decimal min, decimal max, string message)
    {
        if (value < min || value > max)
        {
            throw new InvalidUserInputException(message);
        }
    }

    public static void ValidateStringLength(string value, int minLength, int maxLength, string message)
    {
        if (value.Length < minLength || value.Length > maxLength)
        {
            throw new InvalidUserInputException(message);
        }
    }

    public static void ValidateSymbols(string value, string pattern, string message)
    {
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);

        if (!regex.IsMatch(value))
        {
            throw new InvalidUserInputException(message);
        }
    }

    public static void ValidateNotNull(object value, string message)
    {
        if (value == null)
        {
            throw new InvalidUserInputException(message);
        }
    }
}
