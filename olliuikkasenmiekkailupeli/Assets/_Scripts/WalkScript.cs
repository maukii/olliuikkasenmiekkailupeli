using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkScript : MonoBehaviour {

    public float hor, moveSpeed, minHor, minMoveSpeed; //1, 0.75, 0.5, 0.6
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

        if (hor <= -minHor)
        {
            moveSpeed = moveSpeed + 0.05f;
            anim.SetBool("WalkForward", true);

            if (moveSpeed >= defaultMoveSpeed)
            {
                moveSpeed = defaultMoveSpeed;
            }
        }

        if (hor >= minHor)
        {
            moveSpeed = moveSpeed + 0.05f;
            anim.SetBool("WalkBackwards", true);

            if (moveSpeed >= defaultMoveSpeed)
            {
                moveSpeed = defaultMoveSpeed;
            }
        }

        if (hor == 0 || hor > -minHor && hor < minHor)
        {
            moveSpeed = 0;
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
        }

        if (moveSpeed < minMoveSpeed && hor < -minHor || moveSpeed < minMoveSpeed && hor > minHor)
        {
            rb.velocity = Vector3.zero;     //tässä transform.position.x ei saisi muuttua
            rb.angularVelocity = Vector3.zero; //ei tod.näk. tarpeellinen
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
            hor = 0;
        }
    }
}