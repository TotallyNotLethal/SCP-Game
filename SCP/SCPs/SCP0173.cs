using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class SCP0173 : SCP
{
    [SerializeField]
    private bool isBeingObserved;
    public float MovementSpeed { get; private set; }
    public Vector3 LastKnownPlayerPosition { get; private set; }
    private float attackCooldown;
    private const float AttackRange = 1f;
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
        MovementSpeed = 10f;
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

        if (angleToPlayer <= 45.0f && distanceToPlayer <= 20.0f)
        {
            RaycastHit hit;
            if (Physics.Raycast(player.transform.position, directionToSCP, out hit, distanceToPlayer))
            {
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
            navAgent.isStopped = true;
            CaughtByObserver();
            MoveSpirit();
        }
        else
        {
            navAgent.isStopped = false;
            FreeFromObserver();
            ActivateEffect();
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
    }

    public void InstantaneousMovement(Vector3 targetPosition)
    {
        if (!isBeingObserved)
        {
            transform.position = targetPosition;
        }
    }

    public void CheckEnvironmentInteraction()
    {
    }

    public void TriggerSpecialEffect()
    {
    }

    public void EmitSound()
    {
    }

    public void ApplyPsychologicalEffect(PlayerStats playerStats)
    {
        playerStats.UpdateMentalHealth(-MentalHealthImpact);
    }

    public void AvoidLightSources()
    {
    }

    public override void EncounterBehavior()
    {
        EmitSound();
    }

    private void MoveSpirit()
    {
        if (isBeingObserved)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

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
        var rotation = playerCamera.transform.rotation;

        Vector3 spiritForward = playerCamera.transform.forward;
        spiritForward.y = 0;
        transform.rotation = Quaternion.LookRotation(spiritForward);
        transform.position = playerCamera.transform.position;

        playerCamera.transform.rotation = originalRotation;
        playerCamera.transform.position = originalPosition;

        isBeingObserved = false;
    }
    #endregion
}
