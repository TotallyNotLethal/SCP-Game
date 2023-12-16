using UnityEngine;

public class PlayerState : BaseState
{
    private SCP scp;

    public PlayerState(SCP scp)
    {
        this.scp = scp;
    }
    #region States
    public override void EnterState(SCP scp)
    {
        scp.navAgent.isStopped = false;
    }

    public override void UpdateState(SCP scp)
    {
    }

    public override void ExitState(SCP scp)
    {
    }
    #endregion
}