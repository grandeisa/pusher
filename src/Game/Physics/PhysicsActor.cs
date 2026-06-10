using Raylib_cs;
using System.Numerics;
using Game.Core;
using Game.Utils;

namespace Game.Physics;

public abstract class PhysicsActor : Actor
{
    private const float MOVEMENT_THRESHOLD = 0.01f;
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

    public Vector2 velocity = Vector2.Zero;

    protected void AddCollider(Collider collider, Vector2 relativePosition)
    {
        _colliders.Add(collider, relativePosition);
        collider.position = relativePosition + position;
    }

    public HashSet<Collider>? MoveWithCollision(Vector2 velocity, float delta)
    {
        HashSet<Collider> collisions = [];
        Vector2 movement = velocity * delta;
        Vector2 remainingMovement = Vector2.Zero;

        do
        {
            movement += remainingMovement;
            remainingMovement = Vector2.Zero;
            foreach(Collider localCollider in _colliders.Keys)
            {
                Vector2 localCenter = localCollider.position + localCollider.size/2f;

                foreach(Collider collider in PhysicsHandler.GetColliders())
                {
                    if(_colliders.ContainsKey(collider)) continue;
                    Vector2 simulatedPosition = collider.position - localCollider.size/2f;
                    Vector2 simulatedSize = collider.size + localCollider.size;

                    // Compare segments from collider with line starting at localCenter. (CLOCKWISE)

                    List<(Vector2, Vector2)> segments = [
                    (simulatedPosition, simulatedPosition + simulatedSize * new Vector2(1f, 0f)), // UL -> UR
                    (simulatedPosition + simulatedSize * new Vector2(1f, 0f), simulatedPosition + simulatedSize), // UR -> DR
                    (simulatedPosition + simulatedSize, simulatedPosition + simulatedSize * new Vector2(0f, 1f)), // DR -> DL
                    (simulatedPosition + simulatedSize * new Vector2(0f, 1f), simulatedPosition) // DL -> UL
                    ];

                    foreach((Vector2, Vector2) segment in segments)
                    {
                        Vector2 collisionPoint = localCenter + movement;
                        if (Raylib.CheckCollisionLines(localCenter, localCenter + movement, segment.Item1, segment.Item2, ref collisionPoint))
                        {
                            Vector2 movementEnd = localCenter + movement;
                            Vector2 movementSurplus = movementEnd - collisionPoint;
                            movement = collisionPoint - localCenter;

                            Vector2 segmentNormal = GameMath.GetSegmentNormal(segment.Item1, segment.Item2);

                            remainingMovement += GameMath.Project(movementSurplus, GameMath.RotatedVector(segmentNormal, (float) Math.PI));
                        }
                    }

                }
                
            }
        }while(remainingMovement.Length() > MOVEMENT_THRESHOLD);

        this.velocity = GameMath.Project(velocity, movement);

        position += movement;

        if(collisions.Count == 0) return null;

        return collisions;
    }
}