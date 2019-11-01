using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPool : MonoBehaviour
{
    public GameObject prefabPickup;
    public List<GameObject> pooledPickups;
    public int poolsize = 20;
    bool canGrow = true;

    private void Awake()
    {
        pooledPickups = new List<GameObject>();
        for (int i = 0;i<poolsize;i++)
        {
            GameObject pickup = Instantiate(prefabPickup);
            pickup.SetActive(false);
            pickup.transform.SetParent(transform);
            pooledPickups.Add(pickup);
        }
    }

    public GameObject getPooledPickup()
    {
        foreach(GameObject pp in pooledPickups)
        {
            if(!pp.activeInHierarchy)
            {
                return pp;
            }
        }
        if(canGrow)
        {
            GameObject pickup = Instantiate(prefabPickup);
            pickup.SetActive(true);
            pickup.transform.SetParent(transform);
            //pickup.transform.position = 
            pooledPickups.Add(pickup);
            return pickup;
        }
        return null;
    }

    void Update()
    {
        
    }
}
