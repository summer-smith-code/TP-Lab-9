using NUnit.Framework;
using UnityEditor.PackageManager;
using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    // Sets this script to the instance.
    private void Awake()
    {
        SharedInstance = this;
    }

    // On start, make a new object pool and instantiate all pooled objects in advance.
    private void Start()
    {
        pooledObjects = new();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    // Returns a reference to a pooled object.
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    /*
     * TO INSTANTIATE BULLET FROM PLAYER SCRIPT:
     * 
     * GameObject bullet = ObjectPool.SharedInstance.GetPooledObject();
     * if (bullet != null
     * {
     *      bullet.transform.position = (gun/spawning location).transform.position;
     *      bullet.transform.rotation = (gun/spawning location).transform.rotation;
     *      bullet.SetActive(true);
     * }
     */
}
