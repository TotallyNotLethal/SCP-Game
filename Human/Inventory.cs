using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> items;
    public int capacity;

    public event Action OnInventoryChanged;

    public Inventory(int capacity)
    {
        this.capacity = capacity;
        items = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (items.Count >= capacity)
        {
            Debug.Log("Inventory is full.");
            return false;
        }

        if (item.isStackable)
        {
            Item existingItem = items.Find(i => i.itemName == item.itemName);
            if (existingItem != null)
            {
                existingItem.quantity += item.quantity;
            }
            else
            {
                items.Add(item);
            }
        }
        else
        {
            items.Add(item);
        }

        OnInventoryChanged?.Invoke();
        return true;
    }

    public void RemoveItem(Item item)
    {
        if (item.isStackable && item.quantity > 1)
        {
            item.quantity--;
        }
        else
        {
            items.Remove(item);
        }

        OnInventoryChanged?.Invoke();
    }

    public Item GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    public bool CheckItem(string itemName)
    {
        return items.Exists(item => item.itemName == itemName);
    }

    public List<Item> FindAllOfType(ItemType type)
    {
        return items.FindAll(item => item.itemType == type);
    }

    public void UpdateCapacity(int newCapacity)
    {
        capacity = newCapacity;
    }

    public void ClearInventory()
    {
        items.Clear();
        OnInventoryChanged?.Invoke();
    }

    public void UseItem(string itemName)
    {
        Item item = GetItem(itemName);
        if (item != null)
        {
        }
    }

    public void SortItems()
    {
        items.Sort((item1, item2) => item1.itemName.CompareTo(item2.itemName));
    }

    public float GetTotalWeight()
    {
        float totalWeight = 0;
        foreach (var item in items)
        {
            totalWeight += item.weight * item.quantity;
        }
        return totalWeight;
    }

    public string GetItemsSummary()
    {
        string summary = "";
        foreach (var item in items)
        {
            summary += $"{item.itemName} x{item.quantity}\n";
        }
        return summary;
    }

    public string SaveInventoryState()
    {
        return null;
    }

    public void LoadInventoryState(string savedState)
    {
    }

}
