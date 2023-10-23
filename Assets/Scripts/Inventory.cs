using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public string[] itemNames;

    public bool AddItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!isFull[i])
            {
                isFull[i] = true;
                slots[i] = item;
                return true;
            }
        }
        return false; // Inventory is full.
    }

    public bool HasItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] && slots[i] == item)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] && slots[i] == item)
            {
                isFull[i] = false;
                slots[i] = null;
                return;
            }
        }
    }
}
