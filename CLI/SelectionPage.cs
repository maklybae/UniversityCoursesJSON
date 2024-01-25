using JSONCourse;

namespace CLI;

/// <summary>
/// Represents a menu page for selecting fields.
/// </summary>
internal class SelectionPage : MenuPage
{
    private List<string> _selectedFields = new List<string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="SelectionPage"/> class.
    /// </summary>
    internal SelectionPage() => Update();

    /// <summary>
    /// Updates the options for the selection page.
    /// </summary>
    internal void Update()
    {
        Options.Clear();
        foreach (var name in CourseHelper.s_orderedAlternativeFieldsNames)
        {
            Options.Add((ChooseField, _selectedFields.Contains(name) ? $"[v] {name}" : $"[ ] {name}"));
        }
        Options.Add((RunSelectingByChosenFields, "Select by chosen fields (all requests will be taken into account)"));
        Options.Add((MoveBack, "Move to previus menu page"));
    }

    /// <summary>
    /// Runs the selection process based on chosen fields.
    /// </summary>
    private void RunSelectingByChosenFields()
    {
        var boxedFieldsToSelect = DataConverterLinker.BoxFieldsByAlternativeNames(_selectedFields);
        var selectedCourses = CourseHelper.SelectCoursesFromListByFields(AppMenu.CoursesData, boxedFieldsToSelect);

        if (selectedCourses.Count == 0)
        {
            ConsoleOutput.PrintIssue("Nothing was found", "Check requested data", true);
            return;
        }

        ConsoleOutput.ShowTableView((CourseHelper.s_orderedAlternativeFieldsNames,
            DataConverterLinker.ConvertToJaggedArray(CourseHelper.s_orderedAlternativeFieldsNames, selectedCourses.ToArray())));
        AppMenu.AddMenuPageToStack(new SavingPage(selectedCourses));
    }

    /// <summary>
    /// Handles the selection/deselection of a field.
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
    /// Moves back to the previous menu page.
    /// </summary>
    private void MoveBack() => AppMenu.PopMenuPageFromStack();
}
