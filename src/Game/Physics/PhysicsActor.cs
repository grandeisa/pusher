using System.Numerics;
using Game.Core;

namespace Game.Physics;

public abstract class PhysicsActor : Actor
{
    private Vector2 _position = Vector2.Zero;

    public Vector2 position
    {
        get => _position;
        set
        {
            _position = value;
            foreach(Collider collider in _colliders.Keys)
            {
                collider.position = _colliders[collider] + value;
            }
        }
    }
    // Colliders and their relative position to this object.
    protected Dictionary<Collider, Vector2> _colliders = []; 

    protected void AddCollider(Collider collider, Vector2 relativePosition)
    {
        _colliders.Add(collider, relativePosition);
        collider.position = relativePosition + position;
    }
}