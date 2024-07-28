using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;

public class AvoiderBehavior_Idle : MindMachineBehaviorNode<Avoider>
{

    public override bool Check(Avoider instance)
    {
        return instance.State == DinoAI.DinoStatus.Idle;
    }

    public override async UniTask Tick(Avoider instance)
    {
        await UniTask.Yield();
    }
}
