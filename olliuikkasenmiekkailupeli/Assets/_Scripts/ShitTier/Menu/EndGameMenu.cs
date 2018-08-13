using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour
{
    [SerializeField] GameObject activeNode, menuUI;
    [SerializeField] float timer = 0.2f, defaultTimer = .2f;

    float hor, ver;
    int index;
    public bool canInteract;

    [SerializeField] bool active;

    [SerializeField] GameObject[] nodes;
    [SerializeField] GameObject[] highlights;

    void Start()
    {
        defaultTimer = timer;
        menuUI.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (GameHandler.instance.BattleEnded)
        {
            active = true;
            menuUI.gameObject.SetActive(true);

            GetInput();
            ChangeNode();
            ButtonLogic();
        }
    }

    void DisableHighlights()
    {
        for (int i = 0; i < highlights.Length; i++)
        {
            highlights[i].gameObject.SetActive(false);
        }
    }

    private void ChangeNode()
    {
        if (!canInteract)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0)
            {
                timer = defaultTimer;
                canInteract = true;
            }
        }

        if(gameObject.activeSelf && canInteract)
        {
            if(ver > 0)
            {
                ToggleRight();
            }
            if(ver < 0)
            {
                ToggleLeft();
            }
        }
    }

    void ToggleRight()
    {
        index++;

        if (index > nodes.Length - 1)
        {
            index = 0;
        }
        canInteract = false;

        DisableHighlights();
        highlights[index].SetActive(true);

    }

    void ToggleLeft()
    {
        index--;

        if (index < 0)
        {
            index = nodes.Length - 1;
        }
        canInteract = false;

        DisableHighlights();
        highlights[index].SetActive(true);

    }

    private void ButtonLogic()
    {
        activeNode = nodes[index];

        if ((InputManager.IM.P1_A || InputManager.IM.P2_A || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && canInteract)
        {
            if (activeNode == nodes[0])
            {
                // RESUME
                Debug.Log("Play again");
                PlayAgain();
                canInteract = false;
            }
            else if (activeNode == nodes[1])
            {
                // BACK TO MAIN
                BackToMenu();
                canInteract = false;
            }
        }         
    }

    public void PlayAgain()
    {
        Debug.Log("PlayAgain");
        GameHandler.instance.battleStarted = false;
        LevelChanger.instance.FadeToCharacterSelect();
    }

    public void BackToMenu()
    {
        Debug.Log("BackToMain");
        MainMenuController.MMC.isTutorial = false;
        GameHandler.instance.BattleStarted = false;

        LevelChanger.instance.FadeToMain(); // vaihtaa skenen level changerissa

        //SceneManager.LoadScene(0);  //TOIMII, MUTTA RIKKOO MENU-UKON KÄDEN
    }

    void GetInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
    }
}