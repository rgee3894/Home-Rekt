using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Explosion : MonoBehaviour
{
    public float damage;

    [SerializeField]private float explosionShakeDuration=0.5f;

    [SerializeField]private float explosionShakeMagnitude=4f;

    public float knockbackForce;

    private float explosionRadius;
    
    // Start is called before the first frame update
    void Start()
    {
        explosionRadius = this.GetComponent<CircleCollider2D>().radius;

        Collider2D [] cols = Physics2D.OverlapCircleAll(this.transform.position, explosionRadius);

        foreach(Collider2D col in cols)
        {
            if(col.tag=="Enemy")
            {
                Enemy e=col.gameObject.GetComponent<Enemy>();
                if(e== null)
                {
                    Debug.Log("Enemy script is null at " + col.gameObject);
                    return;
                }
                e.GetHurt(damage);
                //Knockback(col);
            }
        }

        AudioManager.SharedInstance().PlaySound("Explosion");
        //CameraShaker.Instance.ShakeOnce(explosionShakeMagnitude,5,explosionShakeDuration,explosionShakeDuration*2);
        
    }

    private void Knockback(Collider2D col)
    {
        //Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);
        Vector3 dir = this.transform.position-col.gameObject.transform.position;

        dir=-dir.normalized;
        

        col.gameObject.GetComponent<Rigidbody2D>().AddForce(dir*knockbackForce);

        

    }
}
