using Godot;

using System;

public partial class PlayerEvent : Area2D
{
    bool isFocusTarget = false;
    public Action OnNodeTarget;

    [Export]
    private Sprite2D _lazer;


    public override void _Process(double delta)
    {
        if (Input.IsKeyPressed(Key.Space))
        {
            _lazer.Visible = true;
            OnNodeTarget?.Invoke();
        }
        else if (_lazer.Visible)
            _lazer.Visible = false;
    }
}
