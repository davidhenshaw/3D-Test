using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionProfile")]
public class MissionProfile : ScriptableObject
{
    [SerializeField] List<GameObject> spawnGroups;
    private Queue<GameObject> spawnQueue = new Queue<GameObject>();
    private GameObject currentGroup;


    private void Awake()
    {

    }

    public void Start()
    {
        foreach (GameObject obj in spawnGroups)
        {
            spawnQueue.Enqueue(obj);
        }
    }
    
    public void SpawnNextGroup()
    {
        currentGroup = spawnQueue.Dequeue();
        Instantiate(currentGroup, Vector3.zero, Quaternion.identity);
    }

}
