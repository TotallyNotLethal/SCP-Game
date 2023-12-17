using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    // Basic Stats
    public float Health { get; private set; }
    public float Stamina { get; private set; }
    public float Speed { get; private set; }
    public float MentalHealth { get; private set; }

    // SCP Exposure and Effects
    public float SCPExposureLevel { get; private set; }
    public bool IsUnderSCPInfluence { get; private set; }

    // Environmental Factors
    public float Temperature { get; private set; }
    public float RadiationLevel { get; private set; }

    // Advanced Stats
    public float Hunger { get; private set; }
    public float Thirst { get; private set; }
    public float Fatigue { get; private set; }
    public List<Injury> Injuries { get; private set; }
    public List<StatusEffect> StatusEffects { get; private set; }
    public float Perception { get; private set; }
    public float MovementSpeed { get; private set; }

    // Constants for limits
    private const float MaxHealth = 100f;
    private const float MaxStamina = 100f;
    private const float MaxMentalHealth = 100f;
    private const float MaxHunger = 100f;
    private const float MaxThirst = 100f;
    private const float MaxFatigue = 100f;

    // Constructor
    public PlayerStats()
    {
        Health = MaxHealth;
        Stamina = MaxStamina;
        MentalHealth = MaxMentalHealth;
        Hunger = MaxHunger;
        Thirst = MaxThirst;
        Fatigue = MaxFatigue;
        Injuries = new List<Injury>();
        StatusEffects = new List<StatusEffect>();

        SCPExposureLevel = 0f;
        IsUnderSCPInfluence = false;
        Temperature = 20f;
        RadiationLevel = 0f;
    }

    // Update Methods
    public void UpdateHealth(float amount)
    {
        Health = Mathf.Clamp(Health + amount, 0, MaxHealth);
        CheckStatThresholds();
    }

    public void UpdateStamina(float amount)
    {
        Stamina = Mathf.Clamp(Stamina + amount, 0, MaxStamina);
        CheckStatThresholds();
    }

    public void UpdateMentalHealth(float amount)
    {
        MentalHealth = Mathf.Clamp(MentalHealth + amount, 0, MaxMentalHealth);
        CheckStatThresholds();
    }

    public void UpdateSCPExposure(float amount)
    {
        SCPExposureLevel = Mathf.Clamp(SCPExposureLevel + amount, 0, MaxHealth);
        IsUnderSCPInfluence = SCPExposureLevel > 0;
        CheckStatThresholds();
    }

    public void UpdateHunger(float amount)
    {
        Hunger = Mathf.Clamp(Hunger + amount, 0, MaxHunger);
        CheckStatThresholds();
    }

    public void UpdateThirst(float amount)
    {
        Thirst = Mathf.Clamp(Thirst + amount, 0, MaxThirst);
        CheckStatThresholds();
    }

    public void UpdateFatigue(float amount)
    {
        Fatigue = Mathf.Clamp(Fatigue + amount, 0, MaxFatigue);
        CheckStatThresholds();
    }

    public void UpdateTemperature(float amount)
    {
        Temperature += amount;
        CheckStatThresholds();
    }

    public void UpdatePerception(float amount)
    {
        Perception += amount;
        Perception = Mathf.Clamp(Perception, 0f, 100f);
    }

    public void UpdateMovementSpeed(float amount)
    {
        MovementSpeed += amount;
        MovementSpeed = Mathf.Clamp(MovementSpeed, 0f, float.MaxValue);
    }

    public void UpdateRadiationLevel(float amount)
    {
        RadiationLevel = Mathf.Max(RadiationLevel + amount, 0);
        CheckStatThresholds();
    }

    private void CheckStatThresholds()
    {
        if (Health < 20) TriggerHealthWarning();
        if (Stamina < 20) Exhaust(5);
        if (MentalHealth < 20) SanityCheck();
    }

    private void TriggerHealthWarning()
    {
    }

    public void RecoverOverTime(float deltaTime)
    {
        UpdateStamina(deltaTime * 0.05f);
        UpdateMentalHealth(deltaTime * 0.03f);
    }

    public void ApplyEnvironmentalEffects(GameEnvironment currentEnvironment)
    {
    }

    public void HandleSCPInfluence(SCP scp)
    {
    }

    public void ConsumeItem(Item item)
    {
    }

    public void Sleep(int hours)
    {
        UpdateFatigue(-hours * 10);
        UpdateMentalHealth(hours * 2);
    }

    public void ApplyRadiationDamage()
    {
        if (RadiationLevel > 5) UpdateHealth(-RadiationLevel * 0.1f);
    }

    public void ApplyHungerEffects()
    {
        if (Hunger < 20) UpdateHealth(-0.1f);
    }

    public void ApplyThirstEffects()
    {
        if (Thirst < 20) UpdateStamina(-0.2f);
    }

    public void ManageTemperatureEffects()
    {
        if (Temperature < 0 || Temperature > 40) UpdateHealth(-0.2f);
    }

    public void ApplyFatigueEffects()
    {
        if (Fatigue > 80) UpdateMentalHealth(-0.3f);
    }

    public void HandleInjury(InjuryType injury)
    {
    }

    public void SanityCheck()
    {
        if (MentalHealth < 20)
        {

        }
    }

    public bool IsStatCritical()
    {
        return Health < 20 || Stamina < 20 || MentalHealth < 20;
    }

    public void Heal(float amount)
    {
        UpdateHealth(amount);
    }

    public void Exhaust(float amount)
    {
        UpdateStamina(-amount);
    }

    public void AddInjury(InjuryType type, float severity)
    {
        Injuries.Add(new Injury(type, severity));
    }

    public void UpdateInjuries(float deltaTime)
    {
        foreach (var injury in Injuries)
        {
            injury.UpdateInjury(deltaTime);
            injury.ApplyEffects(this);
        }
    }

    public void HandleNewInjury(Injury injury)
    {
        switch (injury.Type)
        {
            case InjuryType.None:
                break;

            case InjuryType.Cut:
                AddStatusEffect(StatusEffectType.BloodLoss, 0.2f);
                break;

            case InjuryType.Bruise:
                AddStatusEffect(StatusEffectType.Exhausted, 0.1f);
                break;

            case InjuryType.Fracture:
                AddStatusEffect(StatusEffectType.Limping, 0.3f);
                break;

            case InjuryType.Burn:
                AddStatusEffect(StatusEffectType.Hypothermia, 0.1f);
                break;

            case InjuryType.Concussion:
                AddStatusEffect(StatusEffectType.Dizziness, 0.4f);
                break;

            case InjuryType.Laceration:
                AddStatusEffect(StatusEffectType.BloodLoss, 0.1f);
                break;

            case InjuryType.Poisoning:
                AddStatusEffect(StatusEffectType.Poisoned, 0.5f);
                break;

            case InjuryType.Hypothermia:
                AddStatusEffect(StatusEffectType.Hypothermia, 0.6f);
                break;

            case InjuryType.Heatstroke:
                AddStatusEffect(StatusEffectType.Heatstroke, 0.6f);
                break;
        }
    }


    public void AddStatusEffect(StatusEffectType type, float severity)
    {
        StatusEffects.Add(new StatusEffect(type, severity));
    }

    public void UpdateStatusEffects(float deltaTime)
    {
        foreach (var effect in StatusEffects)
        {
            effect.UpdateEffect(deltaTime, this);
        }
        StatusEffects.RemoveAll(effect => !effect.IsActive);
    }


    public void SetHealth(float newHealth)
    {
        Health = Mathf.Clamp(newHealth, 0, MaxHealth);
    }

    public void SetStamina(float newStamina)
    {
        Stamina = Mathf.Clamp(newStamina, 0, MaxStamina);
    }

    public void SetSpeed(float newSpeed)
    {
        Speed = newSpeed;
    }
}
