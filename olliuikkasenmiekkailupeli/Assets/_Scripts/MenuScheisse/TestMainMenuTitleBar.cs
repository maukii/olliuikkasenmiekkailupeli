using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenuTitleBar : MonoBehaviour
{
    public Image tb1, tb2, tb3, tb4;
    public Animator anim;

    public bool canInteract;

    float timer, defaultTimer;

    void Start()
    {
        tb1.enabled = true;
        tb2.enabled = false;
        tb3.enabled = false;
        tb4.enabled = false;

        anim.SetFloat("Blend", 1);
        canInteract = true;

        timer = 0.5f;
        defaultTimer = timer;
    }

    void Update()
    {
        HandMove();
    }

    void HandMove()
    {
        if (anim.GetFloat("Blend") == 1 && InputManager.IM.P1_LS_Y < 0 && canInteract)
        {
            anim.SetFloat("Blend", 0.35f);
            
            tb1.enabled = false;
            tb2.enabled = true;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == 0.35f && InputManager.IM.P1_LS_Y < 0 && canInteract)
        {
            anim.SetFloat("Blend", -0.35f);

            tb2.enabled = false;
            tb3.enabled = true;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -0.35f && InputManager.IM.P1_LS_Y < 0 && canInteract)
        {
            anim.SetFloat("Blend", -1);

            tb3.enabled = false;
            tb4.enabled = true;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -1 && InputManager.IM.P1_LS_Y > 0 && canInteract)
        {
            anim.SetFloat("Blend", -0.35f);

            tb3.enabled = true;
            tb4.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -0.35f && InputManager.IM.P1_LS_Y > 0 && canInteract)
        {
            anim.SetFloat("Blend", 0.35f);

            tb2.enabled = true;
            tb3.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == 0.35f && InputManager.IM.P1_LS_Y > 0 && canInteract)
        {
            anim.SetFloat("Blend", 1);

            tb1.enabled = true;
            tb2.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (!canInteract)
        {
            timer = timer - Time.deltaTime;

            if (timer < 0)
            {
                canInteract = true;
            }
        }
    }
}