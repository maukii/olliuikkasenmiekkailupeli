using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAnimationTest : MonoBehaviour {
    Animator[] anim= new Animator[2];
    float inside;
    float hanging;
    float otherInside;
    float otherHanging;
    AnimatorStateInfo[] asi = new AnimatorStateInfo[2];
    StepCounter sc;
    //HandAnimationControl hac;
    float interruptTimerP1;
    float interruptTimerP2;
    int swingHash;
    public float CollisionTimeWeak = 0.3f;
    public float CollisionTimeStrong = 0.7f;
    public float BaseDelay = 0.5f;
    int StepDistance;

    bool strongCollision = true;
    bool miss = false;


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


    // Use this for initialization
    void Start () {
        sc = FindObjectOfType<StepCounter>();
        anim[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<Animator>();
        anim[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<Animator>();
        //hac = gameObject.GetComponent<HandAnimationControl>();


        interruptTimerP2 = 0;
        interruptTimerP1 = 0;
        swingHash = 0;
    }
	
	// Update is called once per frame
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
        inside = anim[0].GetFloat("Inside");
        hanging = anim[0].GetFloat("Hanging");
        otherInside = anim[1].GetFloat("Inside");
        otherHanging = anim[1].GetFloat("Hanging");
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
                if (CheckHeight())
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
    void Deflect(int player)
    {
        interruptTimerP1 = BaseDelay;
        interruptTimerP2 = BaseDelay;
        int otherplayer = player - 1 == -1 ? 1 : 0;
        anim[player].SetBool("Deflect", true);
        anim[otherplayer].SetBool("Deflect", true);
    }
    bool CheckQuard()
    {
        float direction;
        direction = inside * 2 - 1;
        float guard;
        guard = otherHanging == otherInside ? 1 : -1;
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
    bool CheckHeight()
    {

        return true;
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
}
