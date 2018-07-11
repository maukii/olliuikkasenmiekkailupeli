using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public string player1Hor { get; set; }
    public string player1Ver { get; set; }

    public string player2Hor { get; set; }
    public string player2Ver { get; set; }

    public string P1_Hor, P1_Ver, P2_Hor, P2_Ver;

    public int player1Model { get; set; }
    public int player2Model { get; set; }

    public bool player1Dead { get; set; }
    public bool player2Dead { get; set; }

    public bool battleStarted { get; set; }

    public float gameTimer;
    public float maxTimer;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        Timer();
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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

