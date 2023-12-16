
using UnityEngine;

public class PatrolState : BaseState
{
    private SCP scp;

    public PatrolState(SCP scp)
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
        Patrol(scp);

        if (scp.CanSeePlayer())
        {
            scp.SwitchState(new ChaseState(scp));
        }
    }

    public override void ExitState(SCP scp)
    {
    }
#endregion
    private void Patrol(SCP scp)
    {
        if (scp.waypoints.Length == 0 || scp.isLookingAround) return;

        scp.navAgent.SetDestination(scp.waypoints[scp.currentWaypointIndex].position);
        if (!scp.navAgent.pathPending && scp.navAgent.remainingDistance <= scp.navAgent.stoppingDistance)
        {
            if (!scp.navAgent.hasPath || scp.navAgent.velocity.sqrMagnitude == 0f)
            {
                scp.StartCoroutine(scp.LookAroundAtWaypoint());
                scp.currentWaypointIndex = (scp.currentWaypointIndex + 1) % scp.waypoints.Length;
            }
        }
    }
}