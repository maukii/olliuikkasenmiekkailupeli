using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public int player1Model { get; set; }
    public int player2Model { get; set; }

    public bool player1Dead { get; set; }
    public bool player2Dead { get; set; }
    public bool battleStarted { get; private set; }

    public float gameTimer;
    public float maxTimer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        StartBattle();
        CheckInput();
        Timer();
    }

    void StartBattle()
    {
        if (SceneManager.GetActiveScene().name == "testifesti" && !battleStarted)
        {
            battleStarted = true;
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SceneManager.LoadScene("testifesti");
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SceneManager.LoadScene("Main");
        }
    }

    void Timer()
    {
        if (battleStarted)
        {
            gameTimer += Time.deltaTime;
            if(gameTimer >= maxTimer)
            {
                GameEnded();
            }
        }
    }

    void GameEnded()
    {
        gameTimer = 0f;
        battleStarted = false;

        // TODO: show winner --> load mainmenu / retry
    }

}
