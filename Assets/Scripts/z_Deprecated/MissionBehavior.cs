using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MissionBehavior : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnGroups;
    private Queue<GameObject> spawnQueue;
    MissionManager missionControl;

    public void Start()
    {
        foreach(GameObject obj in spawnGroups)
        {
            spawnQueue.Enqueue(obj);
        }
        missionControl = FindObjectOfType<MissionManager>();
    }

    public void SpawnNextGroup()
    {
        GameObject group = spawnQueue.Dequeue();
        Instantiate(group, Vector3.zero, Quaternion.identity);
    }
}
