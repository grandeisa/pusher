using System.Numerics;
using Game.Actors;
using Game.Core;
using Game.Physics;

namespace Game.Scenes;

public class TestScene : Scene
{
    public TestScene() : base([])
    {
        AddActor(new Player(new(256, 256)));

        // Base floor collider
        PhysicsHandler.AddCollider(new(new Vector2(0, 536), new Vector2(800, 32), 0b10000000));
    }


}