namespace Game.Core;

public class Scene
{
    public static HashSet<Scene> Scenes = [];
    private HashSet<Actor> _actors;

    private bool _hasStarted = false;

    public Scene(HashSet<Actor> actors)
    {
        Scenes.Add(this);

        _actors = actors;
    }

    public void Start()
    {
        foreach(Actor a in _actors)
        {
            a.Start();
        }
        _hasStarted = true;
    }

    public void Update(float delta)
    {
        foreach(Actor a in _actors)
        {
            a.Update(delta);
        }
    }

    public void PhysicsUpdate(float delta)
    {
        foreach(Actor a in _actors)
        {
            a.PhysicsUpdate(delta);
        }
    }

    // Simple rendering function, change it to a layer stucture later.
    public void Render()
    {
        foreach(Actor a in _actors)
        {
            a.Render();
        }
    }

    public void AddActor(Actor actor)
    {
        if(!_actors.Add(actor)) return;
        if (!_hasStarted) return;

        actor.Start();
    }

    public bool HasActor(Actor actor) => _actors.Contains(actor);
}