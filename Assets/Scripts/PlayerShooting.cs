using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;

    private Vector3 mousePosition;
    private Vector3 direction;

    private float nextFire;

    [SerializeField]private GameObject bulletSpawn;

    [HideInInspector] public int killCount;

    [HideInInspector] public bool canShoot;
    
    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        canShoot=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canShoot) return;
        Aim();

        if(Input.GetMouseButton(0)) Shoot();
    }

    private void Aim()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        direction = new Vector2(mousePosition.x-transform.position.x,mousePosition.y-transform.position.y);

        transform.up = direction;
    }

    private void Shoot()
    {
        if(Time.time > nextFire)
        {
            GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();

            bullet.transform.position = bulletSpawn.transform.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            Bullet attrib = bullet.GetComponent<Bullet>();

            //bullet.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up*attrib.speed*100);
            bullet.transform.up = this.transform.up;
            AudioManager.PlaySound("shoot");
            nextFire = Time.time + attrib.fireRate;
        }
        
        
    }
}
