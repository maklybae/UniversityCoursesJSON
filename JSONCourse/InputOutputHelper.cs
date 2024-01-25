using System.Text;

namespace JSONCourse;

/// <summary>
/// Helper class for input and output operations.
/// </summary>
internal static class InputOutputHelper
{
    /// <summary>
    /// Reads all lines from the standard input stream until a null line is encountered, and returns the concatenated result.
    /// </summary>
    /// <returns>A string containing all lines read from the standard input stream.</returns>
    internal static string? ReadAllInputStream()
    {
        StringBuilder allText = new();

        // Continue reading lines from the standard input until a null line is encountered.
        while (true)
        {
            string? str = Console.ReadLine();
            if (str == null)
                break;
            allText.Append(str);
            allText.Append(Environment.NewLine);
        }
        return allText.ToString();
    }

    /// <summary>
    /// Writes all characters from the specified string to the standard output stream.
    /// </summary>
    /// <param name="output">The string to be written to the standard output stream.</param>
    internal static void WriteAllStream(string? output)
    {
        if (output == null)
            throw new ArgumentNullException(output);

        foreach (var ch in output)
        {
            Console.Write(ch);
        }
    }
}