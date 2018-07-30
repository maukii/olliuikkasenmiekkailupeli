using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCounter : MonoBehaviour {

    Animator P1Amim;
    Animator P2Amim;

    AnimatorStateInfo asi;
    int[] PrevAnimHash = new int[2];
    int[] HalfStepCount = new int[2];
    int[] PrevStep = new int[2];
    [SerializeField]
    int StepsBetweenPlayers;

    void Start () {
        P1Amim = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<Animator>();
        P2Amim = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<Animator>();
    }
	
	
	void Update () {
        CheckAnimationStateChange(P1Amim, 1);
        CheckAnimationStateChange(P2Amim, 2);
        StepsBetweenPlayers = HalfStepCount[0] + HalfStepCount[1] - 16;

    }

    void CheckAnimationStateChange(Animator anim, int playerNumber)
    {
        asi = anim.GetCurrentAnimatorStateInfo(0);
        if(PrevAnimHash[playerNumber - 1] == 0)
        {
            PrevAnimHash[playerNumber - 1] = asi.fullPathHash;
        }
        else
        {
            if(PrevAnimHash[playerNumber - 1] != asi.fullPathHash)
            {
                UpdateStepCount(asi, playerNumber);
                PrevAnimHash[playerNumber - 1] = asi.fullPathHash;
            }
        }
    }

    void UpdateStepCount(AnimatorStateInfo ansi, int playerNumber)
    {
        if (asi.IsTag("1step"))
        {
            HalfStepCount[playerNumber - 1] += 1;
            PrevStep[playerNumber - 1] = 1;
        }
        else if (asi.IsTag("2step"))
        {
            HalfStepCount[playerNumber - 1] += 2;
            PrevStep[playerNumber - 1] = 2;
        }
        else if (asi.IsTag("4step"))
        {
            HalfStepCount[playerNumber - 1] += 4;
            PrevStep[playerNumber - 1] = 4;
        }
        else if (asi.IsTag("-1step"))
        {
            HalfStepCount[playerNumber - 1] += -1;
            PrevStep[playerNumber - 1] = -1;
        }
        else if (asi.IsTag("-2step"))
        {
            HalfStepCount[playerNumber - 1] += -2;
            PrevStep[playerNumber - 1] = -2;
        }
        else if (asi.IsTag("-4step"))
        {
            HalfStepCount[playerNumber - 1] += -4;
            PrevStep[playerNumber - 1] = -4;
        }
        else
        {
            PrevStep[playerNumber - 1] = 0;
        }
    }
    public int GetStepDistance()
    {
        return StepsBetweenPlayers;
    }
    public int GetPrevStep(int player)
    {
        return PrevStep[player - 1];
    }
}
