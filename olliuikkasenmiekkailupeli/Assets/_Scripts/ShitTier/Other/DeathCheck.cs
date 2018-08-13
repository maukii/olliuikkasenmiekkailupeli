using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCheck : MonoBehaviour
{

    [SerializeField] int playerIndex;

    Animator[] anims;
    [SerializeField] Animator anim;

    [SerializeField] bool dead;
    [SerializeField] bool scoreGiven = false;

    void Start()
    {
        playerIndex = GetComponent<AlternativeMovement5>().playerIndex;

        GetAnim();
    }

    private void GetAnim()
    {
        anims = GetComponentsInChildren<Animator>();

        for (int i = 0; i < anims.Length; i++)
        {
            if (anims[i].enabled)
                anim = anims[i];
        }
    }

    void Update()
    {
        if(GameHandler.instance.BattleStarted)
        {
            dead = anim.GetBool("Dead");
        }

        if(dead && !scoreGiven)
        {
            GameHandler.instance.GameEnded(playerIndex == 1 ? 2 : 1);
            scoreGiven = true;
        }
    }
}
