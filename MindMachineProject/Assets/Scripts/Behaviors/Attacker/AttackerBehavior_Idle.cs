using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;
using System;

public class AttackerBehavior_Idle : MindMachineBehaviorNode<Attacker>
{

    public override bool Check(Attacker instance)
    {
        return instance.State == DinoAI.DinoStatus.Idle;
    }

    public override async UniTask Tick(Attacker instance)
    {
        await UniTask.Delay(300, delayTiming: PlayerLoopTiming.Update, cancellationToken: CancelToken);
        await instance.MoveTo(instance.StartPos, CancelToken);
        instance.Idle();
    }
}
