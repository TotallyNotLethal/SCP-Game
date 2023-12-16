using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ObserverController : MonoBehaviour
{
    #region Fields
    public Transform[] waypoints;
    public Transform playerTransform;
    public PlayerController playerController;
    public LayerMask viewMask;
    public Color fovGizmoColor = Color.yellow;
    public float viewDistance = 10f;
    public float fieldOfView = 45f;
    public float chaseDistance = 8f;

    public NavMeshAgent navAgent;
    private BaseState currentState;
    public int currentWaypointIndex = 0;
    public bool isLookingAround = false;
    private bool isBlinking = false;
    private bool currentlyInView = false;
    private bool playerInView = false;

    public float chaseTimerDuration = 8f;
    #endregion

    #region Unity Lifecycle Methods
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        //SwitchState(new PatrolState(this));
        StartCoroutine(AutomaticBlink());
    }

    void Update()
    {
        //currentState.UpdateState(this);
        HandlePlayerVisibility();
        HandleInput();
    }

    void OnDrawGizmos()
    {
        if (playerTransform == null)
            return;

        DrawFieldOfView();
    }
    #endregion

    #region State Machine Methods

    public void SwitchState(BaseState newState)
    {
        //currentState?.ExitState(this);
        currentState = newState;
        //currentState.EnterState(this);
    }

    public bool IsPatrolling()
    {
        return currentState is PatrolState;
    }

    public bool IsChasing()
    {
        return currentState is ChaseState;
    }
    #endregion

    #region Handle Methods
    private void HandlePlayerVisibility()
    {
        currentlyInView = CanSeePlayer();

        if (currentlyInView && !playerInView)
        {
            //playerController.CaughtByObserver(transform.position);
        }
        else if (!currentlyInView && playerInView)
        {
            //playerController.FreeFromObserver();
        }

        playerInView = currentlyInView;
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RotateObserver(-45);
        }
    }
    
    public void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public bool CanSeePlayer()
    {
        if (isBlinking) return false;

        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < viewDistance)
        {
            Vector3 dirToPlayer = (playerTransform.position - transform.position).normalized;
            float angleBetweenObserverAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if (angleBetweenObserverAndPlayer < fieldOfView / 2f && !Physics.Linecast(transform.position, playerTransform.position, viewMask))
            {
                return true;
            }
        }
        return false;
    }

    private void RotateObserver(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }

    private void DrawFieldOfView()
    {
        Vector3 leftFOVBoundary = DirectionFromAngle(-fieldOfView / 2);
        Vector3 rightFOVBoundary = DirectionFromAngle(fieldOfView / 2);

        Gizmos.color = fovGizmoColor;

        Gizmos.DrawRay(transform.position, leftFOVBoundary * viewDistance);
        Gizmos.DrawRay(transform.position, rightFOVBoundary * viewDistance);

        if (CanSeePlayer())
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, playerTransform.position);
        }
    }

    private Vector3 DirectionFromAngle(float angleInDegrees)
    {
        angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    #endregion

    #region Coroutines
    IEnumerator AutomaticBlink()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(4f, 6f));

            bool wasInViewBeforeBlink = currentlyInView;
            isBlinking = true;

            if (wasInViewBeforeBlink)
            {
                //playerController.FreeFromObserver();
            }

            yield return new WaitForSeconds(0.2f);
            isBlinking = false;

            currentlyInView = CanSeePlayer();
            if (currentlyInView && !playerInView)
            {
                //playerController.CaughtByObserver(transform.position);
            }
            else if (!currentlyInView && playerInView)
            {
                //playerController.FreeFromObserver();
            }
            playerInView = currentlyInView;
        }
    }

    public IEnumerator LookAroundAtWaypoint()
    {
        isLookingAround = true;

        RotateObserver(45);
        yield return new WaitForSeconds(1);
        RotateObserver(-90);
        yield return new WaitForSeconds(1);
        RotateObserver(45);

        isLookingAround = false;
    }
    #endregion
}