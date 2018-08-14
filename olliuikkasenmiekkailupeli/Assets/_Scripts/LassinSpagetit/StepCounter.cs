using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepCounter : MonoBehaviour {

    Animator P1Amim;
    Animator P2Amim;
    Transform P1;
    Transform P2;
    Transform P1model;
    Transform P2model;
    public bool stepCountFromActualDistance;
    public float SceneScaleMult = 1;
    public int startingDistance;
    AnimatorStateInfo asi;
    int[] PrevAnimHash = new int[2];
    int[] HalfStepCount = new int[2];
    int[] PrevStep = new int[2];
    [SerializeField]
    int StepsBetweenPlayers;
    bool retreated = false;
    bool jump = false;
    float actualDistance;
    

    void Start () {
        P1Amim = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<Animator>();
        P2Amim = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<Animator>();
        P1 = GameObject.FindGameObjectWithTag("Player 1").transform;
        P2 = GameObject.FindGameObjectWithTag("Player 2").transform;
        P1model = P1.GetComponentInChildren<HandAnimationControl>().transform;
        P2model = P2.GetComponentInChildren<HandAnimationControl>().transform;
        actualDistance = P1model.position.x - P2model.position.x;
    }
	
	
	void Update () {
        if (!stepCountFromActualDistance)
        {
            CheckAnimationStateChange(P1Amim, 1);
            CheckAnimationStateChange(P2Amim, 2);
            StepsBetweenPlayers = HalfStepCount[0] + HalfStepCount[1] - startingDistance;
        }
        else
        {
            CalculateActualDistance();
        }

    }

    void CalculateActualDistance()
    {
        actualDistance = P1model.position.x - P2model.position.x;
        StepsBetweenPlayers = Mathf.FloorToInt(actualDistance / 0.3f) + (int)(2/SceneScaleMult);
        StepsBetweenPlayers = (int)(StepsBetweenPlayers * SceneScaleMult * 2);
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
        if(asi.IsName("Base Layer.LungeRetreat"))
        {
            retreated = true;
        }
        if (asi.IsName("Base Layer.Jump"))
        {
            jump = true;
        }
        if (asi.IsTag("1step"))
        {
            HalfStepCount[playerNumber - 1] += 1;
            PrevStep[playerNumber - 1] = 1;
            retreated = false;
            jump = false;
        }
        else if (asi.IsTag("2step"))
        {
            HalfStepCount[playerNumber - 1] += 2;
            PrevStep[playerNumber - 1] = 2;
            retreated = false;
            jump = false;
        }
        else if (asi.IsTag("4step"))
        {
            HalfStepCount[playerNumber - 1] += 4;
            PrevStep[playerNumber - 1] = 4;
            retreated = false;
            jump = false;
        }
        else if (asi.IsTag("-1step"))
        {
            HalfStepCount[playerNumber - 1] += -1;
            PrevStep[playerNumber - 1] = -1;
            retreated = false;
            jump = false;
        }
        else if (asi.IsTag("-2step"))
        {
            HalfStepCount[playerNumber - 1] += -2;
            PrevStep[playerNumber - 1] = -2;
            retreated = false;
            jump = false;
        }
        else if (asi.IsTag("-4step"))
        {
            if (retreated && jump)
            {
                retreated = false;
                jump = false;
            }
            else
            {
                HalfStepCount[playerNumber - 1] += -4;
                PrevStep[playerNumber - 1] = -4;
            }
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
