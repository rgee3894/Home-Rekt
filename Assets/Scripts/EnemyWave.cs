using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [SerializeField] private List<EnemyGroup> enemyGroups;

    public EnemyWave()
    {
        enemyGroups = new List<EnemyGroup>();
    }
    
    public List<EnemyGroup> GetEnemyGroups() { return enemyGroups;}

    public void AddEnemyGroup(EnemyGroup group) { enemyGroups.Add(group);}
    public void SetEnemyGroups(List<EnemyGroup> groups){ enemyGroups=groups;}
}