using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class ExitDoor : MonoBehaviour
{
    public GameObject requiredItem; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                if (playerInventory.HasItem(requiredItem))
                {
                    // Player has the required item, so destroy the door and remove the item from the inventory.
                    playerInventory.RemoveItem(requiredItem);
                    Destroy(gameObject);
                }
                // If the player doesn't have the required item, do nothing, and the door will remain.
            }
        }
    }
}
*/