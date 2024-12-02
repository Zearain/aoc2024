using Godot;
using System;

public partial class Day2 : Node2D
{
    private IInputLoader InputLoader;
    private List<int[]> _reports = new List<int[]>();

    [Node("%SolvePart1Button")]
    private Button _solvePart1Button;

    [Node("%SolvePart2Button")]
    private Button _solvePart2Button;

    [Node("%Part1Solution")]
    private Label _part1Solution;

    [Node("%Part2Solution")]
    private Label _part2Solution;

    public override void _Ready()
    {
        InputLoader = new InputLoader();
        _solvePart1Button.Pressed += SolvePart1;
        _solvePart2Button.Pressed += SolvePart2;
    }

    private void SolvePart1()
    {
        this.LoadInput();

        var safeReports = CountSafeReports(_reports);
        _part1Solution.Text = safeReports.ToString();
    }

    private void SolvePart2()
    {
        this.LoadInput();

        var safeReports = CountCompensatedSafeReports(_reports);
        _part2Solution.Text = safeReports.ToString();
    }

    public void LoadInput(int? part = null)
    {
        var input = InputLoader.LoadInput(2, part);
        SetInput(input);
    }

    public void SetInput(string input)
    {
        _reports = ParseReports(input);
    }

    public static List<int[]> ParseReports(string input)
    {
        var reports = new List<int[]>();

        foreach (var line in InputSplitter.Lines(input))
        {
            var report = line.Split(' ').Select(x => int.Parse(x)).ToArray();
            reports.Add(report);
        }

        return reports;
    }

    public static bool IsReportSafe(int[] report)
    {
        var direction = string.Empty;
        for (var i = 1; i < report.Length; i++)
        {
            var currentDir = report[i] > report[i - 1] ? "up" : "down";
            if (direction == string.Empty)
            {
                direction = currentDir;
            }
            else if (direction != currentDir)
            {
                return false;
            }

            var diff = Math.Abs(report[i] - report[i - 1]);
            if (diff < 1 || diff > 3)
            {
                return false;
            }
        }

        return true;
    }

    public static bool IsReportCompensatedSafe(int[] report)
    {
        if (IsReportSafe(report))
        {
            return true;
        }

        for (var i = 0; i < report.Length; i++)
        {
            var modifiedReport = report.Where((_, index) => index != i).ToArray();
            if (IsReportSafe(modifiedReport))
            {
                return true;
            }
        }

        return false;
    }

    public static int CountSafeReports(List<int[]> reports)
    {
        return reports.Count(x => IsReportSafe(x));
    }

    public static int CountCompensatedSafeReports(List<int[]> reports)
    {
        return reports.Count(x => IsReportCompensatedSafe(x));
    }
}
