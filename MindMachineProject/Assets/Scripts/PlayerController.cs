using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float MoveSpeed = 1.0f;
    private Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (x > 0)
        {
            transform.localRotation = Quaternion.identity;
        }

        if (x != 0 || y != 0)
        {
            _anim.Play("Walk");
        }
        else
        {
            _anim.Play("Idle");
        }

        transform.Translate(Vector2.right * x * MoveSpeed + Vector2.up * y * MoveSpeed, Space.World);

    }
}
