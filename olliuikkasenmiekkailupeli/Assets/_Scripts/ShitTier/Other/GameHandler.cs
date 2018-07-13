using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public static GameHandler instance;

    public string P1_Hor, P1_Ver, P2_Hor, P2_Ver; // only for display

    string player1Hor, player1Ver;                            
    string player2Hor, player2Ver;
    int player1Model, player2Model;                            
    bool player1Dead, player2Dead;

    private bool battleStarted;

    public bool BattleStarted
    {
        get { return battleStarted; }
        set { battleStarted = value; }
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
        Timer();
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

    public void GameEnded()
    {
        gameTimer = 0f;
        battleStarted = false;

        // TODO: show winner --> load mainmenu / retry
    }


    // all get functions
    #region GetPlayerProperties 
    public string GetPlayer1Horizontal()
    {
        return player1Hor;
    }
    public string GetPlayer1Vertical()
    {
        return player2Ver;
    }
    public int GetPlayer1Model()
    {
        return player1Model;
    }

    public string GetPlayer2Horizontal()
    {
        return player2Hor;
    }
    public string GetPlayer2Vertical()
    {
        return player2Ver;
    }
    public int GetPlayer2Model()
    {
        return player2Model;
    }
    #endregion

    // all set functions
    #region SetPlayerProperties
    public void SetPlayer1Model(int index)
    {
        player1Model = index;
    }
    public void SetPlayer1Axes(string hor, string ver) // TODO: ADD BUTTONS ALSO 
    {
        player1Hor = hor;
        player1Ver = ver;
    }

    public void SetPlayer2Model(int index)
    {
        player2Model = index;
    }
    public void SetPlayer2Axes(string hor, string ver)
    {
        player2Hor = hor;
        player2Ver = ver;
    } // + btns
    #endregion
}

