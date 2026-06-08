namespace Game.Core;

public abstract class Actor
{
    public virtual void Start(){}
    public virtual void Update(float delta){}
    public virtual void PhysicsUpdate(float delta){}
    public virtual void Render(){}
}