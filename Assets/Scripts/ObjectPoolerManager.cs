using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolerManager : MonoBehaviour
{
    public static ObjectPoolerManager SharedInstance;

    private List<ObjectPooler> objectPoolers;
    
    void Awake()
    {
        SharedInstance=this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
