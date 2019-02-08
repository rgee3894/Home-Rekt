using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : CharacterBehavior
{
    public Slider healthBar; 
    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxValue = this.health;
        healthBar.minValue = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = this.health;
        
    }

    public override void Die()
    {
        AudioManager.PlaySound("houseDead");
        Destroy(this.gameObject);
    }
}
