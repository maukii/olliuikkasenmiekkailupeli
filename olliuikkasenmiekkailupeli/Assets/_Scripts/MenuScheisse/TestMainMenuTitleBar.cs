using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenuTitleBar : MonoBehaviour
{
    public Image tb1, tb2, tb3, tb4;
    public Animator anim;

    public bool canInteract;
    public float timer, defaultTimer;

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

/* PELAAJA SAA ITSE LIIKUTTAA KÄTTÄ... EI VISSIIN OLLU HYVÄ IDEA...

public float blend;

void Start()
{
    tb1.enabled = false;
    tb2.enabled = false;
    tb3.enabled = false;

    anim.SetFloat("Blend", 0);
}

void Update()
{
    blend = InputManager.IM.P1_LS_X;
    anim.SetFloat("Blend", blend);

    CheckSwordPos();
    Menu();
}

void CheckSwordPos()
{
    if (InputManager.IM.P1_LS_X > 1)
    {
        tb1.enabled = false;
        tb2.enabled = false;
        tb3.enabled = false;
    }

    if (InputManager.IM.P1_LS_X > 0.65f)
    {
        //Start Game
        tb1.enabled = true;
    }

    if (InputManager.IM.P1_LS_X < 0.65f && InputManager.IM.P1_LS_X > 0.25f)
    {
        tb1.enabled = false;
        tb2.enabled = false;
        tb3.enabled = false;
    }

    if (InputManager.IM.P1_LS_X < 0.25f && InputManager.IM.P1_LS_X > -0.15f)
    {
        //Options
        tb2.enabled = true;
    }

    if (InputManager.IM.P1_LS_X < -0.15f && InputManager.IM.P1_LS_X > -0.65f)
    {
        tb1.enabled = false;
        tb2.enabled = false;
        tb3.enabled = false;
    }

    if (InputManager.IM.P1_LS_X < -0.65f)
    {
        //Exit
        tb3.enabled = true;
    }

    if (InputManager.IM.P1_LS_X < -1)
    {
        tb1.enabled = false;
        tb2.enabled = false;
        tb3.enabled = false;
    }
}

void Menu()
{
    if (tb1.enabled && Input.GetKey(KeyCode.Space))
    {
        anim.SetTrigger("Lunge");
        StartGame();
    }

    if (tb2.enabled && Input.GetKey(KeyCode.Space))
    {
        anim.SetTrigger("Lunge");
        Options();
    }

    if (tb3.enabled && Input.GetKey(KeyCode.Space))
    {
        anim.SetTrigger("Lunge");
        QuitGame();
    }
}

void StartGame()
{
    Debug.Log("START GAME");
}

void Options()
{
    Debug.Log("OPTIONS");
}

void QuitGame()
{
    Application.Quit();
    Debug.Log("EXIT");
}
*/

/*
//Visual stuff only...
//Tällä hetkellä Box Collider objektissa 'kämmen.L'
public Image bar;
public bool isTouching;

void Update()
{
    if (isTouching)
    {
        bar.enabled = true;
    }

    else
    {
        bar.enabled = false;
    }
}

void OnTriggerEnter(Collider col)
{
    if (col.gameObject.CompareTag("Untagged")) //Laita joku toinen tagi tai sitten ei...
    {
        isTouching = true;
    }
}

void OnTriggerExit(Collider col)
{
    if (col.gameObject.CompareTag("Untagged"))
    {
        isTouching = false;
    }
}
*/
