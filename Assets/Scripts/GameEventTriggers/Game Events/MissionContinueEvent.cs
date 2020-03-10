using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MissionContinueTrigger")]

public class MissionContinueEvent : GameEvent
{
    MissionManager missionManager;

    //private void OnEnable()
    //{
    //    missionManager = FindObjectOfType<MissionManager>();
    //}

    public override void TriggerEvent()
    {
        missionManager = MissionManager.GetInstance();
        missionManager.SpawnNextGroup();
    }
}
