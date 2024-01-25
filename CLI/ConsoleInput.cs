namespace CLI;

/// <summary>
/// Static class responsible for handling user input through the console.
/// </summary>
internal static class ConsoleInput
{
    /// <summary>
    /// Takes user input for a string with cursor visibility management.
    /// </summary>
    /// <returns>User-inputted string.</returns>
    internal static string InputStringWithCursor()
    {
        Console.CursorVisible = true;
        string input = Console.ReadLine() ?? "";
        Console.CursorVisible = false;
        return input;
    }

    /// <summary>
    /// Takes user input for JSON data from the console.
    /// </summary>
    internal static void InputJSONDataFromConsoleDialog()
    {
        ConsoleOutput.ClearBuffer();
        Console.WriteLine("Put the text of your JSON file to the console.");
        Console.WriteLine($"After entering the data, press Ctrl+Z or Ctrl+D sequence and finally Enter.");
        Console.CursorVisible = true;
        DataConverterLinker.SetJSONData();
        Console.CursorVisible = false;
    }

    /// <summary>
    /// Takes user input for a filename.
    /// </summary>
    /// <returns>User-inputted filename.</returns>
    internal static string InputFilename()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a name of the JSON file with the extension: ");
        return InputStringWithCursor();
    }

    /// <summary>
    /// Takes user input for a full file path.
    /// </summary>
    /// <returns>User-inputted full file path.</returns>
    internal static string InputFullPath()
    {
        ConsoleOutput.ClearBuffer();
        Console.Write("Write a full path to file with the .json extension: ");
        return InputStringWithCursor();
    }

    /// <summary>
    /// Takes user input for an integer within a specified range.
    /// </summary>
    /// <param name="lowerBound">The lower bound of the acceptable range.</param>
    /// <param name="upperBound">The upper bound of the acceptable range.</param>
    /// <returns>User-inputted integer within the specified range.</returns>
    internal static int InputIntegerInRange(int lowerBound, int upperBound)
    {
        var input = InputStringWithCursor();

        // Check whether it fulfills requirements.
        if (int.TryParse(input, out int res) && lowerBound <= res && res <= upperBound)
        {
            return res;
        }
        else
        {
            throw new ArgumentException();
        }
    }
}
