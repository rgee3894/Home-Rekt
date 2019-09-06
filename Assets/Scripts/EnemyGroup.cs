using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyGroup
{
    [SerializeField] private GameObject enemy;
    [SerializeField] private int amount;

    public EnemyGroup(GameObject enemy, int amount)
    {
        this.enemy=enemy;
        this.amount=amount;
    }
    
    public GameObject GetEnemy() {return enemy;}
    public int GetAmount() {return amount;}
}
