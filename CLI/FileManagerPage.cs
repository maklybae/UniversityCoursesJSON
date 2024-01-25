using System.Security;

namespace CLI;

/// <summary>
/// Represents a file manager menu that allows navigation through directories and opening JSON files.
/// </summary>
internal class FileManagerPage : MenuPage
{
    // The current directory being displayed
    private DirectoryInfo _currentDirectory = new(Environment.CurrentDirectory);

    /// <summary>
    /// Initializes a new instance of the <see cref="FileManagerPage"/> class and updates the option list.
    /// </summary>
    internal FileManagerPage() => UpdateOptionList();

    /// <summary>
    /// Updates the option list based on the current directory's content.
    /// </summary>
    private void UpdateOptionList()
    {
        Options.Clear();

        // Add drives as options
        Options.Add((null, "==Drives=="));
        var drives = Array.ConvertAll(DriveInfo.GetDrives(), drive => drive.Name);
        foreach (var drive in drives)
        {
            Options.Add((OpenDirectory, drive));
        }
        Options.Add((null, string.Empty));

        // Add the parent directory as an option if it exists and is not a drive
        if (_currentDirectory.Parent != null && !drives.Contains(_currentDirectory.Parent.FullName))
        {
            Options.Add((null, "==Parent directory=="));
            Options.Add((OpenDirectory, _currentDirectory.Parent.FullName));
            Options.Add((null, string.Empty));
        }

        // Add directories as options
        Options.Add((null, "==Directories in the current directory=="));
        foreach (var directory in _currentDirectory.EnumerateDirectories())
        {
            Options.Add((OpenDirectory, directory.FullName));
        }
        Options.Add((null, string.Empty));

        // Add JSON files in the current directory as options
        Options.Add((null, "==JSON Files in the current directory=="));
        foreach (var file in _currentDirectory.EnumerateFiles("*.json"))
        {
            Options.Add((OpenFile, file.FullName));
        }
        Options.Add((null, string.Empty));

        Options.Add((MoveToPreviousPage, "Back to menu"));

        CurrentOption = 0;
    }

    /// <summary>
    /// Opens the selected directory, updating the current directory, and refreshing the option list.
    /// Handles security, argument, and directory not found exceptions, printing appropriate messages.
    /// </summary>
    private void OpenDirectory()
    {
        try
        {
            var tmpDirectory = new DirectoryInfo(Options[CurrentOption].optionDescription);
            if (!tmpDirectory.Exists)
            {
                throw new DirectoryNotFoundException();
            }
            _currentDirectory = tmpDirectory;
        }
        catch (SecurityException)
        {
            ConsoleOutput.PrintIssue("Inpossible to open this directory due to security settings",
                "Check your settings in properties of the directory", true);
        }
        catch (ArgumentException)
        {
            ConsoleOutput.PrintIssue("Something wrong with your directory", "Move to another directory", true);
        }
        catch (DirectoryNotFoundException)
        {
            ConsoleOutput.PrintIssue("Directory not found", "Create it or move to another directory", true);
        }
        UpdateOptionList();
    }

    /// <summary>
    /// Opens the selected JSON file, sets its data, and returns to the previous menu.
    /// </summary>
    private void OpenFile()
    {
        DataConverterLinker.SetJSONData(Options[CurrentOption].optionDescription);
        AppMenu.PopMenuPageFromStack();
    }

    /// <summary>
    /// Moves to the previous menu by popping the current menu page from the stack.
    /// </summary>
    private void MoveToPreviousPage() => AppMenu.PopMenuPageFromStack();
}
