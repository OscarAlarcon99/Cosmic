using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemObject : ScriptableObject
{
    public int iD;
    public Sprite iconPrefab;
    public TypeItem type;
    [TextArea(15, 20)]
    public string description;
    public ItemBuff[] buffs;

    public Item createItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string name;
    public int id;
    public ItemBuff[] buffs;

    public Item(ItemObject item)
    {
        name = item.name;
        id = item.iD;
        buffs = new ItemBuff[item.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max)
            {
                attributes = item.buffs[i].attributes
            };
        }
    }
}

[System.Serializable]
public class ItemBuff
{
    public Attributes attributes;
    public int value;
    public int max;
    public int min;

    public ItemBuff(int max, int min)
    {
        this.max = max;
        this.min = min;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = Random.Range(min, max);
    }

}

