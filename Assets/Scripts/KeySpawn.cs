using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{
    public GameObject keyPrefab; // Assign the key prefab 
    private bool isCollected = false; // Flag to track if the key has been collected.

    public void SpawnKey()
    {
        if (!isCollected)
        {
            Instantiate(keyPrefab, transform.position, Quaternion.identity);
            isCollected = true; // Mark the key as collected.
        }
    }
}
