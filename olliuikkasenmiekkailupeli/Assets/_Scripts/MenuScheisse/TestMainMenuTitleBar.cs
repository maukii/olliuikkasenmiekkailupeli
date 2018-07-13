using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenuTitleBar : MonoBehaviour
{
    public Image tb1, tb2, tb3, tb4;
    public Animator anim;

    bool canInteract;

    float timer, defaultTimer, ver;

    void Start()
    {
        tb1.enabled = true;
        tb2.enabled = false;
        tb3.enabled = false;
        tb4.enabled = false;

        anim.SetFloat("Blend", 1);
        canInteract = true;

        timer = 0.5f;   //Tällä voi vaihtaa nopeutta millä hahmo vaihtaa miekan paikkaa
        defaultTimer = timer;
    }

    void Update()
    {
        ver = Input.GetAxis("Vertical");

        HandMove();
        TitleBars();
    }

    void HandMove()
    {
        if (anim.GetFloat("Blend") == 1 && ver < 0 && canInteract)
        {
            anim.SetFloat("Blend", 0.35f, 0.5f, 1);
            
            tb1.enabled = false;
            tb2.enabled = true;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == 0.35f && ver < 0 && canInteract)
        {
            anim.SetFloat("Blend", -0.35f);

            tb2.enabled = false;
            tb3.enabled = true;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -0.35f && ver < 0 && canInteract)
        {
            anim.SetFloat("Blend", -1);

            tb3.enabled = false;
            tb4.enabled = true;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -1 && ver > 0 && canInteract)
        {
            anim.SetFloat("Blend", -0.35f);

            tb3.enabled = true;
            tb4.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -0.35f && ver > 0 && canInteract)
        {
            anim.SetFloat("Blend", 0.35f);

            tb2.enabled = true;
            tb3.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == 0.35f && ver > 0 && canInteract)
        {
            anim.SetFloat("Blend", 1);

            tb1.enabled = true;
            tb2.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == -1 && ver < 0 && canInteract)
        {
            anim.SetFloat("Blend", 1);

            tb1.enabled = true;
            tb4.enabled = false;
            canInteract = false;

            timer = defaultTimer;
        }

        if (anim.GetFloat("Blend") == 1 && ver > 0 && canInteract)
        {
            anim.SetFloat("Blend", -1);

            tb1.enabled = false;
            tb4.enabled = true;
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

    void TitleBars()
    {
        if (tb1.enabled && InputManager.IM.P1_A || tb1.enabled && InputManager.IM.P2_A)
        {
            //Start game
            Debug.Log("START GAME");
        }

        if (tb2.enabled && InputManager.IM.P1_A || tb2.enabled && InputManager.IM.P2_A)
        {
            //Options
            Debug.Log("OPTIONS");
        }

        if (tb3.enabled && InputManager.IM.P1_A || tb3.enabled && InputManager.IM.P2_A)
        {
            //Credits
            Debug.Log("CREDITS");
        }

        if (tb4.enabled && InputManager.IM.P1_A || tb4.enabled && InputManager.IM.P2_A)
        {
            //Exit
            Debug.Log("EXIT");
            Application.Quit();
        }
    }
}