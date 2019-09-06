using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject primary;
    public GameObject secondary;

    private Vector3 mousePosition;
    private Vector3 direction;

    private float nextPrimary, nextSecondary;

    private ObjectPooler objectPooler;

    [SerializeField]private Transform [] projectileSpawns;

    [HideInInspector] public int killCount;

    [HideInInspector] public bool canShoot;
    
    void Awake()
    {
        EquipPrimary(this.primary);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        killCount = 0;
        canShoot=true;
        projectileSpawns=this.GetComponentsInChildren<Transform>(false).Where(r=>r.tag=="Projectile Spawn").ToArray();
        objectPooler = this.GetComponent<ObjectPooler>();
        
    }

    private void EquipPrimary(GameObject primary)
    {
        if(primary.GetComponent<Projectile>() == null)
        {
            Debug.LogError(primary.name + " has no Projectile component.");
            return;
        }

        this.primary=primary;
        this.GetComponent<ObjectPooler>().objectToPool=primary;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canShoot) return;
        Aim();

        if(Input.GetMouseButton(0)) Shoot();
        if(Input.GetMouseButton(1)) UseSecondary();
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
        if(Time.time > nextPrimary)
        {

            foreach(Transform ps in projectileSpawns)
            {
                GameObject projectile = objectPooler.GetPooledObject();
                projectile.transform.position = ps.position;
                projectile.transform.rotation = this.transform.rotation;
                projectile.SetActive(true);
                
                projectile.transform.right = this.transform.up;
            }
            
            Projectile attrib = primary.GetComponent<Projectile>();
            
            AudioManager.SharedInstance().PlaySound("Player Shoot");
            nextPrimary = Time.time + attrib.fireRate;
        }

    }

    private void UseSecondary()
    {
        if(Time.time > nextSecondary)
        {
            GameObject secondary = Instantiate(this.secondary,projectileSpawns[0].position,this.transform.rotation);
            secondary.transform.right=this.transform.up;
            Projectile attrib = secondary.GetComponent<Projectile>();
            nextSecondary = Time.time + attrib.fireRate;

        }
        
    }
}
