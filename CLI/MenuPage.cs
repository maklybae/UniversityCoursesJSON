namespace CLI;

/// <summary>
/// Represents an abstract class for a menu page with a list of options and methods to draw and execute them.
/// </summary>
internal abstract class MenuPage
{
    // List of options, each containing an action and a description
    private List<(Action optionAction, string optionDescription)> _options;

    // The index of the currently selected option
    private int _currentOption = 0;

    /// <summary>
    /// Gets or sets the list of options for this menu page.
    /// </summary>
    protected List<(Action optionAction, string optionDescription)> Options
    {
        get { return _options; }
        init { _options = value; }
    }

    /// <summary>
    /// Gets or sets the index of the currently selected option, handling circular navigation.
    /// </summary>  
    internal int CurrentOption
    {
        get { return _currentOption; }
        set
        {
            // Ensure the index stays within the bounds of the options list (circular navigation)
            _currentOption = (value % Options.Count + Options.Count) % Options.Count;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MenuPage"/> class with an empty list of options.
    /// </summary>
    protected MenuPage()
    {
        _options = new List<(Action optionAction, string optionDescription)>();
    }

    /// <summary>
    /// Draws the menu page on the console, highlighting the currently selected option.
    /// </summary>
    internal void DrawPage()
    {
        ConsoleOutput.ClearBuffer();
        for (int i = 0; i < Options.Count; i++)
        {
            if (i == CurrentOption)
                ConsoleOutput.PrintSelected(Options[i].optionDescription);
            else
                Console.WriteLine(Options[i].optionDescription);
        }
    }

    /// <summary>
    /// Executes the action associated with the currently selected option.
    /// </summary>
    internal void ExecuteCurrentOption()
    {
        Options[CurrentOption].optionAction?.Invoke();
    }
}
