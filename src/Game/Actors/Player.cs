using System.Numerics;
using Game.Physics;
using Raylib_cs;

namespace Game.Actors;

public class Player : PhysicsActor
{
    const float SPEED = 64f;

    float horizontalInput = 0f;

    public Player(Vector2 position)
    {   
        this.position = position;

        // Full body collider
        AddCollider(new Collider(new Vector2(64, 64), 0b1000000), Vector2.Zero); 
    }

    public override void Update(float delta)
    {
        horizontalInput = Raylib.IsKeyDown(KeyboardKey.Right) - Raylib.IsKeyDown(KeyboardKey.Left);
        horizontalInput = Math.Clamp(horizontalInput, -1f, 1f);
    }

    public override void PhysicsUpdate(float delta)
    {
        velocity.X = horizontalInput * SPEED;

        MoveWithCollision(delta);
    }

    public override void Render()
    {
        foreach(Collider collider in _colliders.Keys)
        {
            Raylib.DrawRectangleLines((int)collider.position.X, (int)collider.position.Y, (int)collider.size.X, (int)collider.size.Y, Color.SkyBlue);
        }
    }
}