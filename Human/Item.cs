using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite itemIcon;
    public ItemType itemType;
    public bool isSCP;
    public string scpNumber;
    public string description;

    public bool isUsable;
    public bool isConsumable;
    public bool isStackable;
    public int quantity;

    public string auraEffect;
    public AudioClip soundEffect;
    public bool isAnimate;
    public string specialProperty;
    public float effectRadius;
    internal int weight;

    public Item(string name, Sprite icon, bool usable, bool stackable, int qty, ItemType type)
    {
        itemName = name;
        itemIcon = icon;
        isSCP = false;
        itemType = type;
        isUsable = usable;
        isStackable = stackable;
        quantity = qty;
    }

    public Item(string scpNum, string name, Sprite icon, string desc, ItemType type = ItemType.SCP)
    {
        scpNumber = scpNum;
        itemName = name;
        itemIcon = icon;
        isSCP = true;
        itemType = type;
        description = desc;
        isUsable = true;
        isStackable = false;
        quantity = 1;
    }

    public virtual void Use()
    {
    }

    public virtual void ActivateEffect()
    {
    }

    public virtual void DeactivateEffect()
    {
    }
}
