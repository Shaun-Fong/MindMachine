using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    public float MoveSpeed = 1.0f;
    private Animator _anim;

    private bool _underAttack = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0)
        {
            transform.localRotation = Quaternion.Euler(0, x < 0 ? 180 : 0, 0);
        }

        if (_underAttack == false)
        {
            if (x != 0 || y != 0)
            {
                _anim.Play("Walk");
            }
            else
            {
                _anim.Play("Idle");
            }
        }

        Vector3 moveDir = Vector2.right * x * MoveSpeed + Vector2.up * y * MoveSpeed;
        transform.Translate(moveDir * Time.deltaTime, Space.World);

    }

    internal async UniTaskVoid Hit(CancellationToken cancellationToken)
    {
        _underAttack = true;
        _anim.Play("Hit");
        await UniTask.Delay(300, cancellationToken: cancellationToken);
        _underAttack = false;
    }
}
