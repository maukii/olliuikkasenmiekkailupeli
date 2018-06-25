﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement2 : MonoBehaviour
{

    public float hor, ver;
    Animator anim;

    [Header("----- Player Movement Axis Names -----")]
    public string horizontal;
    public string vertical;
    public KeyCode action;
    float attackTimer;

    public float inputX, inputY;
    public float speed = 3f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        hor = Input.GetAxis(horizontal);
        ver = Input.GetAxis(vertical);

        inputX = Mathf.Clamp(inputX, -1, 1);
        inputY = Mathf.Clamp(inputY, -1, 1);

        Move();

    }

    void Move()
    {

        if (hor >= 0.5f && inputX < 1f)
        {
            inputX += speed * Time.deltaTime;
            anim.SetFloat("InputX", inputX);
        }
        if( hor <= -0.5f && inputX > -1f)
        {
            inputX -= speed * Time.deltaTime;
            anim.SetFloat("InputX", inputX);
        }
        

        if (ver >= 0.1f && inputY < 1f)
        {
            inputY += speed * Time.deltaTime;
            anim.SetFloat("InputY", inputY);
        }
        if (ver <= -0.1f && inputY > -1f)
        {
            inputY -= speed * Time.deltaTime;
            anim.SetFloat("InputY", inputY);
        }

        if(hor == 0)
        {
            inputX = Mathf.Lerp(inputX, 0, speed * Time.deltaTime);
            if ((inputX <= 0.02f && inputX > 0) || (inputX >= -0.02f && inputX < 0))
            {
                inputX = 0f;
            }

            anim.SetFloat("InputX", inputX);

        }

        if (ver == 0)
        {
            inputY = Mathf.Lerp(inputY, 0, speed * Time.deltaTime);
            if ((inputY <= 0.02f && inputY > 0) || (inputY >= -0.02f && inputY < 0))
            {
                inputY = 0f;
            }

            anim.SetFloat("InputY", inputY);

        }

        if(Input.GetKey(action))
        {
            anim.SetBool("ActionHold", true);
            attackTimer += Time.deltaTime;
            anim.SetFloat("ActionTimer", attackTimer);
            anim.SetLayerWeight(1, 1f);
        }
        else
        {
            anim.SetBool("ActionHold", false); // TODO: do flip to fast attack
            attackTimer = 0;
            anim.SetFloat("ActionTimer", attackTimer);
        }

    }

}
