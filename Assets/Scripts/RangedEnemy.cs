using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform [] projectileSpawns;

    private float nextShoot;

    private ObjectPooler objectPooler;
    
    // Start is called before the first frame update
    void Start()
    {
        objectPooler = this.GetComponent<ObjectPooler>();

    }

    // Update is called once per frame
    void Update()
    {
        if(target==null) return;
        Shoot();
        
    }

    private void Shoot()
    {
        if(Time.time > nextShoot)
        {

            foreach(Transform ps in projectileSpawns)
            {
                GameObject projectile = objectPooler.GetPooledObject();
                projectile.transform.position = ps.position;
                projectile.transform.rotation = this.transform.rotation;
                projectile.SetActive(true);
                
                projectile.transform.right = this.transform.up;
            }
            
            Projectile attrib = pooler.objectToPool.GetComponent<Projectile>();
            
            AudioManager.SharedInstance().PlaySound("Player Shoot");
            nextShoot = Time.time + attrib.fireRate;
        }

    }
}
