using JSONCourse;

namespace CLI;

/// <summary>
/// Static class responsible for managing console output and displaying visual elements.
/// </summary>
internal static class ConsoleOutput
{
    // ASCII art titles for success and welcome
    private static readonly string s_asciiSuccess = $"{Environment.NewLine}   " +
        $".-'''-.   ___    _     _______       _______      .-''-.     .-'''-" +
        $".    .-'''-.  {Environment.NewLine}  / _     \\.'   |  | |   /   __" +
        $"  \\     /   __  \\   .'_ _   \\   / _     \\  / _     \\ " +
        $"{Environment.NewLine} (`' )/`--'|   .'  | |  | ,_/  \\__)   | ,_/  " +
        $"\\__) / ( ` )   ' (`' )/`--' (`' )/`--' {Environment.NewLine}(_ o _" +
        $").   .'  '_  | |,-./  )       ,-./  )      . (_ o _)  |(_ o _).   (" +
        $"_ o _).    {Environment.NewLine} (_,_). '. '   ( \\.-.|\\  '_ '`)  " +
        $"   \\  '_ '`)    |  (_,_)___| (_,_). '.  (_,_). '.  " +
        $"{Environment.NewLine}.---.  \\  :' (`. _` /| > (_)  )  __  > (_)  )" +
        $"  __'  \\   .---..---.  \\  :.---.  \\  : {Environment.NewLine}\\  " +
        $"  `-'  || (_ (_) _)(  .  .-'_/  )(  .  .-'_/  )\\  `-'    /\\    `-" +
        $"'  |\\    `-'  | {Environment.NewLine} \\       /  \\ /  . \\ / `-'" +
        $"`-'     /  `-'`-'     /  \\       /  \\       /  \\       /  " +
        $"{Environment.NewLine}  `-...-'    ``-'`-''    `._____.'     `._____" +
        $".'    `'-..-'    `-...-'    `-...-'   {Environment.NewLine}        " +
        $"                                                                   " +
        $"         {Environment.NewLine}";

    // Fun title Welcome.
    private static readonly string s_asciiWelcome = $".--.      .--.    .-''-.  " +
        $"  .---.        _______      ,-----.    ,---.    ,---.    .-''-.   " +
        $"{Environment.NewLine}|  |_     |  |  .'_ _   \\   | ,_|       /   __ " +
        $" \\   .'  .-,  '.  |    \\  /    |  .'_ _   \\  {Environment.NewLine}|" +
        $" _( )_   |  | / ( ` )   ',-./  )      | ,_/  \\__) / ,-.|  \\ _ \\ | " +
        $" ,  \\/  ,  | / ( ` )   ' {Environment.NewLine}|(_ o _)  |  |. (_ o _" +
        $")  |\\  '_ '`)  ,-./  )      ;  \\  '_ /  | :|  |\\_   /|  |. (_ o _)" +
        $"  | {Environment.NewLine}| (_,_) \\ |  ||  (_,_)___| > (_)  )  \\  '_ " +
        $"'`)    |  _`,/ \\ _/  ||  _( )_/ |  ||  (_,_)___| {Environment.NewLine}" +
        $"|  |/    \\|  |'  \\   .---.(  .  .-'   > (_)  )  __: (  '\\_/ \\   ;| " +
        $"(_ o _) |  |'  \\   .---. {Environment.NewLine}|  '  /\\  `  | \\  `-' " +
        $"   / `-'`-'|___(  .  .-'_/  )\\ `\"/  \\  ) / |  (_,_)  |  | \\  `-'   " +
        $" / {Environment.NewLine}|    /  \\    |  \\       /   |        \\`-'`-' " +
        $"    /  '. \\_/``\".'  |  |      |  |  \\       /  {Environment.NewLine}`-" +
        $"--'    `---`   `'-..-'    `--------`  `._____.'     '-----'    '--'      " +
        $"'--'   `'-..-'   {Environment.NewLine}                                   " +
        $"                                                         ";

    /// <summary>
    /// Prints ASCII art or a basic title for success, depending on the console window width.
    /// </summary>
    /// <param name="isStopped">Indicates whether to wait for a key press if true.</param>
    internal static void PrintSuccess(bool isStopped = false)
    {
        Console.ForegroundColor = ConsoleColor.Green;

        // Limit width to fit to console.
        if (Console.WindowWidth < 90)
        {
            Console.WriteLine("Success!");
        }
        else
        {
            Console.WriteLine(s_asciiSuccess);
        }
        Console.ForegroundColor = ConsoleColor.White;

        // Wait for user input if specified
        if (isStopped)
            Console.ReadKey(true);
    }

    /// <summary>
    /// Prints an ASCII art or a basic title for welcome, depending on the console window width.
    /// </summary>
    internal static void PrintWelcome()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        // Limit width to fit to console.
        if (Console.WindowWidth < 95)
        {
            Console.WriteLine("Welcome!");
        }
        else
        {
            Console.WriteLine(s_asciiWelcome);
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Clears the console buffer.
    /// </summary>
    internal static void ClearBuffer()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");
        Console.Clear();
    }

