using Godot;
using System;

public partial class BaseDay : Node2D
{
    private const string DAY_PREFIX = "Day";

    [Node("DaySign/Text")]
    private RichTextLabel _dayLabel;

    [Node("DoorArea")]
    private Area2D _doorArea;

    [Export]
    public int DayNumber { get; set; } = 1;

    [Export]
    public PackedScene LevelScene { get; set; }

    public override void _Ready()
    {
        _dayLabel.Text = $"{DAY_PREFIX} {DayNumber}";

        _doorArea.BodyEntered += OnBodyEntered;
        _doorArea.BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            player.CurrentDay = this;
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (body is Player player)
        {
            player.CurrentDay = null;
        }
    }
}
