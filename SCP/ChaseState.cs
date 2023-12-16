using UnityEngine;

public class ChaseState : BaseState
{
    private SCP scp;
    private float chaseTimer;

    public ChaseState(SCP scp)
    {
        this.scp = scp;
        chaseTimer = 8f;
    }
    #region States
    public override void EnterState(SCP scp)
    {
        scp.navAgent.isStopped = false;
        chaseTimer = 8f;
    }

    public override void UpdateState(SCP scp)
    {
        ChasePlayer(scp);

        if (!scp.CanSeePlayer())
        {
            chaseTimer -= Time.deltaTime;
            if (chaseTimer <= 0)
            {
                scp.SwitchState(new PatrolState(scp));
            }
        }
    }

    public override void ExitState(SCP scp)
    {
    }
    #endregion

    private void ChasePlayer(SCP scp)
    {
        float distanceToPlayer = Vector3.Distance(scp.transform.position, scp.playerTransform.position);
        bool lineOfSight = !Physics.Linecast(scp.transform.position, scp.playerTransform.position, LayerMask.NameToLayer("Default"));

        if (!lineOfSight || distanceToPlayer > scp.chaseDistance)
        {
            scp.navAgent.SetDestination(scp.playerTransform.position);
            scp.navAgent.isStopped = false;
        }
        else if (lineOfSight && distanceToPlayer < scp.chaseDistance)
        {
            Vector3 directionAwayFromPlayer = scp.transform.position - scp.playerTransform.position;
            Vector3 newPosition = scp.transform.position + directionAwayFromPlayer.normalized * (scp.chaseDistance - distanceToPlayer);
            scp.navAgent.SetDestination(newPosition);
            scp.navAgent.isStopped = false;
        }

        //scp.RotateTowards(scp.playerTransform.position);
    }
}