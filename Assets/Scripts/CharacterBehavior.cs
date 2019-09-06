using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehavior : MonoBehaviour
{
    [SerializeField]protected float health;
    [SerializeField]protected float maxHealth;
    [SerializeField]protected float attack;

    [SerializeField] protected float moveSpeed;

    [SerializeField] protected float attackRate;

    private float nextAttack;

    void Awake()
    {
        health=maxHealth;
    }

    

    public virtual void Attack(float dmg, GameObject target)
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
        if(GetComponent<Flasher>() != null) this.GetComponent<Flasher>().Flash();
        AdditionalHurt();
        if(this.health <= 0)
        {
            //DIE
            Die();
        }

    }

    public virtual void AdditionalHurt() {}
    public virtual void AdditionalDie() {}
    public virtual void Die()
    {   
        AdditionalDie();
        Destroy(this.gameObject);
    }
    
       
    

}
