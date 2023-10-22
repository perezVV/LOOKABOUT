using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    public GameObject itemButton;

    [Header("SFX")] 
    [SerializeField] private AudioClip pickupKey;
    
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //check if item can be add into inventory or not
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    SFXController.instance.PlaySFX(pickupKey, transform, 0.5f);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
