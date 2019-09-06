using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] protected GameObject [] objectsToSpawn;

    public bool keepSpawning = true;
    [SerializeField]protected float spawnDelay;
    [SerializeField] protected string spawnTag;

    protected GameObject [] spawns;

    protected GameObject [] selectedSpawns;

    protected List<GameObject> spawnedObjects;

    void Start()
    {
        Initialize();
    }

    protected void Initialize()
    {
        spawnedObjects = new List<GameObject>();
        spawns = GameObject.FindGameObjectsWithTag(spawnTag);
        Invoke("MultiSpawn",spawnDelay);

    }


    protected void MultiSpawn()
    {   
        if(spawnedObjects.Count > 0) return;
        SelectSpawns();
        Spawn();
        if(!keepSpawning)
        {
            CancelInvoke("MultiSpawn");
        }
    }

    protected virtual void SelectSpawns()
    {
        int numSpawns = Random.Range(1,spawns.Length);

        selectedSpawns = new GameObject[numSpawns];

        for(int i = 0; i < numSpawns; i++)
        {
            selectedSpawns[i] = spawns[Random.Range(0,spawns.Length)];
        }
    }
    
    protected virtual void Spawn()
    {
        for(int i = 0; i < selectedSpawns.Length; i++)
        {
            spawnedObjects.Add(Instantiate(objectsToSpawn[Random.Range(0,objectsToSpawn.Length)],selectedSpawns[i].transform.position,Quaternion.identity));
        }
        
    }

    public virtual void RemoveSpawnedObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
    }
}
