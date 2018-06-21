using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneWalk : MonoBehaviour {

    public float horOne, timer, deadZone;
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

        if (CameraScript.cam.distanceBetweenPlayers >= 10)
        {
            anim.SetBool("WalkBackwards", false);
        }
    }
}