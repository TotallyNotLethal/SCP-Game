using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SCP0173 : SCP
{
    [SerializeField]
    private bool isBeingObserved;
    public float MovementSpeed { get; private set; }
    public Vector3 LastKnownPlayerPosition { get; private set; }
    private float attackCooldown;
    private const float AttackRange = 1f; // Example range for attack
    //public GameObject spiritPrefab;
    //private GameObject spiritInstance;
    public GameObject spiritBoundarySphere;
    public float spiritMoveSpeed = 8f;
    public float spiritRange = 8f;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private Camera spiritCamera;
    public float moveSpeed = 5f;

    protected override void Awake()
    {
        base.Awake();
        InitializeSCP("SCP-0173", "The Sculpture", SCPClass.Euclid, "A humanoid sculpture that moves rapidly when unobserved.");
        IsAggressive = true;
        MovementSpeed = 10f; // Example speed when unobserved
        attackCooldown = 0f;
    }
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckForObservation();
        HandleMovementAndSpiritMode();
    }

    public void ActivateEffect(Player player)
    {
        if (!isBeingObserved)
        {
            //MoveTowardsTarget(LastKnownPlayerPosition);
            if (IsPlayerInRange(player))
            {
                if (attackCooldown <= 0)
                {
                    Attack(player);
                    attackCooldown = 1f;
                }
            }
        }

        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
    }

    private void CheckForObservation()
    {
        isBeingObserved = false;
        foreach (var player in FindObjectsOfType<Player>())
        {
            if (IsInViewOfPlayer(player))
            {
                navAgent.isStopped = true;
                isBeingObserved = true;
                break;
            }
        }
    }

    private bool IsInViewOfPlayer(Player player)
    {
        Vector3 directionToSCP = (transform.position - player.transform.position).normalized;
        float angleToPlayer = Vector3.Angle(player.transform.forward, directionToSCP);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Check if SCP-173 is within the player's field of view and within a certain distance
        if (angleToPlayer <= 45.0f && distanceToPlayer <= 20.0f) // Example values for FOV angle and max distance
        {
            RaycastHit hit;
            // Raycast to check if there is a direct line of sight
            if (Physics.Raycast(player.transform.position, directionToSCP, out hit, distanceToPlayer))
            {
                // Check if the raycast hits SCP-173 and not another object
                return hit.transform == transform;
            }
        }
        return false;
    }

    public void SetObservationStatus(bool status, Vector3 playerPosition)
    {
        isBeingObserved = status;
        if (status)
        {
            LastKnownPlayerPosition = playerPosition;
        }
    }

    private void HandleMovementAndSpiritMode()
    {
        if (isBeingObserved)
        {
            // Freeze SCP-173 and activate spirit mode
            navAgent.isStopped = true;
            CaughtByObserver();
            MoveSpirit();
        }
        else
        {
            // Allow SCP-173 to move and deactivate spirit mode
            navAgent.isStopped = false;
            FreeFromObserver();
            //ActivateEffect(); // Continue with SCP-173's usual behavior
        }
    }

    private void MoveTowardsTarget(Vector3 targetPosition)
    {
        if (!isBeingObserved)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.position += direction * MovementSpeed * Time.deltaTime;
        }
    }

    private bool IsPlayerInRange(Player player)
    {
        return Vector3.Distance(transform.position, player.transform.position) < AttackRange;
    }

    private void Attack(Player player)
    {
        player.Stats.SetHealth(0);
        // Additionally, trigger any relevant effects, animations, or sounds
    }

    // Additional SCP-173 specific methods

    public void InstantaneousMovement(Vector3 targetPosition)
    {
        // Method to simulate instantaneous movement when not observed
        if (!isBeingObserved)
        {
            transform.position = targetPosition;
            // Trigger any relevant effects (like sound, particle effects)
        }
    }

    public void CheckEnvironmentInteraction()
    {
        // Check for interactions with the environment
        // For example, SCP-173 could interact with specific objects or obstacles in its path
    }

    public void TriggerSpecialEffect()
    {
        // Trigger a special effect unique to SCP-173
        // This could be a visual effect, sound, or a gameplay mechanic
    }

    public void EmitSound()
    {
        // SCP-173 can emit a distinct sound, contributing to the eerie atmosphere
        // Play sound effect here
    }

    public void ApplyPsychologicalEffect(PlayerStats playerStats)
    {
        // SCP-173 can have a psychological impact on the player
        playerStats.UpdateMentalHealth(-MentalHealthImpact);
        // Implement additional psychological effect logic
    }

    public void AvoidLightSources()
    {
        // SCP-173 may behave differently in light or darkness
        // Implement logic for SCP-173 to avoid or react to light sources
    }

    // Overriding base methods for SCP-173 specific behaviors
    public override void EncounterBehavior()
    {
        // Define specific behavior when the player encounters SCP-173
        EmitSound();
        //ApplyPsychologicalEffect(Stats);
        // Additional encounter logic...
    }

    private void MoveSpirit()
    {
        // Assume playerCamera is the camera attached to the player
        if (isBeingObserved)
        {
            // Handle rotation
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

            // Handle movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 forward = playerCamera.transform.forward;
            Vector3 right = playerCamera.transform.right;
            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 movement = (forward * moveVertical + right * moveHorizontal).normalized * spiritMoveSpeed;
            Vector3 potentialPosition = playerCamera.transform.position + movement * Time.deltaTime;
            Vector3 offset = potentialPosition - originalPosition;

            if (offset.magnitude > spiritRange)
            {
                offset = offset.normalized * spiritRange;
                potentialPosition = originalPosition + offset;
            }
            playerCamera.transform.position = potentialPosition;
        }
    }



    #region Observer Interaction
    public void CaughtByObserver()
    {
        isBeingObserved = true;
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    public void FreeFromObserver()
    {
        // Teleport to the camera's position and orient towards the camera's forward direction once
        var rotation = playerCamera.transform.rotation;

        Vector3 spiritForward = playerCamera.transform.forward;
        spiritForward.y = 0;
        transform.rotation = Quaternion.LookRotation(spiritForward);
        transform.position = playerCamera.transform.position;

        playerCamera.transform.rotation = originalRotation; // Reset the camera rotation to the original
        playerCamera.transform.position = originalPosition; // Reset the camera position to the original

        isBeingObserved = false; // Now the player is not being observed anymore
    }
    #endregion
}
