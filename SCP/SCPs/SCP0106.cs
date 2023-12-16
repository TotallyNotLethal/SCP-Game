using UnityEngine;
using System.Collections;

public class SCP0106 : SCP
{
    public float CorrosionEffectStrength = 10f;
    public float PhaseThroughWallsDistance = 5f;
    public GameObject corrosionEffectPrefab;
    public Vector3 pocketDimensionLocation;
    private bool isAttackCooldown = false;
    private float attackCooldownTime = 5.0f;
    private float phaseDuration = 10.0f;
    private float attackRadius = 3.0f;
    private float environmentalEffectRadius = 2.0f;
    private float corrosiveDamage = 20f;

    protected override void Awake()
    {
        base.Awake();
        InitializeSCP("SCP-106", "The Old Man", SCPClass.Keter, "A humanoid capable of phasing through solid matter and causing corrosion.");
    }

    protected override void Update()
    {
        base.Update();
        PhaseThroughWalls();
        LeaveCorrosionTrail();
    }

    private void PhaseThroughWalls()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, PhaseThroughWallsDistance))
        {
            if (hit.collider.CompareTag("Wall"))
            {
                transform.position = hit.point + hit.normal * 0.5f;
            }
        }
    }

    private void LeaveCorrosionTrail()
    {
        Instantiate(corrosionEffectPrefab, transform.position, Quaternion.identity);
    }

    private IEnumerator CorrosiveAttack()
    {
        if (!isAttackCooldown)
        {
            isAttackCooldown = true;

            Collider[] hitColliders = Physics.OverlapSphere(transform.position, attackRadius);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    PlayerStats playerStats = hitCollider.gameObject.GetComponent<PlayerStats>();
                    if (playerStats != null)
                    {
                        playerStats.UpdateHealth(-corrosiveDamage);
                    }
                }
            }

            yield return new WaitForSeconds(attackCooldownTime);
            isAttackCooldown = false;
        }
    }

    private IEnumerator EnterAlternateDimension()
    {
        isInAlternateDimension = true;
        gameObject.layer = LayerMask.NameToLayer("NonCollidable");

        yield return new WaitForSeconds(phaseDuration);

        isInAlternateDimension = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void DragPlayerToDimension(GameObject player)
    {
        player.transform.position = pocketDimensionLocation;
        // Implement a timer to return the player back to the original location
    }

    private void CorrodeEnvironment()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, environmentalEffectRadius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Environment"))
            {
                hitCollider.enabled = false;
            }
        }
    }
}
