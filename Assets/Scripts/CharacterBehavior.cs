using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [SerializeField]protected float health;
    [SerializeField]protected float attack;

    [SerializeField] protected float moveSpeed;

    [SerializeField] protected float attackRate;

    private float nextAttack;

    public void Attack(float dmg, GameObject target)
    {
       if(Time.time > nextAttack) 
       {
            CharacterBehavior cb = target.GetComponent<CharacterBehavior>();
            if(cb== null)
            {
                Debug.Log("CharacterBehavior script is null at " + target);
                return;
            }
            cb.GetHurt(dmg);
            nextAttack = Time.time + attackRate;
       }
        

    }

    public void GetHurt(float dmg)
    {
        this.health-=dmg;

        if(this.health <= 0)
        {
            //DIE
            Die();
        }

    }

    public virtual void Die()
    {
        Destroy(this.gameObject);
    }   
    

}
