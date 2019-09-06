using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    Rigidbody2D rb;

    [SerializeField]private GameObject explosionParticleEffect;

    [SerializeField] private float timer;

    private Flasher flasher;

    [SerializeField][Range(0,10)]public float knockbackForce;

    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        flasher = this.GetComponent<Flasher>();
        timer=2f;

        targetPos = this.transform.position+transform.right;

    }

    void Update()
    {
        flasher.Flash();
        
        timer-=Time.deltaTime;
        if(timer<=0) Explode();
        

    }

    /* 
    protected override void Move()
    {

        transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref velocity, 0.3f,speed);

    }
    */

    protected override void CollisionAction(Collision2D col)
    {
        Debug.Log("Hit object with tag yes " + col.gameObject.tag);
        if(col.gameObject.tag=="Bullet" || col.gameObject.tag=="Player") return;

        Explode();

    }

    private void Explode()
    {
        //Release explosion
        var explosionInstance = Instantiate(explosionParticleEffect, this.transform.position,Quaternion.identity);
        
        //Explosion damage = bomb damage   
        explosionInstance.GetComponent<Explosion>().damage=this.damage;
        explosionInstance.GetComponent<Explosion>().knockbackForce=this.knockbackForce;
        
        //Make bomb disappear
        this.gameObject.SetActive(false);
        
        //Destroy explosion after it is done
        Destroy(explosionInstance,explosionInstance.GetComponent<ParticleSystem>().main.duration);

        //Destroy bomb
        Destroy(this.gameObject);

    }
}
