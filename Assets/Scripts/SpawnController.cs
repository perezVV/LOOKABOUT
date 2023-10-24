using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private GameObject key; // Assign key prefab in the Inspector.
    [SerializeField] private GameObject battery; // Assign battery prefab in the Inspector.
    [SerializeField] private GameObject monster; // Assign monster prefab in the Inspector.

    [SerializeField] private Vector2[] spawnLocations;
    
    private HashSet<Vector2> occupiedLocations = new HashSet<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("SpawnObjects");
        
        
    }

    private IEnumerator SpawnObjects()
    {
        yield return new WaitForSeconds(3); // wait until rooms are done spawning
        GameObject[] points = GameObject.FindGameObjectsWithTag("Point");
        spawnLocations = new Vector2[points.Length];
        for (int index = 0; index < points.Length; index++)
        {
            spawnLocations[index] = points[index].transform.position;
        }
        Spawn("key");
        for (int x = 0; x < points.Length / 2; x++)
        {
            Spawn("battery");
        }
        Spawn("monster");
    }

    public bool UnassignLocation(Vector2 location) // return true if found and removed, false otherwise
    {
        return occupiedLocations.Remove(location);
    }

    public Vector2 AssignRandomLocation()
    {
        int randomIndex = Random.Range(0, spawnLocations.Length);
        var spawnLocation = spawnLocations[randomIndex];
        while (!occupiedLocations.Add(spawnLocation)) // already occupied
        {
            randomIndex = Random.Range(0, spawnLocations.Length);
            spawnLocation = spawnLocations[randomIndex];
        }
        return spawnLocation;
    }

    public void Spawn(string obj)
    {
        GameObject spawnObject = monster; // monster is spawned by default
        Vector2 pos = AssignRandomLocation(); // assign a random location
        switch (obj)
        {
            case "key":
                spawnObject = key;
                break;
            case "battery":
                spawnObject = battery;
                break;
            default:
                UnassignLocation(pos); // un-assign monster location just in case monster needs to respawn at same location again
                break;
        }
        Instantiate(spawnObject, pos, Quaternion.identity);
    }
    
}
