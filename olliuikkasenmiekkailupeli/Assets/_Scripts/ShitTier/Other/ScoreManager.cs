using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    public static ScoreManager instance;

    [SerializeField]
    int p1_wins, p2_wins;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
    }

    public void PlayerWon(int playerNumber)
    {
        if(playerNumber == 1)
        {
            p1_wins++;
        }
        else if(playerNumber == 2)
        {
            p2_wins++;
        }
    }
        
}
