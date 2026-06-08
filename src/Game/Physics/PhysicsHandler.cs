namespace Game.Physics;

public static class PhysicsHandler
{
    private static HashSet<Collider> _colliders = [];

    public static void AddCollider(Collider collider)
    {
        _colliders.Add(collider);
    }

    public static HashSet<Collider> GetColliders() => _colliders;
}