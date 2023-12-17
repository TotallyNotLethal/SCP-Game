using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SCP049_2 : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform SCP0049Transform; // Transform of SCP-049
    public float followDistance = 10.0f;
    public LayerMask attackTargetsLayer;
    public float attackRange = 2.0f;

    private GameObject currentTarget;
    private bool isFollowingSCP0049 = true;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        SCP0049Transform = GameObject.FindWithTag("SCP0049").transform; // Make sure SCP-049 has the tag "SCP049"
    }

    void Update()
    {
        if (isFollowingSCP0049)
        {
            FollowSCP049();
        }

        PatrolArea();
        AttackTargets();
    }

    private void FollowSCP049()
    {
        if (Vector3.Distance(transform.position, SCP0049Transform.position) > followDistance)
        {
            navAgent.SetDestination(SCP0049Transform.position);
        }
        else
        {
            navAgent.ResetPath();
        }
    }

    private void PatrolArea()
    {
        // Implement patrolling logic here
        // SCP-049-2 can patrol an area when not following SCP-049
    }

    private void AttackTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRange, attackTargetsLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != currentTarget)
            {
                currentTarget = hitCollider.gameObject;
                StartCoroutine(AttackTarget(currentTarget));
            }
        }
    }

    private IEnumerator AttackTarget(GameObject target)
    {
        // Play attack animation or sound
        yield return new WaitForSeconds(1); // Duration of the attack

        // Logic to handle the attack effect
        // For example, reducing health of the target or similar effects

        currentTarget = null; // Reset target after attack
    }

    public void BreakObstacle(GameObject obstacle)
    {
        // Logic to break doors or obstacles
    }

    public void Scream()
    {
        // Play scream sound effect
    }

    // Additional methods or overrides can be implemented as needed
}
