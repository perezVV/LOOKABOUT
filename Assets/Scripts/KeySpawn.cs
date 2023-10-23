using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{

    public GameObject keyPrefab; // Assign your key prefab in the Inspector.
    public Transform[] spawnPoints; // Assign your key spawn point prefabs in the Inspector.
    private bool keySpawned = false;

    public GameObject batteryPrefab;
    public Transform[] batteryspawnPoints;
    private bool batterySpawned = false;

    public void SpawnKey()
    {
        if (!keySpawned && spawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform selectedSpawnPoint = spawnPoints[randomIndex];
            Instantiate(keyPrefab, selectedSpawnPoint.position, Quaternion.identity);

            keySpawned = true; // Set the flag to indicate that the key has been spawned.
        }
    }
    public void SpawnBattery()
    {
        if (!batterySpawned && batteryspawnPoints.Length > 0)
        {
            int randomIndex = Random.Range(0, batteryspawnPoints.Length);
            Transform selectedSpawnPoint = batteryspawnPoints[randomIndex];
            Instantiate(batteryPrefab, selectedSpawnPoint.position, Quaternion.identity);

            batterySpawned = true; // Set the flag to indicate that the key has been spawned.
        }
    }

}
