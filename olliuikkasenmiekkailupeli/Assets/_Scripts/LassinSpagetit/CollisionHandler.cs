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
    public float[] interruptTimer = new float[2];
    int swingHash;
    public float CollisionTimeWeak = 0.3f;
    public float CollisionTimeStrong = 0.7f;
    public float BaseDelay = 0.5f;
    int StepDistance;

    public bool strongCollision = true;
    public bool handGuardHit = false;
    public bool miss = false;
    public bool NoGuardCollision = false;
    public bool NoStrongCollision = false;


    #region StrengthVariables
    [Header("--StrengthVariables--")]
    public float attackStrength;
    public float defenceStrength;
    public float BaseStrength = 10;
    public float BaseDefence = 10;
    int[] movementbonus = new int[2];
    float attackTypeMult;
    float attackPenalty;
    float stanceMult;
    float parryMult;
    float collisionPoint;
    float defensePenalty;
    public float collisionStrength;
    #endregion
    
    void Start () {
        sc = gameObject.GetComponent<StepCounter>();
        hc = gameObject.GetComponent<HeightCollision>();
        anim[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<Animator>();
        anim[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<Animator>();
        //hac = gameObject.GetComponent<HandAnimationControl>();


        interruptTimer[1] = 0;
        interruptTimer[0] = 0;
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
                    if (!miss)
                    {
                        if (CheckQuard())
                        {

                            Deflect(player);
                        }
                        else
                        {
                            QuardBreak(player);
                        }
                    }
                    else
                    {
                        OverExtend(player);
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

        attackStrength = (BaseStrength + movementbonus[0]) * attackTypeMult * attackPenalty;
        defenceStrength = ((BaseDefence + movementbonus[1]) * stanceMult * parryMult - collisionPoint) * defensePenalty;
        collisionStrength = attackStrength - defenceStrength;
        if(collisionStrength == 0)
        {
            collisionStrength = 1;
        }
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
        NoGuardCollision = StepDistance >= 8 ? true : false;
        NoStrongCollision = StepDistance >= 10 ? true : false;
        if (StepDistance >= 2 && StepDistance <= 12)
        {
            return true;
        }
        
        return false;
    }
    bool CheckHeight(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;
        if(hanging[otherplayer] == 1)
        {
            if (height[player] < hc.GetBase(otherplayer + 1) && height[player] > hc.GetMiddle(otherplayer + 1) && !NoGuardCollision)
            {
                strongCollision = true;
                miss = false;
                handGuardHit = false;
            }
            else if (height[player] < hc.GetHandle(otherplayer + 1) && height[player] > hc.GetBase(otherplayer + 1) && !NoGuardCollision)
            {
                strongCollision = true;
                handGuardHit = true;
                miss = false;
                handGuardHit = false;
            }
            else if (height[player] < hc.GetMiddle(otherplayer + 1) && height[player] > hc.GetTip(otherplayer + 1))
            {
                strongCollision = false;
                miss = false;
                handGuardHit = false;
            }
            else
            {
                miss = true;
                handGuardHit = false;
            }
        }
        else
        {
            if (height[player] > hc.GetBase(otherplayer + 1) && height[player] < hc.GetMiddle(otherplayer + 1) && !NoGuardCollision)
            {
                strongCollision = true;
                miss = false;
                handGuardHit = false;
            }
            else if (height[player] > hc.GetHandle(otherplayer + 1) && height[player] < hc.GetBase(otherplayer + 1) && !NoGuardCollision)
            {
                strongCollision = true;
                handGuardHit = true;
                miss = false;
                handGuardHit = false;
            }
            else if (height[player] > hc.GetMiddle(otherplayer + 1) && height[player] < hc.GetTip(otherplayer + 1))
            {
                strongCollision = false;
                miss = false;
                handGuardHit = false;
            }
            else
            {
                miss = true;
                handGuardHit = false;
            }
        }
        
        return true;
    }

    void Deflect(int player)
    {
        
        int otherplayer = player - 1 == -1 ? 1 : 0;
        SetInterruptTimer(player, collisionStrength / 100);
        SetInterruptTimer(otherplayer, collisionStrength / 100);
        anim[player].SetBool("Deflect", true);
        anim[player].SetBool("light", true);
        anim[otherplayer].SetBool("Deflect", true);
        if (strongCollision)
        {
            anim[otherplayer].SetBool("light", true);
        }
        else
        {
            anim[otherplayer].SetBool("light", false);
        }
    }
    void QuardBreak(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;
        anim[player].SetBool("Interrupt", true);
        anim[otherplayer].SetBool("Interrupt", true);
        if (strongCollision)
        {
            anim[otherplayer].SetBool("light", true);
        }
        else
        {
            anim[otherplayer].SetBool("light", false);
        }
        SetInterruptTimer(player, collisionStrength/2 / 100);
        SetInterruptTimer(otherplayer, collisionStrength / 100);
    }
    void OverExtend(int player)
    {
        SetInterruptTimer(player, collisionStrength / 100);
        anim[player].SetBool("Interrupt", true);
    }

    void SetInterruptTimer(int player, float time)
    {
        if(player == 1)
        {
            interruptTimer[0] = time;
        }
        else
        {
            interruptTimer[1] = time;
        }
    }
    void Timer()
    {
        for(int i = 0; i < 2; i++)
        {
            if (interruptTimer[i] > 0)
            {
                interruptTimer[i] -= Time.deltaTime;
            }
            else
            {
                interruptTimer[i] = 0;
                anim[i].SetBool("Deflect", false);
                anim[i].SetBool("Interrupt", false);
                anim[i].SetBool("light", false);
            }
        }
    }

    public float GetHeight(int player)
    {
        return height[player - 1];
    }
}
