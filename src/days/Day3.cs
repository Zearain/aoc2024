using Godot;
using System;
using System.Text.RegularExpressions;

public partial class Day3 : Node2D
{
    private string _input = string.Empty;
    private IInputLoader InputLoader;

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
        this.LoadInputIfEmpty();

        var instructions = ParseUncorruptedInstructions(_input);
        var sum = SumInstructions(instructions);
        _part1Solution.Text = sum.ToString();
    }

    private void SolvePart2()
    {
        this.LoadInputIfEmpty();
        var instructions = FindAllInstructions(_input);
        var sum = SumInstructions(instructions);
        _part2Solution.Text = sum.ToString();
    }

    private void LoadInputIfEmpty()
    {
        if (string.IsNullOrEmpty(_input))
        {
            _input = InputLoader.LoadInput(3);
        }
    }

    internal static MatchCollection ParseMulInstructions(string input)
    {
        return Regex.Matches(input, @"mul\(\d+,\d+\)");
    }

    internal static string[] ParseUncorruptedInstructions(string input)
    {
        // Instructions are uncorrupted if they follow the pattern: mul(digits,digits)
        var instructions = ParseMulInstructions(input)
            .Select(m => m.Value)
            .ToArray();
        return instructions;
    }

    internal static long SumInstructions(string[] instructions)
    {
        long sum = 0;
        foreach (var instruction in instructions)
        {
            var parts = instruction.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var left = int.Parse(parts[0][4..]);
            var right = int.Parse(parts[1].TrimEnd(')'));
            sum += left * right;
        }
        return sum;
    }

    internal static MatchCollection ParseDoInstructions(string input)
    {
        return Regex.Matches(input, @"do\(\)");
    }

    internal static MatchCollection ParseDontInstructions(string input)
    {
        return Regex.Matches(input, @"don't\(\)");
    }

    internal static Instruction[] FindAllInstructions(string input)
    {
        var mulInstructions = ParseMulInstructions(input)
            .Select(m => new Instruction(m.Index, "mul", ParseParams(m.Value)))
            .ToArray();

        var doInstructions = ParseDoInstructions(input)
            .Select(m => new Instruction(m.Index, "enable", null))
            .ToArray();

        var dontInstructions = ParseDontInstructions(input)
            .Select(m => new Instruction(m.Index, "disable", null))
            .ToArray();

        var allInstructions = mulInstructions
            .Concat(doInstructions)
            .Concat(dontInstructions)
            .OrderBy(i => i.Index)
            .ToArray();

        return allInstructions;
    }

    internal static long SumInstructions(Instruction[] instructions)
    {
        long sum = 0;
        var enabled = true;
        foreach (var instruction in instructions)
        {
            switch (instruction.Type)
            {
                case "mul":
                    if (enabled)
                    {
                        sum += instruction.Params!.Value.Left * instruction.Params!.Value.Right;
                    }
                    break;
                case "enable":
                    enabled = true;
                    break;
                case "disable":
                    enabled = false;
                    break;
            }
        }
        return sum;
    }

    internal static (int Left, int Right) ParseParams(string instruction)
    {
        var parts = instruction.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var left = int.Parse(parts[0][4..]);
        var right = int.Parse(parts[1].TrimEnd(')'));
        return (left, right);
    }

    internal record Instruction(int Index, string Type, (int Left, int Right)? Params);
}
