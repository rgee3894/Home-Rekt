using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using EZCameraShake;

public class Enemy : CharacterBehavior
{
    [SerializeField]protected GameObject target;
    
    [SerializeField] protected GameObject deathParticles;

    [SerializeField] protected SimpleHealthBar healthBar;

    private Rigidbody2D rb;
    
    private bool reachedTarget;

    private TMP_Text killCount;
    private PlayerShooting ps;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Target: " + target);

        reachedTarget = false;

        ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerShooting>();
        killCount = GameObject.Find("Enemy Kill Count").GetComponent<TMP_Text>();
        
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();

        health=maxHealth;
        if(healthBar != null) healthBar.UpdateBar(health,maxHealth);

    }

    // Update is called once per frame
    void Update()
    {
        
        if(target==null) return;
        FaceTarget();

        if(this.gameObject.GetComponent<Collider2D>().IsTouching(target.GetComponent<Collider2D>()))
        {
            //AudioManager.PlaySound("enemyAttack");
            anim.SetBool("isAttacking",true);
            Attack(attack,target);
        }
        else
        {
            reachedTarget=false;
            anim.SetBool("isAttacking",false);
            MoveToTarget();
        } 
    }

    protected void MoveToTarget()
    {
        anim.SetBool("isMoving",true);
        transform.Translate(Vector3.up *moveSpeed* Time.deltaTime);
    }

    protected void FaceTarget()
    {
        Vector2 direction = new Vector2(target.transform.position.x-transform.position.x,target.transform.position.y-transform.position.y);

        transform.up = direction;
    }

    public override void AdditionalHurt()
    {
        if(healthBar != null) healthBar.UpdateBar(health, maxHealth);
    }

    public override void AdditionalDie()
    {
        
        ps.killCount+=1;
        killCount.text = ps.killCount.ToString();
        AudioManager.SharedInstance().PlaySound("Enemy Dead");

        if(deathParticles!=null) {
            var instance = Instantiate(deathParticles,this.transform.position, Quaternion.identity);
            instance.SetActive(true);
            this.gameObject.SetActive(false);
            Destroy(instance,deathParticles.GetComponent<ParticleSystem>().main.duration);
        }

        EnemyWaveSpawner.SharedInstance().RemoveSpawnedObject(this.gameObject);
        //CameraShaker.Instance.ShakeOnce(4,3,0.5f,0.5f);
        

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
