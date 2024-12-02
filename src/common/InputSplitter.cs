public static class InputSplitter
{
    public static string[] Lines(string input)
    {
        // Hack due to working on Windows and input often being Linux formatted
        var lineSeparator = input.Contains(System.Environment.NewLine) ? System.Environment.NewLine : "\n";
        return input.Split(lineSeparator).Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
    }
}