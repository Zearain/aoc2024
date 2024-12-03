using Godot;
using Godot.Collections;

using System;

public partial class BaseDaySpawner : Node2D
{
    private const string DAY_PATH_PREFIX = "res://src/days/Day";

    [Node("EndingTerrain")]
    private TileMapLayer _endingTerrain;

    [Export]
    public PackedScene BaseDayScene { get; set; }

    [Export]
    public Array<int> ImplementedDays { get; set; }

    public override void _Ready()
    {
        for (var i = 0; i < ImplementedDays.Count; i++)
        {
            var dayNumber = ImplementedDays[i];
            var day = BaseDayScene.Instantiate<BaseDay>();
            day.DayNumber = dayNumber;
            day.LevelScene = GetLevelScene(dayNumber);

            AddChild(day);
            day.Position = new Vector2((128 * 5) * i, 0);
        }

        _endingTerrain.Position = new Vector2((128 * 5) * ImplementedDays.Count, 0);
    }

    private static PackedScene GetLevelScene(int dayNumber)
    {
        var path = $"{DAY_PATH_PREFIX}{dayNumber}.tscn";
        return GD.Load<PackedScene>(path);
    }
}
