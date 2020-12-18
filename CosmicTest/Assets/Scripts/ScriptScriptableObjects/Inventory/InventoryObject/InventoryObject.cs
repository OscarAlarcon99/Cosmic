using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;


[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDataBase dataBase;
    public string savePath;
    public Inventory container;

    [ContextMenu("Save")]
    public void Save()
    {
        ManagerBaseData.Instance.Save(savePath, this);
    }

    [ContextMenu("Clear")]
    public void Clear()
    {
        DisplayInventory.Instance.DeleteDisplay();
        container.items.Clear();
    }

    public void AddItem(Item _itemObject, int _amount)
    {
        if (_itemObject.buffs.Length > 0)
        {
            container.items.Add(new InventorySlot(_itemObject.id, _itemObject, _amount));
            return;
        }

        for (int i = 0; i < container.items.Count; i++)
        {
            if (container.items[i].item.id == _itemObject.id)
            {
                container.items[i].AddAmount(_amount);
                return;
            }
        }

        container.items.Add(new InventorySlot(_itemObject.id, _itemObject, _amount));
    }

    void OnApplicationQuit()
    {
        container.items.Clear();
    }

}


[System.Serializable]
public class Inventory
{
    public List<InventorySlot> items = new List<InventorySlot>();
}



[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int amount;
    public int ID;

    public InventorySlot(int _id, Item _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}



