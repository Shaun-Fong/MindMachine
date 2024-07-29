using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using MindMachine;

public class AttackerBehavior_Attack : MindMachineBehaviorNode<Attacker>
{

    public override bool Check(Attacker instance)
    {
        return instance.State == DinoAI.DinoStatus.OtherBehavior;
    }

    public override async UniTask Tick(Attacker instance)
    {
        await UniTask.Delay(1000);
        await instance.ShowMark(CancelToken);
        await instance.MoveTo(PlayerController.Instance.transform.position, CancelToken).AttachExternalCancellation(CancelToken);
        await instance.Attack(PlayerController.Instance, CancelToken);

    }
}
