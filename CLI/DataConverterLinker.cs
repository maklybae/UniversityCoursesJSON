using JSONCourse;

namespace CLI;

/// <summary>
/// Static class responsible for linking and converting data between the application components.
/// </summary>
internal static class DataConverterLinker
{
    /// <summary>
    /// Sets JSON data for courses and prints a success message.
    /// </summary>
    /// <param name="courses">The list of courses to set.</param>
    internal static void SetJSONData(List<Course> courses)
    {
        AppMenu.CoursesData = courses;
        ConsoleOutput.PrintSuccess(true);
    }

    /// <summary>
    /// Sets JSON data from a file or a reference file, handling exceptions and printing messages accordingly.
    /// </summary>
    /// <param name="path">Optional path to the file, null for a reference file.</param>
    internal static void SetJSONData(string? path = null)
    {
        List<Course> res;
        try
        {
            if (path == null)
            {
                res = JSONParser.ReadJSON();
            }
            else
            {
                res = ReadJSONFileToCurses(path);
            }
            AppMenu.CoursesData = res;
            AppMenu.PreviousFilePath = path;
            ConsoleOutput.PrintSuccess(true);
        }
        catch (JSONParserException)
        {
            ConsoleOutput.PrintIssue("JSON data not in the correct format", "Open reference file from edu.hse.ru", true);
        }
        catch (ArgumentException)
        {
            ConsoleOutput.PrintIssue("Path to file is incorrect", "Check it on the local PC", true);
        }
        catch (IOException)
        {
            ConsoleOutput.PrintIssue("Unable to open this file", "Check it on the local PC", true);
        }
        catch (UnauthorizedAccessException)
        {
            ConsoleOutput.PrintIssue("Unable to open securied files", "Check the properties of the file", true);
        }
    }

    /// <summary>
    /// Reads JSON data from a specified file path into a list of courses.
    /// </summary>
    /// <param name="path">The path to the JSON file.</param>
    /// <returns>The list of courses read from the JSON file.</returns>
    internal static List<Course> ReadJSONFileToCurses(string path)
    {
        List<Course> res;
        StreamReader? streamReader = null;
        try
        {
            streamReader = new StreamReader(path);
            Console.SetIn(streamReader);
            res = JSONParser.ReadJSON();
        }
        finally
        {
            streamReader?.Dispose();
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));

        }
        return res;
    }

    /// <summary>
    /// Outputs processed data to the console or a specified file path, handling exceptions and printing messages accordingly.
    /// </summary>
    /// <param name="courses">The list of courses to output.</param>
    /// <param name="path">Optional path to the file, null for console output.</param>
    internal static void OutputProcessedData(List<Course> courses, string? path = null)
    {
        StreamWriter? streamWriter = null;
        TextWriter? prevOut = Console.Out;
        if (path != null)
        {
            try
            {
                if (!path.ToLower().EndsWith(".json"))
                {
                    throw new IOException();
                }
                streamWriter = new StreamWriter(path);
                Console.SetOut(streamWriter);
            }
            catch (IOException)
            {
                ConsoleOutput.PrintIssue("Incorrect name of file or path", "Repeat an attempt", true);
                return;
            }
            catch (UnauthorizedAccessException)
            {
                ConsoleOutput.PrintIssue("Unable to open securied files", "Check the properties of the file", true);
                return;
            }
            catch (ArgumentException)
            {
                ConsoleOutput.PrintIssue("Something went wrong with your file", "Repeat an attempt", true);
                return;
            }
        }
        else
        {
            ConsoleOutput.ClearBuffer();
        }
        JSONParser.WriteJSON(courses);
        if (path != null)
        {
            streamWriter?.Dispose();
            Console.SetOut(prevOut);
        }
        ConsoleOutput.PrintSuccess(true);
    }

    /// <summary>
    /// Converts courses data to a jagged array based on specified headings.
    /// </summary>
    /// <param name="headings">The array of headings to use for conversion.</param>
    /// <param name="institutes">The array of courses data to convert.</param>
    /// <returns>The jagged array representing the converted data.</returns>
    internal static string[][] ConvertToJaggedArray(string[] headings, Course[] institutes)
    {
        // Create a jagged array to store the converted data.
        string[][] res = new string[institutes.Length][];

        // Iterate through each Institute object and convert its data into the jagged array.
        for (int i = 0; i < institutes.Length; i++)
        {
            // Create a new array to store the Institute's data.
            res[i] = new string[headings.Length];

            // Populate the array with data from the Institute object.
            for (int j = 0; j < headings.Length; j++)
            {
                // Use the specified headings to retrieve data from the Institute object.
                res[i][j] = institutes[i][headings[j]] ?? "Null";
            }
        }

        return res;
    }

    /// <summary>
    /// Boxes fields by alternative names into a dictionary with user input for each field.
    /// </summary>
    /// <param name="alternativeNames">List of alternative names for the fields.</param>
    /// <returns>Dictionary containing boxed fields by alternative names.</returns>
    internal static Dictionary<string, object> BoxFieldsByAlternativeNames(List<string> alternativeNames)
    {
        Dictionary<string, object> res = new();
        foreach (var field in alternativeNames)
        {
            switch (CourseHelper.s_typesOfFieldsByAlternativeNames[field])
            {
                case CourseHelper.JSONFieldType.Integer:
                    res.Add(field, ConsoleDialog.InputIntegerField(field));
                    continue;
                case CourseHelper.JSONFieldType.String:
                    res.Add(field, ConsoleDialog.InputStringField(field));
                    continue;
                case CourseHelper.JSONFieldType.Boolean:
                    res.Add(field, ConsoleDialog.InputBooleanField(field));
                    continue;
                case CourseHelper.JSONFieldType.ListOfStrings:
                    res.Add(field, ConsoleDialog.InputStringField(field));
                    continue;
            }
        }
        return res;
    }
}
