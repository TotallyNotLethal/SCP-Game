using UnityEngine;

public class SCP0096 : SCP
{
    private bool isAgitated;
    private Transform target;
    public float chaseSpeed = 10f;
    public float attackRange = 2f;

    protected override void Awake()
    {
        base.Awake();
        InitializeSCP("SCP-0096", "The Shy Guy", SCPClass.Euclid, "A humanoid creature that becomes extremely aggressive upon someone viewing its face.");
        isAgitated = false;
    }

    protected override void Update()
    {
        base.Update();
        if (isAgitated)
        {
            ChaseTarget();
        }
    }

    public void TriggerAgitation(Transform viewer)
    {
        isAgitated = true;
        target = viewer;
        // Additional logic for becoming agitated, like playing a sound or animation
    }

    private void ChaseTarget()
    {
        if (target != null)
        {
            navAgent.SetDestination(target.position);
            navAgent.speed = chaseSpeed;

            if (Vector3.Distance(transform.position, target.position) <= attackRange)
            {
                AttackTarget();
            }
        }
    }

    private void AttackTarget()
    {
        // Logic for attacking the target
        // This could involve reducing the target's health, playing attack animations, etc.
        isAgitated = false; // Reset agitation state after the attack
    }

    public override void ActivateEffect(PlayerStats playerStats)
    {
        // Special effects that SCP-096 can have on the player
    }

    // Additional SCP-096 specific methods...
}
