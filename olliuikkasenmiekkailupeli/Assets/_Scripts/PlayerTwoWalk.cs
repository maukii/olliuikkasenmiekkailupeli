using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwoWalk : MonoBehaviour {

    public float horTwo, timer, deadZone;
    private float defaultTimer;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        defaultTimer = timer;
    }

    void Update()
    {
        //horOne = Input.GetAxis("Horizontal");
        horTwo = Input.GetAxis("P2_horizontal");

        if (horTwo > deadZone)
        {
            anim.SetBool("WalkBackwards", true);
        }

        if (horTwo < -deadZone)
        {
            anim.SetBool("WalkForward", true);
        }

        if (horTwo == 0 || horTwo < deadZone && horTwo > -deadZone)
        {
            anim.SetBool("WalkForward", false);
            anim.SetBool("WalkBackwards", false);
        }

        if (CameraScript.cam.distanceBetweenPlayers >= 10)
        {
            anim.SetBool("WalkBackwards", false);
        }
    }
}