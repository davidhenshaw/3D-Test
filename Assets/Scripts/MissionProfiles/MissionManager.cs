using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    private static MissionManager instance;
    [SerializeField] List<MissionProfile> missions;
    int currentMission = 0;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCurrentMission();
    }

    // Update is called once per frame

    void Update()
    {

    }

    public static MissionManager GetInstance()
    {
        return instance;
    }

    public void OnMissionComplete()
    {
        Debug.Log("**Mission " + missions[currentMission].name + " COMPREE**");
        LoadNextMission();

    }

    public void SpawnNextGroup()
    {
        missions[currentMission].SpawnNextGroup();
    }

    void LoadNextMission()
    {
        if (currentMission < missions.Count - 1)
        {
            currentMission++;
            StartCurrentMission();
        }
        else
        {
            Debug.LogError("Tried to load another mission but there are none left");
        }
    }

    public void StartCurrentMission()
    {
        missions[currentMission].Start();
        missions[currentMission].SpawnNextGroup();
    }
}
