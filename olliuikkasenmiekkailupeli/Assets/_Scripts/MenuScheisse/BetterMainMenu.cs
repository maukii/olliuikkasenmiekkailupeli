using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetterMainMenu : MonoBehaviour
{
    public Animator anim;
    public GameObject currentNode;
    public Image tb1, tb2, tb3, tb4;

    public float timer, defaultTimer, ver;

    public bool canInteract;
    
    float dampTime = 0.2f;

    void Start()
    {
        canInteract = true;

        timer = 0.5f;   //Tällä voi vaihtaa nopeutta millä hahmo vaihtaa miekan paikkaa
        defaultTimer = timer;
    }

    void Update()
    {
        ver = Input.GetAxis("Vertical");

        HandMove();
        CheckNode();
        TitleBars();
    }

    void HandMove()
    {
        if (currentNode == null)
        {
            currentNode = GameObject.Find("StartButton");
            anim.SetFloat("Blend", 1);

            canInteract = true;
        }

        if (currentNode.name == "StartButton" && ver < 0 && canInteract)
        {
            currentNode.name = "OptionsButton";
            canInteract = false;
        }

        if (currentNode.name == "OptionsButton" && ver < 0 && canInteract)
        {
            currentNode.name = "CreditsButton";
            canInteract = false;
        }

        if (currentNode.name == "CreditsButton" && ver < 0 && canInteract)
        {
            currentNode.name = "ExitButton";
            canInteract = false;
        }

        if (currentNode.name == "ExitButton" && ver < 0 && canInteract)
        {
            currentNode.name = "StartButton";
            canInteract = false;
        }

        if (currentNode.name == "StartButton" && ver > 0 && canInteract)
        {
            currentNode.name = "ExitButton";
            canInteract = false;
        }

        if (currentNode.name == "OptionsButton" && ver > 0 && canInteract)
        {
            currentNode.name = "StartButton";
            canInteract = false;
        }

        if (currentNode.name == "CreditsButton" && ver > 0 && canInteract)
        {
            currentNode.name = "OptionsButton";
            canInteract = false;
        }

        if (currentNode.name == "ExitButton" && ver > 0 && canInteract)
        {
            currentNode.name = "CreditsButton";
            canInteract = false;
        }
    }

    void CheckNode()
    {
        if (currentNode != null)
        {
            if (!canInteract)
            {
                timer = timer - Time.deltaTime;

                if (timer < 0)
                {
                    canInteract = true;
                }
            }

            if (canInteract)
            {
                timer = defaultTimer;
            }

            if (currentNode.name == "StartButton")
            {
                anim.SetFloat("Blend", 1, dampTime, Time.deltaTime);

                tb1.enabled = true;
                tb2.enabled = false;
                tb3.enabled = false;
                tb4.enabled = false;
            }

            if (currentNode.name == "OptionsButton")
            {
                anim.SetFloat("Blend", 0.35f, dampTime, Time.deltaTime);

                tb1.enabled = false;
                tb2.enabled = true;
                tb3.enabled = false;
                tb4.enabled = false;
            }

            if (currentNode.name == "CreditsButton")
            {
                anim.SetFloat("Blend", -0.35f, dampTime, Time.deltaTime);

                tb1.enabled = false;
                tb2.enabled = false;
                tb3.enabled = true;
                tb4.enabled = false;
            }

            if (currentNode.name == "ExitButton")
            {
                anim.SetFloat("Blend", -1, dampTime, Time.deltaTime);

                tb1.enabled = false;
                tb2.enabled = false;
                tb3.enabled = false;
                tb4.enabled = true;
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