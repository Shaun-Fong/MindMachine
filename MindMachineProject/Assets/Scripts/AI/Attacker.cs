using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using MindMachine;
using UnityEngine;

public class Attacker : DinoAI
{
    private MindMachineBehavior<Attacker> _mindMachine;

    void Start()
    {
        _mindMachine = new MindMachineBehavior<Attacker>();
        _mindMachine.Run(this);
    }

    void OnDestroy()
    {
        _mindMachine?.Release();
    }

    internal override void TriggerEnterCallback()
    {
        OtherBehavior();
    }

    internal override void TriggerExitCallback()
    {
        Idle();
    }

    public async UniTask Attack(PlayerController player, CancellationToken cancelToken)
    {
        await UniTask.Delay(300).AttachExternalCancellation(cancelToken);
        MainAnim.Play("Kick");
        await UniTask.Delay(300).AttachExternalCancellation(cancelToken);
        var dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist < 0.1f)
        {
            player.Hit(cancelToken).Forget();
        }
        MainAnim.Play("Idle");
    }
}
