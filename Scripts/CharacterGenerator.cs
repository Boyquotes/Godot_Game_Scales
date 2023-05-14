using Godot;

using System;
using System.Threading;
using System.Threading.Tasks;

public partial class CharacterGenerator : Node2D
{
    [Export]
    private ScalesDataBase _scalesData;

    public CharacterBot Characte;

    private PackedScene _prefab = GD.Load<PackedScene>("Prefabs/Bot.tscn");
    //private PackedScene _prefabsNight = GD.Load<PackedScene>("Prefabs/BotNight.tscn");

    private bool _isExit = false;

    public override void _ExitTree()
    {
        _isExit = true;
    }

    public override void _Ready()
    {
        Instantiate(-200, -40);
        Instantiate(40, 200);
        Thread thread = new Thread(() => Generate());
        thread.Start();



    }



    public void Generate()
    {
        while (!_isExit)
        {
            Thread.Sleep(GD.RandRange(1000,20000));
            int randLight = GD.RandRange(0, 100);
            int randNight = GD.RandRange(0, 100);

            if (randLight > 50)
                Instantiate(-200, -40);
            if (randNight > 50)
                Instantiate(40, 200);
        }
    }

    public void Instantiate(int minPostion, int maxPostion)
    {

        //if (minPostion > 0)
            Characte = _prefab.Instantiate() as CharacterBot;
        //else
            //Characte = _prefabsNight.Instantiate() as CharacterBot;
        Characte.ScalesData = _scalesData;
        Characte.Position = new Vector2(GD.RandRange(minPostion, maxPostion), -16);
        Characte.MinPosition = minPostion;
        Characte.MaxPosition = maxPostion;
        Characte.TypeCharacter = Mathf.Sign(minPostion);

        AddChild(Characte);
    }
}
