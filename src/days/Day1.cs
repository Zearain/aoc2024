using Godot;
using System;

public partial class Day1 : Node2D
{
    private string _input = string.Empty;
    private string[] _lines = Array.Empty<string>();
    private IInputLoader InputLoader;

    [Node("%SolvePart1Button")]
    private Button _solvePart1Button;

    [Node("%SolvePart2Button")]
    private Button _solvePart2Button;

    [Node("%Part1Solution")]
    private Label _part1Solution;

    [Node("%Part2Solution")]
    private Label _part2Solution;

    public string Input => _input;
    public string[] Lines => _lines;

    public override void _Ready()
    {
        InputLoader = new InputLoader();

        _solvePart1Button.Pressed += SolvePart1;
        _solvePart2Button.Pressed += SolvePart2;
    }


    private void SolvePart1()
    {
        this.LoadInput();

        var sortedLists = SortCoordinateLists(ParseCoordinateLists(Lines));
        var totalDistance = TotalDistance(sortedLists.Left, sortedLists.Right);

        _part1Solution.Text = totalDistance.ToString();
    }

    private void SolvePart2()
    {
        this.LoadInput();

        var (left, right) = ParseCoordinateLists(Lines);
        var score = left.Sum(x => Day1.SimilarityScore(x, right));

        _part2Solution.Text = score.ToString();
    }


    public void LoadInput(int? part = null)
    {
        SetInput(InputLoader.LoadInput(1, part));
    }

    public void SetInput(string input)
    {
        _input = input;
        _lines = InputSplitter.Lines(input);
    }

    public static (int[] Left, int[] Right) ParseCoordinateLists(string[] lines)
    {
        var left = new int[lines.Length];
        var right = new int[lines.Length];

        for (var i = 0; i < lines.Length; i++)
        {
            var parts = lines[i].Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToArray();
            left[i] = int.Parse(parts[0]);
            right[i] = int.Parse(parts[1]);
        }

        return (left, right);
    }

    public static (int[] Left, int[] Right) SortCoordinateLists((int[] Left, int[] Right) lists)
    {
        var left = lists.Left;
        var right = lists.Right;

        Array.Sort(left);
        Array.Sort(right);

        return (left, right);
    }

    public static int Distance(int x, int y)
    {
        return Math.Abs(y - x);
    }

    public static long TotalDistance(int[] sortedLeft, int[] sortedRight)
    {
        var distance = 0L;

        for (var i = 0; i < sortedLeft.Length; i++)
        {
            distance += Distance(sortedLeft[i], sortedRight[i]);
        }

        return distance;
    }

    public static long SimilarityScore(int x, int[] right)
    {
        return x * right.Count(i => i == x);
    }
}
