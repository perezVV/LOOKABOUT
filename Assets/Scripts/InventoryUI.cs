using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to use Text component.

public class InventoryUI : MonoBehaviour
{
    public Text[] itemTexts; // Assign your UI Text elements for item names in the Inspector.
    public Inventory inventory; // Reference to your Inventory script.

    // Call this method whenever the inventory changes to update the displayed item names.
    public void UpdateItemNames()
    {
        for (int i = 0; i < inventory.itemNames.Length; i++)
        {
            itemTexts[i].text = inventory.itemNames[i];
        }
    }
}
