using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMovement : MonoBehaviour {

    public float hor, ver;

    Vector2 startPosition;
    Animator anim;

    // this way both players can use same script
    [Header("----- Player Movement Axis Names -----")]
    public string horizontal;
    public string vertical;
    //public string jump;

    void Start()
    {
        anim = GetComponent<Animator>();
        startPosition = transform.position;
    }

    void Update()
    {
        hor = Input.GetAxis(horizontal);
        ver = Input.GetAxis(vertical);

        Move();
    }

    void Move()
    {
        if (hor >= 0.1f)
        {
            // move right
            //anim.SetBool("WalkForward", true);
            //anim.SetBool("WalkBackwards", false);
            anim.SetFloat("InputX", hor);
        }
        else if(hor <= -0.1f)
        {
            // move left
            //anim.SetBool("WalkBackwards", true);
            //anim.SetBool("WalkForward", false);
            anim.SetFloat("InputX", hor);
        }

        if(ver >= 0.1f)
        {
            anim.SetFloat("InputY", ver);           
        }
        else if(ver <= -0.1f)
        {
            anim.SetFloat("InputY", ver);
        }

        if(hor == 0 && ver == 0)
        {
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
            anim.SetFloat("InputY", 0);
            anim.SetFloat("InputX", 0);
        }
    }

}
