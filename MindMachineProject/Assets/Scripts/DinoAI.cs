using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MindMachine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class DinoAI : MonoBehaviour
{
    public enum DinoStatus
    {
        Idle,
        OtherBehavior
    }

    public DinoStatus State { get; private set; } = DinoStatus.Idle;

    public Trigger BehaviorTrigger;
    private Animator _anim;

    private float MoveSpeed = 0.5f;

    void OnEnable()
    {
        BehaviorTrigger.TriggerEnterCallback += TriggerEnterCallback;
        BehaviorTrigger.TriggerExitCallback += TriggerExitCallback;
    }

    void OnDisable()
    {
        BehaviorTrigger.TriggerEnterCallback -= TriggerEnterCallback;
        BehaviorTrigger.TriggerExitCallback -= TriggerExitCallback;
    }

    internal virtual void TriggerEnterCallback()
    {
        Idle();
    }

    internal virtual void TriggerExitCallback()
    {
        OtherBehavior();
    }

    public void Idle()
    {
        State = DinoStatus.Idle;
        GetComponent<Animator>().Play("Idle");
    }

    public void OtherBehavior()
    {
        State = DinoStatus.OtherBehavior;
    }

    public async UniTask MoveTo(Vector3 targetPos, CancellationToken cancelToken)
    {
        float distance = Vector3.Distance(targetPos, transform.position);

        GetComponent<Animator>().Play((distance <= 0.1f) ? "Idle" : "Walk");

        while (distance > 0.1f && cancelToken.IsCancellationRequested == false)
        {
            distance = Vector3.Distance(targetPos, transform.position);

            var dir = targetPos - transform.position;
            var moveVec = dir.normalized * MoveSpeed;

            transform.Translate(moveVec * Time.deltaTime, Space.World);

            transform.localRotation = Quaternion.Euler(0, moveVec.x < 0 ? 180 : 0, 0);

            await UniTask.Yield();
        }

        GetComponent<Animator>().Play((distance <= 0.1f) ? "Idle" : "Walk");
    }
}
