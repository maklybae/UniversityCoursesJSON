using JSONCourse;

namespace CLI;

/// <summary>
/// Represents a menu page for sorting options.
/// </summary>
internal class SortingPage : MenuPage
{
    private List<string> _selectedFields = new List<string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="SortingPage"/> class.
    /// </summary>
    internal SortingPage() => Update();

    /// <summary>
    /// Updates the options displayed on the sorting page.
    /// </summary>
    internal void Update()
    {
        Options.Clear();
        foreach (var name in CourseHelper.s_orderedAlternativeFieldsNames)
        { 
            Options.Add((ChooseField, _selectedFields.Contains(name) ? $"[{_selectedFields.IndexOf(name) + 1}] {name}" : $"[ ] {name}"));
        }
        Options.Add((RunSortingBySelectedField, "Sort by chosen fields (all requests will be taken into account"));
        Options.Add((MoveBack, "Move to previus menu page"));
    }

    /// <summary>
    /// Handles the selection or deselection of a field for sorting.
    /// </summary>
    internal void ChooseField()
    {
        if (_selectedFields.Contains(Options[CurrentOption].optionDescription[4..]))
            _selectedFields.Remove(Options[CurrentOption].optionDescription[4..]);
        else
            _selectedFields.Add(Options[CurrentOption].optionDescription[4..]);
        Update();
    }

    /// <summary>
    /// Runs sorting by the selected fields and displays the result in a table view.
    /// </summary>
    private void RunSortingBySelectedField()
    {
        var sortingOptions = ConsoleDialog.InputSortingOptionsDictionary(_selectedFields);
        var sortedCourses = CourseHelper.SortCoursesList(AppMenu.CoursesData, _selectedFields, sortingOptions);
        ConsoleOutput.ShowTableView((CourseHelper.s_orderedAlternativeFieldsNames,
            DataConverterLinker.ConvertToJaggedArray(CourseHelper.s_orderedAlternativeFieldsNames, sortedCourses.ToArray())));
        AppMenu.AddMenuPageToStack(new SavingPage(sortedCourses));
    }

    /// <summary>
    /// Moves back to the previous menu page.
    /// </summary>
    private void MoveBack() => AppMenu.PopMenuPageFromStack();
}