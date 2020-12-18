using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<GroundItem>())
        {
            GroundItem newitem = other.gameObject.GetComponent<GroundItem>();
            Player.Instance.inventory.AddItem(new Item(newitem.item), 1);
            other.gameObject.SetActive(false);
        }
    }
}
