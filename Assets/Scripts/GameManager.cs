using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Spawn spawn;
    public Spawn batterySpawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn.SpawnKey(); // Call the SpawnKey method.
        spawn.SpawnBattery(); // Call the SpawnBattery method.
        //spawn.SpawnMonster(); // Call the SpawnMonster method.
    }
}
