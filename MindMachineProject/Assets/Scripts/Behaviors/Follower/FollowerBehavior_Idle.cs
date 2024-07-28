using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;
using System;

public class FollowerBehavior_Idle : MindMachineBehaviorNode<Follower>
{

    public override bool Check(Follower instance)
    {
        return instance.State == DinoAI.DinoStatus.Idle;
    }

    public override async UniTask Tick(Follower instance)
    {
        instance.Idle();
        await UniTask.Delay(5000, cancellationToken: CancelToken);
    }
}
