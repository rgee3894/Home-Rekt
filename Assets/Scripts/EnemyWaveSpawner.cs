using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : Spawner
{

    private static EnemyWaveSpawner instance;

    public static EnemyWaveSpawner SharedInstance() { return instance;} 

    [SerializeField]private EnemyWave[] enemyWaves;

    private EnemyWave currentWave;

    private int waveNum=0;

    [SerializeField]private int minEnemies;
    [SerializeField]private int maxEnemies;

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    

    void Start()
    {
        spawnedObjects = new List<GameObject>();
        spawns = GameObject.FindGameObjectsWithTag(spawnTag);
        SetupWave();
        StartCoroutine(GameManager.SharedInstance().AnnounceWave(waveNum));
        StartCoroutine(SpawnWave(currentWave,spawnDelay));
            
    }

    private IEnumerator SpawnWave(EnemyWave wave, float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        
        foreach(EnemyGroup eg in wave.GetEnemyGroups())
        {
            for(int i = 0; i < eg.GetAmount(); i++)
            {
                GameObject spawnPoint = RandomlySelectSpawn();
                spawnedObjects.Add(Instantiate(eg.GetEnemy(),spawnPoint.transform.position,Quaternion.identity));
            }
        }     
    }

    private GameObject RandomlySelectSpawn() { return spawns[Random.Range(0,spawns.Length)]; }

    public override void RemoveSpawnedObject(GameObject obj)
    {
        spawnedObjects.Remove(obj);
        if(spawnedObjects.Count <= 0) 
        {
            waveNum++;
            SetupWave();
            StartCoroutine(GameManager.SharedInstance().AnnounceWave(waveNum));
            StartCoroutine(SpawnWave(currentWave,spawnDelay));


        }
        
    }

    private void SetupWave()
    {
        
            if(waveNum >= enemyWaves.Length) 
            { 
                currentWave = GenerateRandomWave();
            }
            else 
            { 
                currentWave = enemyWaves[waveNum];
            }
    }

    private EnemyWave GenerateRandomWave()
    {
        EnemyWave randomWave = new EnemyWave();
        
        foreach(GameObject obj in objectsToSpawn)
        {
            EnemyGroup enemyGroup = new EnemyGroup(obj,Random.Range(minEnemies,maxEnemies));
            randomWave.AddEnemyGroup(enemyGroup);
        }

        return randomWave;
    }

}
