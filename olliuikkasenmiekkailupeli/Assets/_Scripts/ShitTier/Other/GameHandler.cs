using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;
    
    int player1Model, player2Model;                            
    bool player1Dead, player2Dead;

    public bool battleStarted;
    public bool BattleStarted
    {
        get { return battleStarted; }
        set { battleStarted = value; }
    }

    public bool battleEnded;
    public bool BattleEnded
    {
        get { return battleEnded; }
        set { battleEnded = value; }
    }

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
        if (!MainMenuController.MMC.isTutorial)
        {
            Timer();
        }

        if (SceneManager.GetActiveScene().name != "GameScene")
            battleEnded = false;
    }

    void Timer()
    {
        if (battleStarted)
        {
            gameTimer += Time.deltaTime;
            if(gameTimer >= maxTimer)
            {
                GameEnded(-1);
            }
        }
    }

    public void GameEnded(int playerIndex)
    {
        gameTimer = 0f;
        battleStarted = false;

        if (playerIndex == 1 || playerIndex == 2)
        {
            ScoreManager.instance.PlayerWon(playerIndex);
        }
        else if(playerIndex == -1) // time ended
        {
            Debug.Log("time ended");
        }
    }


    // GetModels
    #region GetPlayerModels
    public int GetPlayer1Model()
    {
        return player1Model;
    }

    public int GetPlayer2Model()
    {
        return player2Model;
    }
    #endregion

    #region SetPlayerModels
    public void SetPlayer1Model(int index)
    {
        player1Model = index;
    }

    public void SetPlayer2Model(int index)
    {
        player2Model = index;
    }
    #endregion 
    
}

