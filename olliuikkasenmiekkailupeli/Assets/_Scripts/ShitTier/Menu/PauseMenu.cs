using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public KeyCode startButton; // can have own for p1 and p2

    [SerializeField] GameObject PauseMenuUI;
    public static bool gameIsPaused = false;

    bool canInteract;
    float timer = 0.5f, defaultTimer;

    private void Update()
    {
        if(GameHandler.instance.BattleStarted)
        {
            MenuLogic();
        }
    }

    private void MenuLogic()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(startButton))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        Debug.Log("Pause");
        PauseMenuUI.SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("Return");
        PauseMenuUI.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
