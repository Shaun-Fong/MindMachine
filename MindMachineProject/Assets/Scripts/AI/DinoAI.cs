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
    public Animator MarkAnim;

    public Animator MainAnim { get; private set; }

    private float MoveSpeed = 0.5f;

    public Vector2 StartPos { get; private set; }
    public Vector3 TargetPos { get; private set; }
    private bool _moving = false;

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

    void Awake()
    {
        MainAnim = GetComponent<Animator>();
        MarkAnim.gameObject.SetActive(false);
        StartPos = transform.position;
        TargetPos = StartPos;
        _moving = false;
    }

    void Update()
    {
        var distance = Vector3.Distance(TargetPos, transform.position);

        if (distance > 0.1f)
        {
            _moving = true;
            var dir = TargetPos - transform.position;
            var moveVec = dir.normalized * MoveSpeed;

            transform.Translate(moveVec * Time.deltaTime, Space.World);

            transform.localRotation = Quaternion.Euler(0, moveVec.x < 0 ? 180 : 0, 0);
        }
        else
        {
            _moving = false;
        }
    }

    internal virtual void TriggerEnterCallback()
    {
        OtherBehavior();
    }

    internal virtual void TriggerExitCallback()
    {
        Idle();
    }

    public void Idle()
    {
        State = DinoStatus.Idle;
        MainAnim.Play("Idle");
        MarkAnim.gameObject.SetActive(false);
    }

    public void OtherBehavior()
    {
        State = DinoStatus.OtherBehavior;
    }

    public async UniTask MoveTo(Vector3 targetPos, CancellationToken cancelToken)
    {
        TargetPos = targetPos;

        await UniTask.NextFrame();

        MainAnim.Play("Walk");
        await UniTask.WaitUntil(() => _moving == false).AttachExternalCancellation(cancelToken);
        MainAnim.Play("Idle");
    }

    public async UniTask ShowMark(CancellationToken cancelToken)
    {
        MarkAnim.gameObject.SetActive(false);
        MarkAnim.gameObject.SetActive(true);

        await UniTask.Delay(1300).AttachExternalCancellation(cancelToken);

        MarkAnim.gameObject.SetActive(false);
    }
}
