using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public float speed;

    public float fireRate;

    [SerializeField]private GameObject hitParticleEffect;

    

    void Start()
    {

    }

    void FixedUpdate()
    {
        this.transform.position += transform.up*(speed * Time.deltaTime);
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Hit object with tag " + col.gameObject.tag);
        
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
        var instance = Instantiate(hitParticleEffect,this.transform.position,Quaternion.identity);
        this.gameObject.SetActive(false);
        Destroy(instance,hitParticleEffect.GetComponent<ParticleSystem>().main.duration);
        
    }


}
