using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StepCounter),typeof(HeightCollision))]
public class CollisionHandler : MonoBehaviour {
    Animator[] anim= new Animator[2];
    float[] inside = new float[2];
    float[] hanging = new float[2];
    float[] height = new float[2];
    AnimatorStateInfo[] asi = new AnimatorStateInfo[2];
    StepCounter sc;
    HeightCollision hc;
    //HandAnimationControl hac;
    float interruptTimerP1;
    float interruptTimerP2;
    int swingHash;
    public float CollisionTimeWeak = 0.3f;
    public float CollisionTimeStrong = 0.7f;
    public float BaseDelay = 0.5f;
    int StepDistance;

    public bool strongCollision = true;
    public bool handGuardHit = false;
    public bool miss = false;


    #region StrengthVariables
    [Header("--StrengthVariables--")]
    public float BaseStrength = 10;
    public float BaseDefence = 10;
    int[] movementbonus = new int[2];
    float attackTypeMult;
    float attackPenalty;
    float stanceMult;
    float parryMult;
    float collisionPoint;
    float defensePenalty;
    float collisionStrength;
    #endregion
    
    void Start () {
        sc = gameObject.GetComponent<StepCounter>();
        hc = gameObject.GetComponent<HeightCollision>();
        anim[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<Animator>();
        anim[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<Animator>();
        //hac = gameObject.GetComponent<HandAnimationControl>();


        interruptTimerP2 = 0;
        interruptTimerP1 = 0;
        swingHash = 0;
    }
	
	void Update () {
        UpdateVariables();
        
        if(asi[0].IsTag("Swing") && asi[1].IsTag("Swing"))
        {
            //molemmat lyö
        }
        else if (asi[0].IsTag("Swing"))
        {
            Attack(0);
            
        }
        else if (asi[1].IsTag("Swing"))
        {
            Attack(1);

        }
        Timer();
    }

    void UpdateVariables()
    {
        inside[0] = anim[0].GetFloat("Inside");
        hanging[0] = anim[0].GetFloat("Hanging");
        height[0] = anim[0].GetFloat("Height");
        inside[1] = anim[1].GetFloat("Inside");
        hanging[1] = anim[1].GetFloat("Hanging");
        height[1] = anim[1].GetFloat("Height");
        asi[0] = anim[0].GetCurrentAnimatorStateInfo(1);
        asi[1] = anim[1].GetCurrentAnimatorStateInfo(1);
    }

    void Attack(int player)
    {
        swingHash = asi[player].fullPathHash;
        float collidetime = anim[player].GetBool("Strong") ? CollisionTimeStrong : CollisionTimeWeak;
        if (asi[player].normalizedTime > collidetime)
        {
            if (CheckDistance())
            {
                if (CheckHeight(player))
                {
                    CalculateStrength(player);
                    if (CheckQuard())
                    {
                        
                        Deflect(player);
                    }
                    else
                    {
                        QuardBreak(player);
                    }
                }
            }
        }
    }

    void CalculateStrength(int player)
    {
        
        movementbonus[player] = sc.GetPrevStep(player + 1);
        attackTypeMult = anim[player].GetBool("Strong") ? 1.5f : 1;
        attackPenalty = 1;
        if (!strongCollision) attackPenalty -= 0.3f;
        //if(injured) attackpenalty -= injurePenalty;

        int otherplayer = player - 1 == -1 ? 1 : 0;
        movementbonus[otherplayer] = sc.GetPrevStep(otherplayer + 1);
        stanceMult = 1; //DO SOMETHING LATER
        parryMult = 1; //Parry not working yet
        collisionPoint = strongCollision ? 0 : 5;
        defensePenalty = 1;
        if (CheckQuard())
            defensePenalty = -1;
        if (miss)
            defensePenalty = 0;

    }
    
    bool CheckQuard()
    {
        float direction;
        direction = inside[0] * 2 - 1;
        float guard;
        guard = hanging[1] == inside[1] ? 1 : -1;
        if(direction == guard)
        {
            return true;
        }
        return false;
    }
    bool CheckDistance()
    {
        StepDistance = -sc.GetStepDistance();
        if(StepDistance >= 4 && StepDistance <= 12)
        {
            return true;
        }
        return false;
    }
    bool CheckHeight(int player)
    {
        //NOTE:FIX HANGING !!!!
        int otherplayer = player - 1 == -1 ? 1 : 0;
        if (height[player] > hc.GetHandle(otherplayer + 1) && height[player] < hc.GetMiddle(otherplayer + 1))
        {
            strongCollision = true;
        }
        else if(height[player] > hc.GetMiddle(otherplayer + 1) && height[player] < hc.GetTip(otherplayer + 1))
        {
            strongCollision = false;
        }
        else
        {
            miss = true;
        }
        if(height[player] > hc.GetHandle(otherplayer + 1) && height[player] < hc.GetBase(otherplayer + 1))
        {
            handGuardHit = true;
        }
        else
        {
            handGuardHit = false;
        }
        return true;
    }

    void Deflect(int player)
    {
        interruptTimerP1 = BaseDelay;
        interruptTimerP2 = BaseDelay;
        int otherplayer = player - 1 == -1 ? 1 : 0;
        anim[player].SetBool("Deflect", true);
        anim[otherplayer].SetBool("Deflect", true);
    }
    void QuardBreak(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;
        anim[otherplayer].SetBool("Interrupt", true);
        interruptTimerP1 = BaseDelay;
        interruptTimerP2 = BaseDelay;
    }

    void SetInterruptTimer(int player, float time)
    {
        if(player == 1)
        {
            interruptTimerP1 = time;
        }
        else
        {
            interruptTimerP2 = time;
        }
    }
    void Timer()
    {
        if (interruptTimerP1 > 0)
        {
            interruptTimerP1 -= Time.deltaTime;
        }
        else
        {
            interruptTimerP1 = 0;
            anim[1].SetBool("Deflect", false);
            anim[1].SetBool("Interrupt", false);
            anim[1].SetBool("lightDeflect", false);
        }
        if (interruptTimerP2 > 0)
        {
            interruptTimerP2 -= Time.deltaTime;
        }
        else
        {
            interruptTimerP2 = 0;
            anim[0].SetBool("Deflect", false);
            anim[0].SetBool("Interrupt", false);
            anim[0].SetBool("lightDeflect", false);
        }
    }

    public float GetHeight(int player)
    {
        return height[player - 1];
    }
}
