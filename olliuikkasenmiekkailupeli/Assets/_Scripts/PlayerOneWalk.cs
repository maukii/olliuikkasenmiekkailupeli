using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWalk : MonoBehaviour {

    public float horOne, timer, deadZone;
    float defaultTimer;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        defaultTimer = timer;
    }

    void Update()
    {
        //horOne = Input.GetAxis("Horizontal");
        horOne = Input.GetAxis("P1_horizontal");

        if (horOne > deadZone)
        {
            anim.SetBool("WalkForward", true);
        }

        if (horOne < -deadZone)
        {
            anim.SetBool("WalkBackwards", true);
        }

        if (horOne == 0 || horOne < deadZone && horOne > -deadZone)
        {
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
        }
    }

    /* >>>>>>>>>>>>>>>>>>TÄMÄ SILLOIN KUN ON USEAMPI ANIMAATIO<<<<<<<<<<<<<<<<<<
    
    void Move()
    {
        if (horOne < 0) //0 - -1
        {
            //timer = timer - jotain
            //askel-boolit nollataan
            //aloita askel-animaatio

            if (timer > 0)
            {
                //jatka lyhyt askel-animaatio
                timer = defaultTimer;
            }

            if (timer <= 0)
            {
                //jatka pitkä askel-animaatioon
                timer = defaultTimer;
            }
        }

        if (horOne > 0) //0 - 1
        {
            //timer = timer - jotain
            //askel-boolit nollataan
            //aloita askel-animaatio

            if (timer > 0)
            {
                //jatka lyhyt askel-animaatio
                timer = defaultTimer;
            }

            if (timer <= 0)
            {
                //jatka pitkä askel-animaatioon
                timer = defaultTimer;
            }
        }

        if (horOne == 0)
        {
            //animaatiot falseksi
        }

        if (horTwo < 0) //0 - -1
        {
            //timer = timer - jotain
            //askel-boolit nollataan
            //aloita askel-animaatio

            if (timer > 0)
            {
                //jatka lyhyt askel-animaatio
                timer = defaultTimer;
            }

            if (timer <= 0)
            {
                //jatka pitkä askel-animaatioon
                timer = defaultTimer;
            }
        }

        if (horTwo > 0) //0 - 1
        {
            //timer = timer - jotain
            //askel-boolit nollataan
            //aloita askel-animaatio

            if (timer > 0)
            {
                //jatka lyhyt askel-animaatio
                timer = defaultTimer;
            }

            if (timer <= 0)
            {
                //jatka pitkä askel-animaatioon
                timer = defaultTimer;
            }
        }

        if (horTwo == 0)
        {
            //animaatiot falseksi
        }
    }
    */












    /* OLD RIBALS
    public float hor, moveSpeed, minHor, minMoveSpeed; //1, 0.75, 0.5, 0.6
    float defaultMoveSpeed;
    defaultMoveSpeed = moveSpeed;
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
        rb.Sleep();
        anim.SetBool("WalkForward", false);
        anim.SetBool("WalkBackwards", false);
        hor = 0;
    }
    */
}