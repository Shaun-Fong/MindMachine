using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController Instance { get; private set; }

    public float MoveSpeed = 1.0f;
    private Animator _anim;

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

        if (x != 0 || y != 0)
        {
            _anim.Play("Walk");
        }
        else
        {
            _anim.Play("Idle");
        }

        Vector3 moveDir = Vector2.right * x * MoveSpeed + Vector2.up * y * MoveSpeed;
        transform.Translate(moveDir * Time.deltaTime, Space.World);

    }
}
