using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] private GameObject screens;
    private GameWindowSwitch switchScreenTo;

    void Start()
    {
        screens = GameObject.FindGameObjectWithTag("WindowSwitch");
        switchScreenTo = screens.GetComponent<GameWindowSwitch>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("hi");
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null)
            {
                if (playerInventory.HasKey())
                {
                    switchScreenTo.Win();
                    // Destroy(gameObject);
                }
                // If the player doesn't have the required item, do nothing, and the door will remain.
            }
        }
    }
}
