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

            case StatusEffectType.Pestilence: // SCP-049
                                              // playerStats.UpdateHealth(-Severity * deltaTime * 1.5f);
                break;

            case StatusEffectType.Starving: // SCP-101
                                            // playerStats.UpdateHunger(-Severity * deltaTime * 2f);
                break;

            case StatusEffectType.DimensionalShift: // SCP-137
                                                    // playerStats.TeleportToAlternateDimension();
                break;

            case StatusEffectType.Infatuation: // SCP-166
                                               // playerStats.UpdateConfusion(Severity * deltaTime * 0.5f);
                break;

            case StatusEffectType.Foreboding: // SCP-295
                                              // playerStats.UpdateMorale(-Severity * deltaTime * 0.8f);
                break;

            case StatusEffectType.Consumed: // SCP-357
                                            // playerStats.Disappear();
                break;

            case StatusEffectType.TemporalDisplacement: // SCP-460
                                                        // playerStats.TimeTravel(Severity * deltaTime * 10f);
                break;

            case StatusEffectType.Frustration: // SCP-523
                                               // playerStats.UpdateFrustration(Severity * deltaTime * 0.7f);
                break;

            case StatusEffectType.InterdimensionalConnection: // SCP-527
                                                              //playerStats.SwitchDimension(Type);
                break;

            case StatusEffectType.UnintendedConsequences: // SCP-668
                                                          // playerStats.ApplyUnintendedEffects(Severity * deltaTime * 1.2f);
                break;
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
