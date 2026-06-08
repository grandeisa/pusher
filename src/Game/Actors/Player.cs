using System.Numerics;
using Game.Physics;
using Raylib_cs;

namespace Game.Actors;

public class Player : PhysicsActor
{
    const float SPEED = 604f;
    const float JUMP_FORCE = -256f;

    float horizontalInput = 0f;
    Vector2 size = new(64, 64);

    public Player(Vector2 position)
    {   
        this.position = position;

        // Full body collider
        AddCollider(new Collider(size, 0b1000000), Vector2.Zero); 
    }

    public override void Update(float delta)
    {
        horizontalInput = Raylib.IsKeyDown(KeyboardKey.Right) - Raylib.IsKeyDown(KeyboardKey.Left);
        horizontalInput = Math.Clamp(horizontalInput, -1f, 1f);

        if(Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            velocity.Y = JUMP_FORCE;
        }
    }

    public override void PhysicsUpdate(float delta)
    {
        velocity.X = horizontalInput * SPEED;
        velocity.Y += PhysicsHandler.GRAVITY * delta;

        if(MoveWithCollision(velocity.Y * new Vector2(0f, 1f), delta) != null)
        {
            velocity.Y = 0f;
        }
        if(MoveWithCollision(velocity.X * new Vector2(1f, 0f), delta) != null)
        {
            velocity.X = 0f;
        }
    }

    public override void Render()
    {
        Raylib.DrawRectangleV(position, size, Color.Green);
    }
}