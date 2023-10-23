using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;
    public string[] itemNames;

    public void AddKey(Key key)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (!isFull[i])
                {
                    isFull[i] = true;
                    itemNames[i] = key.keyName;
                    return;
                }
            }
        }
}
