using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySpawn : MonoBehaviour
{

    public GameObject keyPrefab; // Assign your key prefab in the Inspector.
    public Transform[] spawnPoints; // Assign your key spawn point prefabs in the Inspector.
    private bool keySpawned = false;

    public void SpawnKey()
    {
        if (!keySpawned && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedSpawnPoint = spawnPoints[randomIndex];
            Instantiate(keyPrefab, selectedSpawnPoint.position, Quaternion.identity);

            keySpawned = true; // Set the flag to indicate that the key has been spawned.
            Debug.Log("Key spawned");
        }
    }

}
