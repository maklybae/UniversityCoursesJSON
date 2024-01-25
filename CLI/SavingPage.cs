using JSONCourse;

namespace CLI;

/// <summary>
/// Represents a menu page for saving processed data.
/// </summary>
internal class SavingPage : MenuPage
{
    List<Course> _coursesToSave;

    /// <summary>
    /// Initializes a new instance of the <see cref="SavingPage"/> class.
    /// </summary>
    /// <param name="coursesToSave">The list of courses to save.</param>
    internal SavingPage(List<Course> coursesToSave)
    {
        _coursesToSave = coursesToSave;
        Options.Add((SaveToDesktop, "Save to the desktop directory"));
        Options.Add((SaveToCurrentDirectory, $"Save to the current directory: {Environment.CurrentDirectory}"));
        Options.Add((SaveToRecentlyOpenedFile, $"Save to the recently opened file: {AppMenu.PreviousFilePath ?? "Unable"}"));
        Options.Add((SaveToCustomPath, $"Save to any other folder by full path to the file"));
        Options.Add((PrintJSONFormattedData, $"Print JSON Formatted data in console"));
        Options.Add((ChangeCurrentData, "Сhange the current data to the processed one"));
        Options.Add((MoveBack, "Move to previus menu page"));
    }

    /// <summary>
    /// Saves processed data to the desktop directory.
    /// </summary>
    private void SaveToDesktop()
    {
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), ConsoleInput.InputFilename());
        if (!File.Exists(path) || ConsoleDialog.InputOverwriteFile())
        {
            DataConverterLinker.OutputProcessedData(_coursesToSave, path);
        }
    }

    /// <summary>
    /// Saves processed data to the current directory.
    /// </summary>
    private void SaveToCurrentDirectory()
    {
        string path = Path.Combine(Environment.CurrentDirectory, ConsoleInput.InputFilename());
        if (!File.Exists(path) || ConsoleDialog.InputOverwriteFile())
        {
            DataConverterLinker.OutputProcessedData(_coursesToSave, path);
        }
    }

    /// <summary>
    /// Saves processed data to the recently opened file.
    /// </summary>
    private void SaveToRecentlyOpenedFile()
    {
        if (AppMenu.PreviousFilePath == null)
        {
            ConsoleOutput.PrintIssue("Unable option due to lack of data about previous file", "Read JSON data with file opening option and this path will be sasved as previous.");
        }
        else
        {
            if (!File.Exists(AppMenu.PreviousFilePath) || ConsoleDialog.InputOverwriteFile())
            {
                DataConverterLinker.OutputProcessedData(_coursesToSave, AppMenu.PreviousFilePath);
            }
        }
    }

    /// <summary>
    /// Saves processed data to a custom path provided by the user.
    /// </summary>
    private void SaveToCustomPath()
    {
        string path = ConsoleInput.InputFullPath();
        if (!File.Exists(path) || ConsoleDialog.InputOverwriteFile())
        {
            DataConverterLinker.OutputProcessedData(_coursesToSave, path);
        }
    }

    /// <summary>
    /// Prints the JSON formatted data in the console.
    /// </summary>
    private void PrintJSONFormattedData() => DataConverterLinker.OutputProcessedData(_coursesToSave);

    /// <summary>
    /// Changes the current data to the processed one.
    /// </summary>
    private void ChangeCurrentData() => DataConverterLinker.SetJSONData(_coursesToSave);

    /// <summary>
    /// Moves back to the previous menu page.
    /// </summary>
    private void MoveBack() => AppMenu.PopMenuPageFromStack();

}