using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public StatusEffectType Type { get; private set; }
    public float Severity { get; private set; } // Value between 0 and 1, where 1 is the most severe.
    public bool IsActive { get; private set; }
    public float Duration { get; private set; } // Duration the effect has been active.

    // Constructor
    public StatusEffect(StatusEffectType type, float severity)
    {
        Type = type;
        Severity = Mathf.Clamp01(severity);
        IsActive = true;
        Duration = 0f;
    }

    // Update the status effect over time
    public void UpdateEffect(float deltaTime, PlayerStats playerStats)
    {
        if (IsActive)
        {
            Duration += deltaTime;
            ApplyEffect(deltaTime, playerStats);
        }
    }

    // Apply effects of the status effect to the player
    private void ApplyEffect(float deltaTime, PlayerStats playerStats)
    {
        switch (Type)
        {
            case StatusEffectType.BloodLoss:
                playerStats.UpdateHealth(-Severity * deltaTime); // Health depletes over time
                break;
                // ... other cases
        }
    }

    // Method to end the status effect
    public void EndEffect()
    {
        IsActive = false;
    }

    // Get a description of the status effect
    public string GetDescription()
    {
        return $"{Type} - Severity: {Severity}";
    }
}
