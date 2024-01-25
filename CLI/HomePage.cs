namespace CLI;

/// <summary>
/// Represents the home page menu with various options for data manipulation and program settings.
/// </summary>
internal class HomePage : MenuPage
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HomePage"/> class and sets up the initial options.
    /// </summary>
    internal HomePage()
    {

        Options = new()
        {
            (MoveToDataOutputMenu, "Data Output"),
            (MoveToSelectionMenu, "Selecting"),
            (MoveToSortingMenu, "Sorting"),
            (MoveToSettinUpMenu, "Set up configuration"),
            (MoveToHelp, "Help"),
            (Exit, "Exit")
        };
    }

    /// <summary>
    /// Moves to the data output menu by adding it to the menu stack.
    /// </summary>
    private void MoveToDataOutputMenu() => AppMenu.AddMenuPageToStack(new PrintingPage());

    /// <summary>
    /// Moves to the selection menu by adding it to the menu stack.
    /// </summary>
    private void MoveToSelectionMenu() => AppMenu.AddMenuPageToStack(new SelectionPage());

    /// <summary>
    /// Moves to the sorting menu by adding it to the menu stack.
    /// </summary>
    private void MoveToSortingMenu() => AppMenu.AddMenuPageToStack(new SortingPage());

    /// <summary>
    /// Moves to the configuration setup menu by adding it to the menu stack.
    /// </summary>
    private void MoveToSettinUpMenu() => AppMenu.AddMenuPageToStack(new SettingUpPage());

    /// <summary>
    /// Prints the help page to the console.
    /// </summary>
    private void MoveToHelp() => ConsoleDialog.PrintHelpPage();

    /// <summary>
    /// Exits the program by terminating the environment.
    /// </summary>
    private void Exit() => Environment.Exit(0);
}
