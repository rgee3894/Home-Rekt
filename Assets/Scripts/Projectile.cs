using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float speed;

    public float fireRate;

    [SerializeField]private GameObject hitParticleEffect;
    

    void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        this.transform.position += transform.right*(speed * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        CollisionAction(col);
        
    }

    protected virtual void CollisionAction(Collision2D col)
    {
        //Debug.Log("Hit object with tag " + col.gameObject.tag);

        if(col.gameObject.tag=="Bullet") return;
        
        if(col.gameObject.tag == "Enemy")
        {
            Debug.Log("Hit enemy!");
            Enemy e=col.gameObject.GetComponent<Enemy>();
            if(e== null)
            {
                Debug.Log("Enemy script is null at " + col.gameObject);
                return;
            }
            e.GetHurt(damage);

        }
        else if(col.gameObject.tag == "Player" && this.gameObject.tag=="EnemyProjectile")
        {
            Debug.Log("Hit player!");
            Player p =col.gameObject.GetComponent<Player>();
            if(p== null)
            {
                Debug.Log("Player script is null at " + col.gameObject);
                return;
            }
            p.GetHurt(damage);

        }
        var instance = Instantiate(hitParticleEffect,this.transform.position,Quaternion.identity);
        instance.SetActive(true);
        this.gameObject.SetActive(false);
        Destroy(instance,hitParticleEffect.GetComponent<ParticleSystem>().main.duration);

    }


}
