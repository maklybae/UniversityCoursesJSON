namespace JSONCourse;

/// <summary>
/// Represents a course entity.
/// </summary>
public class Course
{
    public int? Id { private init; get; }
    public string? Name { private init; get; }
    public string? Instructor { private init; get; }
    public string? Department { private init; get; }
    public int? Enrollment { private init; get; }
    public bool? IsOnline { private init; get; }
    public List<string>? Students { private init; get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Course"/> class.
    /// </summary>
    /// <param name="id">The ID of the course.</param>
    /// <param name="name">The name of the course.</param>
    /// <param name="instructor">The instructor of the course.</param>
    /// <param name="department">The department of the course.</param>
    /// <param name="enrollment">The enrollment count for the course.</param>
    /// <param name="isOnline">A value indicating whether the course is online.</param>
    /// <param name="students">The list of students for the course.</param>
    public Course(
        int? id,
        string? name,
        string? instructor,
        string? department,
        int? enrollment,
        bool? isOnline,
        List<string>? students)
    {
        Id = id;
        Name = name;
        Instructor = instructor;
        Department = department;
        Enrollment = enrollment;
        IsOnline = isOnline;
        Students = students;
    }

    /// <summary>
    /// Gets the value of a specific property by its alternative name.
    /// </summary>
    /// <param name="index">The alternative name of the property.</param>
    /// <returns>The value of the specified property.</returns>
    public string? this[string? index]
    {
        get
        {
            if (index == null) return null;
            return index switch
            {
                "course_id" => Id.ToString(),
                "course_name" => Name,
                "instructor" => Instructor,
                "department" => Department,
                "enrollment" => Enrollment.ToString(),
                "is_online" => IsOnline.ToString(),
                "students" => string.Join(", ", Students ?? new List<string>()),
                _ => null
            };
        }
    }
}