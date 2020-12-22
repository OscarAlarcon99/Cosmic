using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayInventory : Singleton<DisplayInventory>
{
    public InventoryObject inventory;
    public GameObject inventoryPrefab;
    public GameObject panel;

    public Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    void Start()
    {
        CreateDisplay();
    }

    public void StateInventory()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

    void Update()
    {
      UpdateDisplay();
    }

    public void UpdateDisplay()
    {

        for (int i = 0; i < inventory.container.items.Count; i++)
        {
            InventorySlot slot = inventory.container.items[i];

            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            }
            else
            {
                GameObject obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[slot.item.id].iconPrefab;
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(inventory.container.items[i], obj);
            }
        }


    }

    public void DeleteDisplay()
    {
        for (int i = 0; i < itemsDisplayed.Count; i++)
        {
            InventorySlot slot = inventory.container.items[i];
            Destroy(itemsDisplayed[slot].gameObject);
        }

        itemsDisplayed.Clear();
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.container.items.Count; i++)
        {
            InventorySlot slot = inventory.container.items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.dataBase.getItem[slot.item.id].iconPrefab;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

            itemsDisplayed.Add(slot, obj);
        }
    }
}
