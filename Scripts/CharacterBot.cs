using Godot;

using System.Threading;

public partial class CharacterBot : Sprite2D
{

    [Export]
    private Texture2D[] _skinsLight;

    [Export]
    private Texture2D[] _skinsNight;

    public ScalesDataBase ScalesData;
    public int MinPosition = 0;
    public int MaxPosition = 0;
    public int TypeCharacter = 0;
    public const float MaxEnergyScale = 40;

    private PlayerEvent _playerEvent;
    private const int MaxAge = 100;
    private int _age = 0;
    private int _force = 0;

    private double _timeSleep = 0;
    private double _timeSleepMax = 6;

    private float _newPosition = 0;
    public float EnergyScale { get; set; } = 0;

    public override void _EnterTree()
    {
        UpdateAge();
    }

    public override void _ExitTree()
    {
        ScalesData.Scales.Weight -= EnergyScale;
        if (_playerEvent != null)
            _playerEvent.OnNodeTarget -= Destroy;
    }

    public override void _Ready()
    {
        _newPosition = GD.RandRange(MinPosition, MaxPosition);
    }

    public override void _Process(double delta)
    {
        _timeSleep += delta;
        if (_timeSleep > _timeSleepMax)
        {
            _timeSleep = 0;
            UpdateAge();
        }
        OnMove(_timeSleep);
    }

    private void UpdateAge()
    {
        GD.Print(">>Age:" + _age);
        if (_age < MaxAge)
        {
            switch (_age)
            {
                case 0:
                    NewSkin(0, 1, 4);
                    break;
                case 10:
                    NewSkin(1, -1, 8);
                    break;
                case 20:
                    NewSkin(2, -4, 16);
                    break;
                case 30:
                    NewSkin(3, -6, 20);
                    break;
                case 40:
                    UIRiderector.Instance.Record++;
                    NewSkin(4, -8, 16);
                    break;

            }

            ScalesData.Scales.Weight += _force;
            EnergyScale += _force;
            _age++;
        }
    }

    private void NewSkin(int index, int min, int max)
    {
        _force += GD.RandRange(min, max) * TypeCharacter;
        if (_force > 0)
            Texture = _skinsLight[index];
        else
            Texture = _skinsNight[index];

    }


    private void OnMove(double delta)
    {
        if (Mathf.Abs(_newPosition - Position.X) < 0.1f && _timeSleep < 1)
            _newPosition = GD.RandRange(MinPosition, MaxPosition);
        else
            Position = Position.MoveToward(new Vector2(_newPosition, Position.Y), (float)delta / 8f);
    }

    private void OnTriggerEnter(Area2D area)
    {
        if ((area as PlayerEvent) == null) return;

        _playerEvent = area as PlayerEvent;
        if (_playerEvent != null)
            _playerEvent.OnNodeTarget += Destroy;
    }

    private void OnTriggerExit(Area2D area)
    {
        if (_playerEvent != null)
            _playerEvent.OnNodeTarget -= Destroy;
    }

    private void Destroy()
    {
        QueueFree();
    }
}

