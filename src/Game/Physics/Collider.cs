using System.Numerics;

namespace Game.Physics;

/// <summary>
/// 2D collider class. 
/// For this game, all colliders are rectangles.
/// </summary>
public class Collider
{
    public Vector2 position;
    public Vector2 size;
    public byte layer;

    public Collider(Vector2 position, Vector2 size, byte layer)
    {
        this.position = position;
        this.size = size;
        this.layer = layer;

        PhysicsHandler.AddCollider(this);
    }

    public Collider(Vector2 size, byte layer)
    {
        this.size = size;
        this.layer = layer;

        PhysicsHandler.AddCollider(this);
    }
}