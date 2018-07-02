using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement4 : MonoBehaviour
{

    public float hor, ver;
    Animator anim;

    [Header("----- Player Movement Axis Names -----")]
    public string horizontal;
    public string vertical;
    public KeyCode action;
    public float attackTimer;

    public float inputX, inputY;
    public float speed = 3f;

    [Header("--- Inputs ---")]
    public bool forward;
    public bool back;
    // up, down

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

        if(Input.GetAxisRaw(horizontal) == -1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }

        if(Input.GetAxisRaw(horizontal) == 1)
        {
            back = true;
        }
        else
        {
            back = false;
        }

        anim.SetFloat("InputX", -hor);
        anim.SetBool("forward", forward);
        anim.SetBool("back", back);

        //if (hor >= 0.5f && inputX < 1f)
        //{
        //    inputX += speed * Time.deltaTime;
        //    anim.SetFloat("InputX", inputX);
        //}
        //if (hor <= -0.5f && inputX > -1f)
        //{
        //    inputX -= speed * Time.deltaTime;
        //    anim.SetFloat("InputX", inputX);
        //}
        //
        //if (hor == 0)
        //{
        //    inputX = Mathf.Lerp(inputX, 0, speed * Time.deltaTime);
        //    if ((inputX <= 0.3f && inputX > 0) || (inputX >= -0.3f && inputX < 0))
        //    {
        //        inputX = 0f;
        //    }
        //
        //    anim.SetFloat("InputX", inputX);
        //
        //}

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

        if (ver == 0)
        {
            inputY = Mathf.Lerp(inputY, 0, speed * Time.deltaTime);
            if ((inputY <= 0.3f && inputY > 0) || (inputY >= -0.3f && inputY < 0))
            {
                inputY = 0f;
            }

            anim.SetFloat("InputY", inputY);

        }

    }

}
