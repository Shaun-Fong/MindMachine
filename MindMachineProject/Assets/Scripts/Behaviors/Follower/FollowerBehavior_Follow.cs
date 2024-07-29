using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;

public class FollowerBehavior_Follow : MindMachineBehaviorNode<Follower>
{

    public override bool Check(Follower instance)
    {
        return instance.State == DinoAI.DinoStatus.OtherBehavior;
    }

    public override async UniTask Tick(Follower instance)
    {
        await UniTask.Delay(1000);

        var dir = PlayerController.Instance.transform.position - instance.transform.position;
        var dist = Vector3.Distance(PlayerController.Instance.transform.position, instance.transform.position);
        Vector3 targetPos = dir.normalized * (dist - 0.4f) + PlayerController.Instance.transform.position;

        if (Vector3.Distance(instance.transform.position, targetPos) > 0.1f)
        {
            await instance.ShowMark(CancelToken);
            await instance.MoveTo(targetPos, CancelToken).AttachExternalCancellation(CancelToken);
        }

    }
}
