using UnityEngine;

public abstract class Player : MonoBehaviour
{
    public string Name { get; private set; }
    public PlayerStats Stats { get; private set; }
    public Inventory Inventory { get; private set; }

    protected Player(string name)
    {
        Name = name;
        Stats = new PlayerStats();
        Inventory = new Inventory(10);
    }

    public abstract void Move();
    public abstract void Attack();
    public abstract void Interact();

    public void AddItemToInventory(Item item)
    {
        Inventory.AddItem(item);
    }

    public void ReceiveInjury(Injury injury)
    {
        Stats.HandleNewInjury(injury);
    }

    public void Update()
    {
        Stats.UpdateStatusEffects(deltaTime);
        Stats.UpdateInjuries(deltaTime);
    }

    // Additional methods
    public void UseItem(string itemName)
    {
    }

    public void EquipItem(string itemName)
    {
    }

    public void DropItem(string itemName)
    {
    }

    public void Heal(float amount)
    {
        Stats.UpdateHealth(amount);
    }

    public void RestoreStamina(float amount)
    {
        Stats.UpdateStamina(amount);
    }

    public void ChangeSpeed(float newSpeed)
    {
        Stats.SetSpeed(newSpeed);
    }

    public void ApplyStatusEffect(StatusEffectType effectType, float severity)
    {
    }

    public void ClearStatusEffects()
    {
    }

    public abstract void PerformRoleSpecificAction();

}
