using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpawner : MonoBehaviour
{
    [SerializeField] GameObject icePrefab;
    static List<GameObject> instances;
    
    // Start is called before the first frame update
    void Start()
    {
        instances = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if(instances.Count >= 3)
            {
                DestroyAllInstances();
            }
            else
            {
                Spawn();
            }
        }
    }

    public void DestroyAllInstances()
    {
        foreach(GameObject obj in instances)
        {
            Destroy(obj);
        }
        instances.Clear();
    }

    void Spawn()
    {
        float spawnForce = 5f;
        var newIceObj = Instantiate(icePrefab, transform.position, transform.rotation) as GameObject;

        newIceObj.GetComponent<Rigidbody>().AddForce(transform.forward*spawnForce, ForceMode.VelocityChange);

        instances.Add(newIceObj);
    }
}
