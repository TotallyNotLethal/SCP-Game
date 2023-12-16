using UnityEngine;

[System.Serializable]
public class Injury
{
    public InjuryType Type { get; private set; }
    public float Severity { get; private set; } // A value between 0 and 1, where 1 is the most severe.
    public bool IsTreated { get; private set; }
    public float TimeSinceInjury { get; private set; } // Time in seconds since the injury occurred.

    // Constructor
    public Injury(InjuryType type, float severity)
    {
        Type = type;
        Severity = Mathf.Clamp01(severity); // Ensure severity is within 0-1 range.
        IsTreated = false;
        TimeSinceInjury = 0f;
    }

    // Update the injury over time (e.g., called every frame or on a time interval)
    public void UpdateInjury(float deltaTime)
    {
        if (!IsTreated)
        {
            TimeSinceInjury += deltaTime;

            // Logic to worsen the injury over time if untreated
            // For example, an untreated cut could become infected.
            // Severity could increase over time depending on the injury type.
        }
    }

    // Treat the injury
    public void TreatInjury()
    {
        IsTreated = true;
        // Implement logic for treating the injury
        // For example, applying a bandage to a cut.
    }

    // Apply effects of the injury to the player
    public void ApplyEffects(PlayerStats playerStats)
    {
        // Logic to apply the injury's effects on the player's stats
        // For example, a fracture could reduce movement speed.
        switch (Type)
        {
            case InjuryType.Cut:
                // Apply effect of a cut
                break;
            case InjuryType.Fracture:
                // Apply effect of a fracture
                break;
                // Implement cases for other injury types
        }
    }

    // Get a description of the injury
    public string GetDescription()
    {
        switch (Type)
        {
            case InjuryType.Cut:
                return $"Cut - Severity: {Severity}";
            case InjuryType.Fracture:
                return $"Fracture - Severity: {Severity}";
                // Implement cases for other injury types
        }
        return "Unknown Injury";
    }
}
