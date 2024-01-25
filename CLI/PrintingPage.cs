using JSONCourse;

namespace CLI;

/// <summary>
/// Represents a menu page for printing different rows of data.
/// </summary>
internal class PrintingPage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PrintPage"/> class.
    /// </summary>
    internal PrintingPage()
    {
        Options = new()
        {
                (PrintTop, "Print top rows"),
                (PrintBottom, "Print bottom rows"),
                (PrintAll, "Print all rows"),
                (MoveToPreviousPage, "Back")
        };
    }

    /// <summary>
    /// Prints the top rows of data.
    /// </summary>
    private void PrintTop()
    {
        int count = ConsoleDialog.InputIntegerInRangeUntilCorrect(0, AppMenu.CoursesData!.Count);
        ConsoleOutput.ShowTableView((CourseHelper.s_orderedAlternativeFieldsNames,
            DataConverterLinker.ConvertToJaggedArray(CourseHelper.s_orderedAlternativeFieldsNames, AppMenu.CoursesData!.ToArray()!)), PrintOptions.PrintTop, count);
    }

    /// <summary>
    /// Prints the bottom rows of data.
    /// </summary>
    private void PrintBottom()
    {
        int count = ConsoleDialog.InputIntegerInRangeUntilCorrect(0, AppMenu.CoursesData!.Count);
        ConsoleOutput.ShowTableView((CourseHelper.s_orderedAlternativeFieldsNames,
            DataConverterLinker.ConvertToJaggedArray(CourseHelper.s_orderedAlternativeFieldsNames, AppMenu.CoursesData!.ToArray())), PrintOptions.PrintBottom, count);
    }

    /// <summary>
    /// Prints all rows of data.
    /// </summary>
    private void PrintAll()
    {
        ConsoleOutput.ShowTableView((CourseHelper.s_orderedAlternativeFieldsNames,
            DataConverterLinker.ConvertToJaggedArray(CourseHelper.s_orderedAlternativeFieldsNames, AppMenu.CoursesData!.ToArray()!)));
    }

    /// <summary>
    /// Moves back to the previous menu page.
    /// </summary>
    private void MoveToPreviousPage() => AppMenu.PopMenuPageFromStack();
}
