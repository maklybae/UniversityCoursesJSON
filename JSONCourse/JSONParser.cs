using System.Text.RegularExpressions;

namespace JSONCourse;

/// <summary>
/// Helper class for parsing JSON data and converting it to/from a list of Course objects.
/// </summary>
public static class JSONParser
{
    // Patterns
    private const string MatchAllCoursesPattern = @"\[\s*
(?<course>\s*{\s*
""course_id"":\s*\d+,\s*
""course_name"":\s*""[^""]*"",\s*
""instructor"":\s*""[^""]*"",\s*
""department"":\s*""[^""]*"",\s*
""enrollment"":\s*\d+,\s*
""is_online"":\s*(?>true|false),\s*
""students"":\s*\[(?>\s*""[^""]*"",?)*\s*\]\s*
},?)+\s*
\]";
    private const string MatchCourseDataPattern = @"""course_id"":\s*(?<course_id>\d+),\s*
""course_name"":\s*""(?<course_name>[^""]*)"",\s*
""instructor"":\s*""(?<instructor>[^""]*)"",\s*
""department"":\s*""(?<department>[^""]*)"",\s*
""enrollment"":\s*(?<enrollment>\d+),\s*
""is_online"":\s*(?<is_online>true|false),\s*
""students"":\s*\[(?>\s*""(?<students>[^""]*)"",?)*\s*\]\s*";
    private static readonly string JSONCourseObjectPattern = @"  {{
    ""course_id"": {0},
    ""course_name"": ""{1}"",
    ""instructor"": ""{2}"",
    ""department"": ""{3}"",
    ""enrollment"": {4},
    ""is_online"": {5},
    ""students"": [
{6}
    ]
  }}";

    // Using for output json data
    private const int JSONStandartIndent = 2;

    /// <summary>
    /// Reads JSON data from the standard input stream and converts it to a list of Course objects.
    /// </summary>
    /// <returns>A list of Course objects parsed from the JSON data.</returns>
    public static List<Course> ReadJSON()
    {
        string? jsonData = InputOutputHelper.ReadAllInputStream();
        if (jsonData == null)
        {
            throw new ArgumentNullException(nameof(jsonData));
        }
        var match = Regex.Match(jsonData, MatchAllCoursesPattern, RegexOptions.IgnorePatternWhitespace);
        if (!match.Success)
        {
            throw new JSONParserException("JSON data not in the correct format");
        }

        var jsonCourseObjects = match.Groups["course"].Captures.ToList().ConvertAll(value => value.ToString());
        return ConvertListOfJSONObjectsToListOfCourses(jsonCourseObjects);   
    }

    /// <summary>
    /// Converts a JSON object string to a Course object.
    /// </summary>
    /// <param name="jsonData">The JSON object string to convert.</param>
    /// <returns>A Course object parsed from the JSON data.</returns>
    private static Course ConvertJSONObejectToCourse(string jsonData)
    {
        var matchGroups = Regex.Match(jsonData, MatchCourseDataPattern, RegexOptions.IgnorePatternWhitespace).Groups;
        return new Course(
            int.Parse(matchGroups["course_id"].Value),
            matchGroups["course_name"].Value,
            matchGroups["instructor"].Value,
            matchGroups["department"].Value,
            int.Parse(matchGroups["enrollment"].Value),
            bool.Parse(matchGroups["is_online"].Value),
            matchGroups["students"].Captures.ToList().ConvertAll(value => value.ToString())
            );
    }

    /// <summary>
    /// Converts a list of JSON object strings to a list of Course objects.
    /// </summary>
    /// <param name="jsonObjects">The list of JSON object strings to convert.</param>
    /// <returns>A list of Course objects parsed from the JSON data.</returns>
    private static List<Course> ConvertListOfJSONObjectsToListOfCourses(List<string> jsonObjects) =>
        jsonObjects.ConvertAll(value => ConvertJSONObejectToCourse(value));

    /// <summary>
    /// Writes a list of Course objects to the standard output stream in JSON format.
    /// </summary>
    /// <param name="dataToWrite">The list of Course objects to write.</param>
    public static void WriteJSON(List<Course>? dataToWrite)
    {
        if (dataToWrite == null)
        {
            throw new ArgumentNullException(nameof(dataToWrite));
        }
        InputOutputHelper.WriteAllStream(ConvertListOfCoursesToJSONString(dataToWrite));
    }

    /// <summary>
    /// Converts a list of Course objects to a JSON-formatted string array and joins them into a single string.
    /// </summary>
    /// <param name="courses">The list of Course objects to convert.</param>
    /// <returns>A JSON-formatted string representing the list of Course objects.</returns>
    private static string ConvertListOfCoursesToJSONString(List<Course> courses)
    {
        var jsonObjects = courses.ConvertAll(value => ConvertCourseToJSONObject(value));
        string joinedJSONObjects = string.Join($",{Environment.NewLine}", jsonObjects);
        return $"[{Environment.NewLine}{joinedJSONObjects}{Environment.NewLine}]";
    }

    /// <summary>
    /// Converts a Course object to a JSON-formatted string.
    /// </summary>
    /// <param name="course">The Course object to convert.</param>
    /// <returns>A JSON-formatted string representing the Course object.</returns>
    private static string ConvertCourseToJSONObject(Course? course)
    {
        if (course == null)
        {
            throw new ArgumentNullException(nameof(course));
        }
        return string.Format(
            JSONCourseObjectPattern,
            course.Id,
            course.Name,
            course.Instructor,
            course.Department,
            course.Enrollment,
            course.IsOnline.ToString()?.ToLower(),
            string.Join($",{Environment.NewLine}",
            course.Students?.ConvertAll(value => $"{new string(' ', JSONStandartIndent * 3)}\"{value}\"") ?? new List<string>())
            );
    }
}
