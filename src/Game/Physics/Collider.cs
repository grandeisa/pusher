using System.Numerics;

namespace Game.Physics;

/// <summary>
/// 2D collider class. 
/// For this game, all colliders are rectangles.
/// </summary>
class Collider
{
    public Vector2 position;
    public Vector2 size;
    public byte layer;
}