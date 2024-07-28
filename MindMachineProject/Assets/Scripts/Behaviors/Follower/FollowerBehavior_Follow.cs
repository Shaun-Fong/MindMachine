using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;
using System;

public class FollowerBehavior_Follow : MindMachineBehaviorNode<Follower>
{

    public override bool Check(Follower instance)
    {
        return instance.State == DinoAI.DinoStatus.OtherBehavior;
    }

    public override async UniTask Tick(Follower instance)
    {
        Vector3 targetPos = Vector3.zero;
        if (PlayerController.Instance != null)
        {
            targetPos = PlayerController.Instance.transform.position;
        }
        await instance.MoveTo(targetPos, CancelToken).AttachExternalCancellation(CancelToken);
    }
}
