using Godot;

using System;

public partial class UIRiderector : Node
{

    [Export]
    private Label _recordLabel;

    public int _record = 0;
    public int Record
    {
        get => _record;
        set
        {
            _record = value;
            _recordLabel.Text = _record.ToString("000");
        }
    }

    public static UIRiderector Instance { get; private set; }

    public override void _EnterTree()
    {
        Instance = this;
    }
}
