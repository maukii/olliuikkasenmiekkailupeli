using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenuTitleBar : MonoBehaviour
{
    public Image tb1, tb2, tb3;

    void Start()
    {
        tb1.enabled = false;
        tb2.enabled = false;
        tb3.enabled = false;
    }

    void Update()
    {
        if (InputManager.IM.P1_LS_X < 0.01f && InputManager.IM.P1_LS_X >= -0.33f)
        {
            //Start Game
            tb1.enabled = true;
            tb2.enabled = false;
        }

        if (InputManager.IM.P1_LS_X < -0.33f && InputManager.IM.P1_LS_X >= -0.66f)
        {
            //Options
            tb1.enabled = false;
            tb2.enabled = true;
            tb3.enabled = false;
        }

        if (InputManager.IM.P1_LS_X < -0.66f && InputManager.IM.P1_LS_X >= -1f)
        {
            //Exit
            tb2.enabled = false;
            tb3.enabled = true;
        }
    }

    void Menu()
    {
        if (tb1.enabled && Input.GetKey(KeyCode.Space))
        {
            StartGame();
        }

        if (tb2.enabled && Input.GetKey(KeyCode.Space))
        {
            Options();
        }

        if (tb3.enabled && Input.GetKey(KeyCode.Space))
        {
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
}
