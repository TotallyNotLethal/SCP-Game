using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SCP049_2 : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform SCP0049Transform;
    public float followDistance = 10.0f;
    public LayerMask attackTargetsLayer;
    public float attackRange = 2.0f;

    private GameObject currentTarget;
    private bool isFollowingSCP0049 = true;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        SCP0049Transform = GameObject.FindWithTag("SCP0049").transform;
    }

    void Update()
    {
        if (isFollowingSCP0049)
        {
            FollowSCP0049();
        }

        PatrolArea();
        AttackTargets();
    }

    private void FollowSCP0049()
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
        yield return new WaitForSeconds(1);

        currentTarget = null;
    }

    public void BreakObstacle(GameObject obstacle)
    {
    }

    public void Scream()
    {
    }
}
