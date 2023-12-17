using UnityEngine;
using UnityEngine.AI;

public class SCP0131 : SCP
{
    public float followDistance = 5.0f;
    public LayerMask alertableObjects;
    public Transform SCP173Transform;
    public float SCP173AlertDistance = 10.0f;
    public float dangerEvadeDistance = 5.0f;
    public LayerMask puzzleObjectsLayer;

    private Transform target;
    private bool isWatchingSCP173 = false;
    private bool isEvadingDanger = false;

    protected override void Start()
    {
        base.Start();
        target = FindObjectOfType<Player>().transform;
        SCP173Transform = GameObject.FindWithTag("SCP173").transform;
    }

    protected override void Update()
    {
        base.Update();
        WatchForSCP173();
        EvadeDanger();
        InvestigateObjects();
        if (!isWatchingSCP173 && !isEvadingDanger)
        {
            FollowTarget();
            LookAtInterestingObjects();
        }
    }

    private void FollowTarget()
    {
        if (Vector3.Distance(transform.position, target.position) > followDistance)
        {
            navAgent.SetDestination(target.position);
        }
        else
        {
            navAgent.ResetPath();
        }
    }

    private void LookAtInterestingObjects()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f, alertableObjects))
        {
            AlertPlayer(hit.collider.gameObject);
        }
    }

    private void WatchForSCP173()
    {
        float distanceToSCP173 = Vector3.Distance(transform.position, SCP173Transform.position);
        if (distanceToSCP173 < SCP173AlertDistance)
        {
            isWatchingSCP173 = true;
            navAgent.ResetPath();
            transform.LookAt(SCP173Transform);
        }
        else
        {
            isWatchingSCP173 = false;
        }
    }

    private void EvadeDanger()
    {
        if (isWatchingSCP173 && Vector3.Distance(transform.position, SCP173Transform.position) < dangerEvadeDistance)
        {
            isEvadingDanger = true;
            Vector3 directionAwayFromDanger = (transform.position - SCP173Transform.position).normalized;
            Vector3 newPos = transform.position + directionAwayFromDanger * 5.0f;
            navAgent.SetDestination(newPos);
        }
        else
        {
            isEvadingDanger = false;
        }
    }

    private void InvestigateObjects()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10.0f, puzzleObjectsLayer);
        foreach (var hitCollider in hitColliders)
        {
            // Logic to investigate or interact with puzzle objects
            Debug.Log("SCP-131 is investigating: " + hitCollider.name);
        }
    }

    private void AlertPlayer(GameObject alertObject)
    {
        // Logic to alert the player about the object or danger
        Debug.Log("SCP-131 alerts the player about: " + alertObject.name);
    }

    // Additional methods or overrides can be implemented as needed
}
