using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject obj;
    public bool keepSpawning = true;
    public float spawnTime;
    public float spawnDelay;

    private GameObject [] spawns;

    private GameObject [] selectedSpawns;
    

    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("Spawns");
        //StartCoroutine(SpawnAtIntervals(spawnDelay)); // Or whatever delay we want.
        InvokeRepeating("MultiSpawn",spawnTime,spawnDelay);
    }

    public void MultiSpawn()
    {
        SelectSpawns();
        Spawn();
        if(!keepSpawning)
        {
            CancelInvoke("MultiSpawn");
        }
    }

    IEnumerator SpawnAtIntervals(float secondsBetweenSpawns)
    {
        // Repeat until keepSpawning == false or this GameObject is disabled/destroyed.
        while(keepSpawning)
        {
            // Put this coroutine to sleep until the next spawn time.
            yield return new WaitForSeconds(secondsBetweenSpawns);

            // Now it's time to spawn again.
            SelectSpawns();
            Spawn();
        }
    }

    private void SelectSpawns()
    {
        int numSpawns = Random.Range(1,spawns.Length);

        selectedSpawns = new GameObject[numSpawns];

        for(int i = 0; i < numSpawns; i++)
        {
            selectedSpawns[i] = spawns[Random.Range(0,spawns.Length-1)];
        }
    }
    
    private void Spawn()
    {
        for(int i = 0; i < selectedSpawns.Length; i++)
        {
            Instantiate(obj,selectedSpawns[i].transform.position,Quaternion.identity);
        }
        
    }
}
