using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SCP0049 : SCP
{
    public float cureRange = 5.0f;
    public LayerMask cureTargetsLayer;
    public GameObject SCP0049_2Prefab;
    private bool isCuring = false;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        PatrolAndWander();
        CheckAndCureTargets();
    }

    private void PatrolAndWander()
    {
        if (!navAgent.hasPath)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10.0f;
            randomDirection += transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10.0f, NavMesh.AllAreas);
            navAgent.SetDestination(hit.position);
        }
    }

    private void CheckAndCureTargets()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, cureRange, cureTargetsLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (!isCuring && !hitCollider.GetComponent<SCP0049_2>())
            {
                StartCoroutine(CureTarget(hitCollider.gameObject));
            }
        }
    }

    private IEnumerator CureTarget(GameObject target)
    {
        isCuring = true;
        navAgent.ResetPath();
        transform.LookAt(target.transform);

        yield return new WaitForSeconds(3);

        Instantiate(SCP0049_2Prefab, target.transform.position, Quaternion.identity);
        Destroy(target);

        isCuring = false;
    }
}
