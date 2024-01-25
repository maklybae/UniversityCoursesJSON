using JSONCourse;
namespace CLI;

/// <summary>
/// Static class responsible for managing the application menu.
/// </summary>
internal static class AppMenu
{
    // Stack to manage navigation between different menu pages
    private static Stack<MenuPage> MenuPagesStack { get; }

    /// <summary>
    /// Static constructor initializes the menu stack and adds initial menu pages.
    /// </summary>
    static AppMenu()
    {
        MenuPagesStack = new Stack<MenuPage>();
        MenuPagesStack.Push(new HomePage());
        MenuPagesStack.Push(new SettingUpPage());
    }

    /// <summary>
    /// Property to store the list of courses loaded from JSON.
    /// </summary>
    internal static List<Course>? CoursesData { get; set; } = null;

    /// <summary>
    /// Property to store the path of the previously loaded JSON file.
    /// </summary>
    internal static string? PreviousFilePath { get; set; } = null;

    /// <summary>
    /// Method to add a new menu page to the stack.
    /// </summary>
    /// <param name="toAdd">Menu page to add to the stack.</param>
    internal static void AddMenuPageToStack(MenuPage toAdd) => MenuPagesStack.Push(toAdd);

    /// <summary>
    /// Method to remove the top menu page from the stack.
    /// </summary>
    internal static void PopMenuPageFromStack() => MenuPagesStack.Pop();

    /// <summary>
    /// Method to display the menu and handle user interaction.
    /// </summary>
    internal static void ShowMenu()
    {
        // Hide the cursor for a cleaner interface
        Console.CursorVisible = false;

        // Print the initial help page with instructions
        ConsoleDialog.PrintHelpPage();

        // Continue showing the menu until the application exits
        while (true)
        {
            // Get the current menu page from the top of the stack
            MenuPage currentPage = MenuPagesStack.Peek();
            currentPage.DrawPage();

            // Handle user input based on arrow keys and Enter key
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.Enter:
                    currentPage.ExecuteCurrentOption();
                    break;
                case ConsoleKey.UpArrow:
                    currentPage.CurrentOption--;
                    break;
                case ConsoleKey.DownArrow:
                    currentPage.CurrentOption++;
                    break;
            }
        }
    }

}
