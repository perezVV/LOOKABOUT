using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    private Inventory inventory;
    public GameObject itemButton;

    private FlashlightPower flashlight;

    [Header("SFX")] 
    [SerializeField] private AudioClip pickupSound;
    
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<FlashlightPower>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameObject.name == "Battery")
            {
                if (!flashlight.CanPickupBattery())
                {
                    return;
                }
                Debug.Log("battery pickup");
                SFXController.instance.PlaySFX(pickupSound, transform, 0.1f);
                flashlight.PickupBattery();
                Destroy(gameObject);
                return;
            }
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    //check if item can be add into inventory or not
                    inventory.isFull[i] = true;
                    Instantiate(itemButton, inventory.slots[i].transform, false);
                    SFXController.instance.PlaySFX(pickupSound, transform, 0.5f);
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
