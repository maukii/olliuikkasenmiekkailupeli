﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour {

    public float hor, moveSpeed;
    float defaultMoveSpeed;
    Animator anim;
    Rigidbody rb;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        defaultMoveSpeed = moveSpeed;
    }

    void Update()
    {
        hor = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(hor * moveSpeed, rb.velocity.y);

        if (hor <= -0.5f)
        {
            moveSpeed = moveSpeed + 0.05f;
            anim.SetBool("WalkForward", true);

            if (moveSpeed >= defaultMoveSpeed)
            {
                moveSpeed = defaultMoveSpeed;
            }
        }

        if (hor >= 0.5f)
        {
            moveSpeed = moveSpeed + 0.05f;
            anim.SetBool("WalkBackwards", true);

            if (moveSpeed >= defaultMoveSpeed)
            {
                moveSpeed = defaultMoveSpeed;
            }
        }

        if (hor == 0 || hor > -0.5f && hor < 0.5f)
        {
            moveSpeed = 0;
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
        }

        if (moveSpeed < 0.5f && hor < -0.5f || moveSpeed < 0.5f && hor > 0.5f)
        {
            rb.velocity = Vector3.zero;     //tässä transform.position.x ei saisi muuttua
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
            hor = 0;
        }
    }
}