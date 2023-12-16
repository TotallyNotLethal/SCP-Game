using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

[System.Serializable]
public class SCP : MonoBehaviour
{
    [SerializeField]
    public string SCPNumber { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public SCPClass ObjectClass { get; private set; }

    public BaseState currentState;
    public NavMeshAgent navAgent;
    public bool isLookingAround;
    private List<Player> allPlayers;
    public Transform playerTransform;
    internal float chaseDistance = 8f;
    public Transform[] waypoints;
    public Camera playerCamera;
    public int currentWaypointIndex = 0;
    public float mouseSensitivity = 100;
    public float xRotation = 0;
    public float yRotation = 0;
    
    // SCP-specific attributes
    public bool IsAggressive { get; set; }
    public float InfluenceRadius { get; set; }
    public float HealthImpact { get; set; }
    public float MentalHealthImpact { get; set; }
    public bool CausesPhysicalAlteration { get; set; }
    public bool IsTelekinetic { get; set; }
    public bool CanTeleport { get; set; }
    public AudioClip SoundEffect { get; set; }

    // Constructor
    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        allPlayers = new List<Player>(FindObjectsOfType<Player>());
        // Default values can be set here or in derived classes
    }

    protected virtual void Start()
    {
        SwitchState(new PlayerState(this));
    }

    protected virtual void Update()
    {
        currentState.UpdateState(this);
    }

    public void InitializeSCP(string scpNumber, string name, SCPClass objectClass, string description)
    {
        SCPNumber = scpNumber;
        Name = name;
        ObjectClass = objectClass;
        Description = description;
    }

    public void SwitchState(BaseState newState)
    {
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
        Debug.Log(currentState.ToString());
    }

    public bool IsPatrolling()
    {
        return currentState is PatrolState;
    }

    public bool IsChasing()
    {
        return currentState is ChaseState;
    }

    public bool IsWandering()
    {
        return currentState is PatrolState;
    }

    public bool IsInvestigating()
    {
        return currentState is ChaseState;
    }

    // SCP behavior methods
    public virtual void ActivateEffect(PlayerStats playerStats)
    {
        // Implement the logic for the SCP's effect on the player
    }

    public virtual void DeactivateEffect(PlayerStats playerStats)
    {
        // Implement logic to reverse or stop the SCP's effect
    }

    public virtual void EncounterBehavior()
    {
        // Define behavior when the player encounters this SCP
    }

    // Additional methods for specific SCP actions
    public void Teleport(Vector3 newPosition)
    {
        if (CanTeleport)
        {
            // Implement teleportation logic
        }
    }

    public void ApplyTelekineticForce(Vector3 force)
    {
        if (IsTelekinetic)
        {
            // Apply a telekinetic force to objects or the player
        }
    }


    #region Player Movement
    private void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward * moveVertical + right * moveHorizontal);
        if (movement.magnitude > 0.1f)
        {
            Vector3 nextPosition = transform.position + movement * 6 * Time.deltaTime;
            transform.position = nextPosition;
            transform.rotation = Quaternion.Euler(0, playerCamera.transform.eulerAngles.y, 0);
        }
    }

    public void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;

        //if (!isBeingObserved)
        //{
        //    playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //    transform.Rotate(Vector3.up * mouseX);
        //}
        //else
        //{
        //    GetComponent<Camera>().transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        //}
    }
    #endregion


    #region State-called Methods
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

    public void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public bool CanSeePlayer()
    {
        //foreach (Player p in allPlayers)
        //{
        Player p = FindObjectsOfType<Player>()[0];
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if (distanceToPlayer < 10)
            {
                Vector3 dirToPlayer = (p.transform.position - transform.position).normalized;
                float angleBetweenObserverAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);

                if (angleBetweenObserverAndPlayer < 75 / 2f && !Physics.Linecast(transform.position, p.transform.position, LayerMask.NameToLayer("Default")))
                {
                    return true;
                }
            }
            return false;
        //}
        //return false;
    }

    private void RotateObserver(float angle)
    {
        transform.Rotate(Vector3.up, angle);
    }
    #endregion

    // Additional methods for SCP-specific mechanics
}

public enum SCPClass
{
    Safe,      // SCPs that are easily and safely contained
    Euclid,    // SCPs that require more resources to contain completely or reliably
    Keter,     // SCPs that are exceedingly difficult to contain consistently or reliably
    Thaumiel,  // SCPs that are used to contain or counteract the effects of other SCPs
    Apollyon   // SCPs that cannot be contained or pose an existential threat to humanity or reality
}
