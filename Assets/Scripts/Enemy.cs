using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : CharacterBehavior
{
    [SerializeField]private GameObject target;

    private Rigidbody2D rb;
    
    private bool reachedTarget;

    private TMP_Text killCount;
    private PlayerShooting ps;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target");
        Debug.Log("Target: " + target);

        rb = this.GetComponent<Rigidbody2D>();

        reachedTarget = false;

        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        killCount = GameObject.Find("Enemy Kill Count").GetComponent<TMP_Text>();


        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(target==null) return;
        FaceTarget();

        if(this.gameObject.GetComponent<Collider2D>().IsTouching(target.GetComponent<Collider2D>()))
        {
            //AudioManager.PlaySound("enemyAttack");
            Attack(attack,target);
        }
        else
        {
            reachedTarget=false;
            MoveToTarget();
        } 
    }

    private void MoveToTarget()
    {
        transform.Translate(Vector3.up *moveSpeed* Time.deltaTime);
    }

    private void FaceTarget()
    {
        Vector2 direction = new Vector2(target.transform.position.x-transform.position.x,target.transform.position.y-transform.position.y);

        transform.up = direction;
    }

    public override void Die()
    {
        
        ps.killCount+=1;
        killCount.text = ps.killCount.ToString();
        AudioManager.PlaySound("enemyDead");
        Destroy(this.gameObject);

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Target")
        {
            reachedTarget = true;
            Debug.Log("Reached target!");
            
        }
    }
}
