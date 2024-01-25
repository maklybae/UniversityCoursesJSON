using JSONCourse;

namespace CLI;

/// <summary>
/// Static class responsible for handling console-based dialogs and user input.
/// </summary>
internal static class ConsoleDialog
{
    /// <summary>
    /// Prints the help page with instructions, waits for user input.
    /// </summary>
    internal static void PrintHelpPage()
    {
        Console.CursorVisible = false;
        ConsoleOutput.ClearBuffer();
        ConsoleOutput.PrintWelcome();
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine(Constants.HelpText);
        Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}{Constants.Author}");
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine($"{Environment.NewLine}Press any button...");
        Console.ReadKey(true);
    }

    /// <summary>
    /// Takes user input to choose a sorting option for a specified field.
    /// </summary>
    /// <param name="field">The field for which the sorting option is chosen.</param>
    /// <returns>The chosen sorting option.</returns>
    internal static CourseHelper.CourseSortingOptions InputSortingOption(string field)
    {
        while (true)
        {
            ConsoleOutput.ClearBuffer();
            Console.WriteLine($"Choosing sorting option for field: {field}");
            Console.WriteLine("Press QWERTY 'a' to sort with the ascending option");
            Console.WriteLine("Press QWERTY 'd' to sort with the descending option");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.A:
                    return CourseHelper.CourseSortingOptions.Ascending;
                case ConsoleKey.D:
                    return CourseHelper.CourseSortingOptions.Descending;
                default:
                    ConsoleOutput.PrintIssue("Incorrect button has been pressed", "Try again", true);
                    continue;
            }
        }
    }

    /// <summary>
    /// Takes user input to create a dictionary of sorting options for a list of fields.
    /// </summary>
    /// <param name="fields">List of fields for which sorting options are chosen.</param>
    /// <returns>Dictionary containing sorting options for each field.</returns>
    internal static Dictionary<string, CourseHelper.CourseSortingOptions> InputSortingOptionsDictionary(List<string> fields)
    {
        Dictionary<string, CourseHelper.CourseSortingOptions> res = new();
        foreach (var field in fields)
        {
            res.Add(field, InputSortingOption(field));
        }
        return res;
    }

    /// <summary>
    /// Takes user input to determine whether to overwrite an existing file.
    /// </summary>
    /// <returns>True if the user chooses to overwrite, false otherwise.</returns>
    internal static bool InputOverwriteFile()
    {
        while (true)
        {
            ConsoleOutput.ClearBuffer();
            Console.WriteLine("Press QWERTY 'y' to overwrite existing file");
            Console.WriteLine("Press QWERTY 'n' otherwise");
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Y:
                    return true;
                case ConsoleKey.N:
                    return false;
                default:
                    ConsoleOutput.PrintIssue("Incorrect button has been pressed", "Try again", true);
                    continue;
            }
        }
    }

    /// <summary>
    /// Takes user input for a string field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted string.</returns>
    internal static string InputStringField(string requestedName)
    {
        Console.Write($"Input text field \"{requestedName}\" to search: ");
        return ConsoleInput.InputStringWithCursor();
    }

    /// <summary>
    /// Takes user input for a boolean field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>True if the user presses 't', false if 'f'.</returns>
    internal static bool InputBooleanField(string requestedName)
    {
        Console.WriteLine($"Choosing sorting option for field: {requestedName}");
        Console.Write("Press QWERTY 't' for true, 'f' for false: ");
        while (true)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.T:
                    Console.WriteLine("true");
                    return true;
                case ConsoleKey.F:
                    Console.WriteLine("false");
                    return false;
            }
        }
    }

    /// <summary>
    /// Takes user input for an integer field.
    /// </summary>
    /// <param name="requestedName">The name of the field being input.</param>
    /// <returns>User-inputted integer.</returns>   
    internal static int InputIntegerField(string requestedName)
    {
        while (true)
        {
            Console.Write($"Input integer field \"{requestedName}\" to search: ");
            try
            {
                return ConsoleInput.InputIntegerInRange(int.MinValue, int.MaxValue);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Wrong value! Try again!");
            }
        }

    }

    /// <summary>
    /// Takes user input for an integer within a specified range until a correct value is entered.
    /// </summary>
    /// <param name="lowerBound">The lower bound of the acceptable range.</param>
    /// <param name="upperBound">The upper bound of the acceptable range.</param>
    /// <returns>User-inputted integer within the specified range.</returns>
    internal static int InputIntegerInRangeUntilCorrect(int lowerBound, int upperBound)
    {
        while (true)
        {
            ConsoleOutput.ClearBuffer();
            Console.Write($"Enter an integer in the range [{lowerBound}, {upperBound}]: ");
            try
            {
                return ConsoleInput.InputIntegerInRange(lowerBound, upperBound);
            }
            catch (ArgumentException)
            {
                ConsoleOutput.PrintIssue("Incorrect number", "Check your input and repeat an attempt", true);
            }
        }
    }
}
