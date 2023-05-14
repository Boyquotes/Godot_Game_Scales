using Godot;

using System;

public partial class PlayerDataBase : Node2D
{
    public float EnergyScale { get; set; }

    [Export]
    private Player Player { get; set; }
}
