public abstract class BaseState
{
    public abstract void EnterState(SCP scp);

    public abstract void UpdateState(SCP scp);

    public virtual void ExitState(SCP scp)
    {
    }
}