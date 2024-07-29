using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;

public class AvoiderBehavior_Avoid : MindMachineBehaviorNode<Avoider>
{

    public override bool Check(Avoider instance)
    {
        return instance.State == DinoAI.DinoStatus.OtherBehavior;
    }

    public override async UniTask Tick(Avoider instance)
    {
        await UniTask.NextFrame();
        await instance.ShowMark(CancelToken);
        await instance.MoveTo(Random.insideUnitCircle.normalized * 0.45f + instance.StartPos, CancelToken);
        await UniTask.WaitUntil(()=>instance.State == DinoAI.DinoStatus.Idle);
    }
}
