using Godot;

public sealed class InputLoader : IInputLoader
{
    public string LoadInput(int day, int? part = null)
    {
        const string RES_PREFIX = "res://assets/inputs/day/";
        const string FILE_SUFFIX = "input.txt";
        var path = $"{RES_PREFIX}{day}/{(part.HasValue ? $"{part}/" : string.Empty)}{FILE_SUFFIX}";
        return Godot.FileAccess.GetFileAsString(path).Trim();
    }
}