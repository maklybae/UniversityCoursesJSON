namespace JSONCourse;

/// <summary>
/// Helper class providing sorting and selection functionalities for courses.
/// </summary>
public static class CourseHelper
{
    /// <summary>
    /// Enumeration representing sorting options for courses.
    /// </summary>
    public enum CourseSortingOptions
    {
        Ascending,
        Descending
    }

    /// <summary>
    /// Enumeration representing different types of JSON fields.
    /// </summary>
    public enum JSONFieldType
    {
        Integer,
        String,
        Boolean,
        ListOfStrings
    }

    /// <summary>
    /// Ordered array of alternative field names for courses.
    /// </summary>
    public static readonly string[] s_orderedAlternativeFieldsNames = new string[]
    {
        "course_id",
        "course_name",
        "instructor",
        "department",
        "enrollment",
        "is_online",
        "students",
    };

    /// <summary>
    /// Dictionary mapping alternative field names to their corresponding JSON field types.
    /// </summary>
    public static readonly Dictionary<string, JSONFieldType> s_typesOfFieldsByAlternativeNames = new()
    {
        {"course_id", JSONFieldType.Integer },
        {"course_name", JSONFieldType.String },
        {"instructor", JSONFieldType.String },
        {"department", JSONFieldType.String },
        {"enrollment", JSONFieldType.Integer },
        {"is_online", JSONFieldType.Boolean },
        {"students", JSONFieldType.ListOfStrings }
    };

