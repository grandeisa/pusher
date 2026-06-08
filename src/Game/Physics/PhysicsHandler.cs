namespace Game.Physics;

public static class PhysicsHandler
{
    public const float GRAVITY = 98f;

    private static HashSet<Collider> _colliders = [];

    public static void AddCollider(Collider collider)
    {
        _colliders.Add(collider);
    }

    public static HashSet<Collider> GetColliders() => _colliders;
}