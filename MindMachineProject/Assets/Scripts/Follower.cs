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

    void Update()
    {
        if (State == DinoStatus.OtherBehavior && m_Timer > 0)
        {
            m_Timer -= Time.deltaTime;
            if (m_Timer <= 0)
            {
                Idle();
            }
        }
    }

    internal override void TriggerEnterCallback()
    {
        OtherBehavior();
        if (FollowTime != -1)
        {
            m_Timer = FollowTime;
        }
    }

    internal override void TriggerExitCallback()
    {


    }
}