    /// <summary>
    /// Performs the primary sorting of courses based on a specified alternative field name and sorting option.
    /// </summary>
    /// <param name="courses">The list of courses to be sorted.</param>
    /// <param name="alternativeName">The alternative field name to sort by.</param>
    /// <param name="sortingOption">The sorting option (ascending or descending).</param>
    /// <returns>An ordered enumerable of courses based on the specified criteria.</returns>
    private static IOrderedEnumerable<Course>? PrimarySorterByAlternativeName(List<Course> courses, string alternativeName, CourseSortingOptions sortingOption)
    {
        IOrderedEnumerable<Course>? orderer = null;
        switch (alternativeName)
        {
            case "course_id":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => course.Id) : courses.OrderByDescending(course => course.Id);
                break;
            case "course_name":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => course.Name) : courses.OrderByDescending(course => course.Name);
                break;
            case "instructor":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => course.Instructor) : courses.OrderByDescending(course => course.Instructor);
                break;
            case "department":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => course.Department) : courses.OrderByDescending(course => course.Department);
                break;
            case "enrollment":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => course.Enrollment) : courses.OrderByDescending(course => course.Enrollment);
                break;
            case "is_online":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => course.IsOnline) : courses.OrderByDescending(course => course.IsOnline);
                break;
            case "students":
                orderer = sortingOption == CourseSortingOptions.Ascending ? courses.OrderBy(course => (course.Students?.Count ?? 0)) : courses.OrderByDescending(course => (course.Students?.Count ?? 0));
                break;
        }
        return orderer;
    }

    /// <summary>
    /// Performs the secondary sorting of courses based on a specified alternative field name and sorting option.
    /// </summary>
    /// <param name="orderer">The previously ordered enumerable of courses.</param>
    /// <param name="alternativeName">The alternative field name to sort by.</param>
    /// <param name="sortingOption">The sorting option (ascending or descending).</param>
    /// <returns>An ordered enumerable of courses further sorted based on the specified criteria.</returns>
    private static IOrderedEnumerable<Course>? SecondarySorterByAlternativeName(IOrderedEnumerable<Course>? orderer, string alternativeName, CourseSortingOptions sortingOption)
    {
        switch (alternativeName)
        {
            case "course_id":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => course.Id) : orderer?.ThenByDescending(course => course.Id);
                break;
            case "course_name":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => course.Name) : orderer?.ThenByDescending(course => course.Name);
                break;
            case "instructor":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => course.Instructor) : orderer?.ThenByDescending(course => course.Instructor);
                break;
            case "department":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => course.Department) : orderer?.ThenByDescending(course => course.Department);
                break;
            case "enrollment":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => course.Enrollment) : orderer?.ThenByDescending(course => course.Enrollment);
                break;
            case "is_online":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => course.IsOnline) : orderer?.ThenByDescending(course => course.IsOnline);
                break;
            case "students":
                orderer = sortingOption == CourseSortingOptions.Ascending ? orderer?.ThenBy(course => (course.Students?.Count ?? 0)) : orderer?.ThenByDescending(course => (course.Students?.Count ?? 0));
                break;
        }
        return orderer;
    }

    /// <summary>
    /// Sorts a list of courses based on specified keys and sorting options.
    /// </summary>
    /// <param name="courses">The list of courses to sort.</param>
    /// <param name="keys">The keys to sort by.</param>
    /// <param name="sortingOptions">The sorting options for each key.</param>
    /// <returns>The sorted list of courses.</returns>
    public static List<Course> SortCoursesList(List<Course>? courses,
        List<string>? keys, Dictionary<string, CourseSortingOptions>? sortingOptions)
    {
        if (courses == null)
            throw new ArgumentNullException(nameof(courses));
        if (keys == null)
            throw new ArgumentNullException(nameof(keys));
        if (sortingOptions == null)
            throw new ArgumentNullException(nameof(sortingOptions));
        if (keys.Count < 1)
            throw new ArgumentException("Requested prarmeters to sort should contain at least one argument", nameof(keys));

        IOrderedEnumerable<Course>? orderer = PrimarySorterByAlternativeName(courses, keys[0], sortingOptions[keys[0]]);
        
        for (int i = 1; i < keys.Count; i++)
        {
            orderer = SecondarySorterByAlternativeName(orderer, keys[0], sortingOptions[keys[0]]);
        }

        return orderer?.ToList() ?? courses;
    }

    /// <summary>
    /// Selects courses from a list based on specified fields and values.
    /// </summary>
    /// <param name="courses">The list of courses to select from.</param>
    /// <param name="requestedParams">The fields and values to select by.</param>
    /// <returns>The list of selected courses.</returns>
    public static List<Course> SelectCoursesFromListByFields(List<Course>? courses, Dictionary<string, object>? requestedParams)
    {
        if (courses == null)
            throw new ArgumentNullException(nameof(courses));
        if (requestedParams == null)
            throw new ArgumentNullException(nameof(requestedParams));

        List<Predicate<Course>> predicates = GenreateSelectingPredicates(requestedParams);
        List<Course> selected = courses.FindAll(course => predicates.ConvertAll(predicate => predicate(course)).All(verdict => verdict));
        return selected;
    }

    /// <summary>
    /// Generates a list of predicates for selecting courses based on the specified field-value pairs.
    /// </summary>
    /// <param name="requestedParams">A dictionary containing field-value pairs for selection.</param>
    /// <returns>A list of predicates, each representing a condition for selecting courses.</returns>
    private static List<Predicate<Course>> GenreateSelectingPredicates(Dictionary<string, object> requestedParams)
    {
        List<Predicate<Course>> predicates = new();
        
        foreach (var kvp in requestedParams)
        {
            switch (kvp.Key)
            {
                 case "course_id":
                    predicates.Add(course => course.Id == (int)kvp.Value);
                    continue;
                case "course_name":
                    predicates.Add(course => course.Name == (string)kvp.Value);
                    continue;
                case "instructor":
                    predicates.Add(course => course.Instructor == (string)kvp.Value);
                    continue;
                case "department":
                    predicates.Add(course => course.Department == (string)kvp.Value);
                    continue;
                case "enrollment":
                    predicates.Add(course => course.Enrollment == (int)kvp.Value);
                    continue;
                case "is_online":
                    predicates.Add(course => course.IsOnline == (bool)kvp.Value);
                    continue;
                case "students":
                    predicates.Add(course => course.Students?.Contains((string)kvp.Value) ?? false);
                    continue;
            }
        }
        return predicates;
    }

}