    /// <summary>
    /// Prints a selected text with a highlighted background.
    /// </summary>
    /// <param name="text">The text to be printed.</param>
    internal static void PrintSelected(string text)
    {
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    /// <summary>
    /// Prints an issue message with a fix recommendation.
    /// </summary>
    /// <param name="issue">The issue message.</param>
    /// <param name="fixRecommendation">The recommendation to fix the issue.</param>
    /// <param name="isStopped">Indicates whether to wait for a key press if true.</param>
    internal static void PrintIssue(string issue, string fixRecommendation, bool isStopped = false)
    {
        ClearBuffer();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("Issue: ");
        Console.WriteLine(issue);

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(fixRecommendation);

        Console.ForegroundColor = ConsoleColor.White;
        if (isStopped)
            Console.ReadKey(true);
    }

    /// <summary>
    /// Shows a table view with auto-width console output.
    /// </summary>
    /// <param name="tableData">Pair of headings and data for the table.</param>
    /// <param name="columnToExclude">Index of the column to exclude from printing if the value is null.</param>
    internal static void PrintTableView((string[] headings, string[][] data) tableData, int currentWidth, int first, int last)
    {
        int columnWidth = (currentWidth - last - first + 1) / (last - first);
        ClearBuffer();

        if (columnWidth <= 3)
        {
            PrintIssue("Unable to print with this size", "Zoom out the console window");
            return;
        }

        // Print headings.
        Console.WriteLine(string.Join('|', Array.ConvertAll(tableData.headings[first..last],
            s => s.Length <= columnWidth ? s.PadRight(columnWidth) : s[..(columnWidth - 3)] + "...")));
        Console.WriteLine(new string('—', currentWidth));

        // Print data.
        foreach (var dataRow in tableData.data)
        {
            Console.WriteLine(string.Join('|', Array.ConvertAll(dataRow[first..last],
                s => s.Length <= columnWidth ? s.PadRight(columnWidth) : s[..(columnWidth - 3)] + "...")));
        }

        // Print statistics.
        Console.WriteLine(new string('—', currentWidth));
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Total data rows: {tableData.data.Length}");
        Console.WriteLine($"Use Left-Right arrows to check other columns. Press any other button to move from this page.");
        Console.WriteLine($"To view more characters in a column, zoom in or zoom out the console window or chage the console font size");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write("Press any button...");
    }

    /// <summary>
    /// Displays a table view with options for partial printing and navigation.
    /// </summary>
    /// <param name="tableData">Pair of headings and data for the table.</param>
    /// <param name="option">Printing options for the table.</param>
    /// <param name="count">Number of rows to print for partial printing options.</param>
    internal static void ShowTableView((string[] headings, string[][] data) tableData, PrintOptions option = PrintOptions.All, int count = -1)
    {
        (string[] headings, string[][] data) tableDataToPrint;
        tableDataToPrint.headings = tableData.headings;
        tableDataToPrint.data = option switch
        {
            PrintOptions.PrintTop => tableData.data[..count],
            PrintOptions.PrintBottom => tableData.data[^count..],
            _ => tableData.data,
        };

        // Estimate
        int currentFirstColumn = 0;
        int countColumns = CourseHelper.s_orderedAlternativeFieldsNames.Length;
        int currentLastColumn = Math.Min(countColumns, Constants.MaxColumnsInConsole);

        int prevWidth = Console.WindowWidth;
        PrintTableView(tableDataToPrint, prevWidth, currentFirstColumn, currentLastColumn);

        // Continuously check for changes in the console window width to update the displayed table.
        while (true)
        {
            if (!Console.KeyAvailable)
            {
                if (prevWidth != Console.WindowWidth)
                {
                    prevWidth = Console.WindowWidth;
                    PrintTableView(tableDataToPrint, prevWidth, currentFirstColumn, currentLastColumn);
                }
            }
            else
            {
                ConsoleKey ck = Console.ReadKey(true).Key;
                if (ck == ConsoleKey.LeftArrow)
                {
                    if (currentFirstColumn - 1 >= 0)
                    {
                        currentFirstColumn--;
                        currentLastColumn--;
                        PrintTableView(tableDataToPrint, prevWidth, currentFirstColumn, currentLastColumn);
                    }
                }
                else if (ck == ConsoleKey.RightArrow)
                {
                    if (currentLastColumn + 1 <= countColumns)
                    {
                        currentFirstColumn++;
                        currentLastColumn++;
                        PrintTableView(tableDataToPrint, prevWidth, currentFirstColumn, currentLastColumn);
                    }
                }
                else
                {
                    break;
                }
            }
            Thread.Sleep(5);
        }
    }
}

