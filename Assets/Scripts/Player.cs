using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : CharacterBehavior
{
    
    [SerializeField] private SimpleHealthBar healthBar;
    
    // Start is called before the first frame update
    void Start()
    {
        health=maxHealth;
        healthBar.UpdateBar(health,maxHealth);
        
    }


    public override void AdditionalHurt()
    {
        healthBar.UpdateBar(health, maxHealth);
    }

    public override void Die()
    {
        AudioManager.SharedInstance().PlaySound("Player Dead");
        Destroy(this.gameObject);
    }
}
