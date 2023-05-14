using Godot;

using System;

public partial class ScalesDataBase : Node2D
{

    [Export]
    public Scales Scales { get; set; }

    [Export]
    public PlayerDataBase PlayerData { get; set; }

    public override void _EnterTree()
    {
        Scales.TriggerEnter += ScalesTriggerEnter;
    }

    public override void _ExitTree()
    {
        Scales.TriggerEnter -= ScalesTriggerEnter;
    }

    private void ScalesTriggerEnter()
    {
        Scales.Weight += PlayerData.EnergyScale;
    }
}
