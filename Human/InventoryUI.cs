using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemSlotPrefab;
    public Transform itemSlotContainer;

    private Inventory inventory;

    void Start()
    {
        inventory = new Inventory(8);
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in itemSlotContainer)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in inventory.items)
        {
            GameObject slot = Instantiate(itemSlotPrefab, itemSlotContainer);
        }
    }

    public void AddItemToInventory(Item item)
    {
        inventory.AddItem(item);
        UpdateUI();
    }
}