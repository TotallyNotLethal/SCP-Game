using UnityEngine;

[System.Serializable]
public class Injury
{
    public InjuryType Type { get; private set; }
    public float Severity { get; private set; }
    public bool IsTreated { get; private set; }
    public float TimeSinceInjury { get; private set; }

    public Injury(InjuryType type, float severity)
    {
        Type = type;
        Severity = Mathf.Clamp01(severity);
        IsTreated = false;
        TimeSinceInjury = 0f;
    }

    public void UpdateInjury(float deltaTime)
    {
        if (!IsTreated)
        {
            TimeSinceInjury += deltaTime;
        }
    }

    public void TreatInjury()
    {
        IsTreated = true;
    }

    public void ApplyEffects(PlayerStats playerStats)
    {
        switch (Type)
        {
            case InjuryType.None:
                break;

            case InjuryType.Cut:
                playerStats.UpdateHealth(-Severity * 0.1f);
                break;

            case InjuryType.Bruise:
                playerStats.UpdateStamina(-Severity * 0.05f);
                break;

            case InjuryType.Fracture:
                playerStats.UpdateMovementSpeed(-Severity * 0.2f);
                break;

            case InjuryType.Burn:
                playerStats.UpdateHealth(-Severity * 0.2f);
                playerStats.UpdateStamina(-Severity * 0.1f);
                break;

            case InjuryType.Concussion:
                playerStats.UpdatePerception(-Severity * 0.2f);
                break;

            case InjuryType.Laceration:
                playerStats.UpdateHealth(-Severity * 0.15f);
                break;

            case InjuryType.Poisoning:
                playerStats.UpdateHealth(-Severity * 0.2f);
                playerStats.UpdateStamina(-Severity * 0.2f);
                break;

            case InjuryType.Hypothermia:
                playerStats.UpdateTemperature(-Severity * 0.2f);
                break;

            case InjuryType.Heatstroke:
                playerStats.UpdateTemperature(Severity * 0.2f);
                playerStats.UpdateStamina(-Severity * 0.2f);
                break;
        }
    }

    public string GetDescription()
    {
        switch (Type)
        {
            case InjuryType.None:
                return "No Injury";

            case InjuryType.Cut:
                return $"Cut - Severity: {Severity}";

            case InjuryType.Bruise:
                return $"Bruise - Severity: {Severity}";

            case InjuryType.Fracture:
                return $"Fracture - Severity: {Severity}";

            case InjuryType.Burn:
                return $"Burn - Severity: {Severity}";

            case InjuryType.Concussion:
                return $"Concussion - Severity: {Severity}";

            case InjuryType.Laceration:
                return $"Laceration - Severity: {Severity}";

            case InjuryType.Poisoning:
                return $"Poisoning - Severity: {Severity}";

            case InjuryType.Hypothermia:
                return $"Hypothermia - Severity: {Severity}";

            case InjuryType.Heatstroke:
                return $"Heatstroke - Severity: {Severity}";
        }
        return "Unknown Injury";
    }
}
