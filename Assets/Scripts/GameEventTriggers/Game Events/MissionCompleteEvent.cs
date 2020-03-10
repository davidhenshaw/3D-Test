using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MissionCompleteTrigger")]

public class MissionCompleteEvent : GameEvent
{
    MissionManager missionManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        missionManager = FindObjectOfType<MissionManager>();
    }


    public override void TriggerEvent()
    {
        missionManager = MissionManager.GetInstance();
        missionManager.OnMissionComplete();
    }
}
