namespace CLI;

/// <summary>
/// Represents a menu page for setting up options.
/// </summary>
internal class SettingUpPage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SettingUpPage"/> class.
    /// </summary>
    internal SettingUpPage()
    {
        Options = new()
        {
            (InputJSONDataFromConsole, "Input JSON data from console"),
            (InputJSONDataFromFile, "Input JSON data from local file"),
            (MoveToMainMenu, "Move to main menu with all possible data processing functions")
        };
    }

    /// <summary>
    /// Handles inputting JSON data from the console.
    /// </summary>
    private void InputJSONDataFromConsole() => ConsoleInput.InputJSONDataFromConsoleDialog();

    /// <summary>
    /// Handles inputting JSON data from a local file using the file manager menu.
    /// </summary>
    private void InputJSONDataFromFile() => AppMenu.AddMenuPageToStack(new FileManagerPage());

    /// <summary>
    /// Moves to the main menu with all possible data processing functions.
    /// </summary>
    private void MoveToMainMenu()
    {
        if (AppMenu.CoursesData != null)
            AppMenu.PopMenuPageFromStack();
        else
            ConsoleOutput.PrintIssue("Unable to move to functional menu without opening the file",
                "Load the file or enter data to console in setting up menu", true);   
    }

}
