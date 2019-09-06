﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;

    private List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    
    void Awake()
    {
        SharedInstance=this;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++) {
            GameObject obj = (GameObject)Instantiate(objectToPool,this.gameObject.transform.parent);
            obj.SetActive(false); 
            pooledObjects.Add(obj);
        }
        
    }

    public GameObject GetPooledObject() {
        //1
        for (int i = 0; i < pooledObjects.Count; i++) {
        //2
            if (!pooledObjects[i].activeInHierarchy) {
            return pooledObjects[i];
            }
        }
        //3   
        return null;
    }

    public void SetPoolObject()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
