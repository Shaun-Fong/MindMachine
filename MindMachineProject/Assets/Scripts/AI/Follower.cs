using System.Collections;
using System.Collections.Generic;
using MindMachine;
using UnityEngine;

public class Follower : DinoAI
{

    public float FollowTime = -1;
    private float m_Timer;
    private MindMachineBehavior<Follower> _mindMachine;

    void Start()
    {
        _mindMachine = new MindMachineBehavior<Follower>();
        _mindMachine.Run(this);
    }

    void OnDestroy()
    {
        _mindMachine?.Release();
    }

    internal override void TriggerEnterCallback()
    {
        if (State == DinoStatus.Idle)
        {
            OtherBehavior();
        }
        else
        {
            Idle();
        }
    }

    internal override void TriggerExitCallback()
    {


    }
}
