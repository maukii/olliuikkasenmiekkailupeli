using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StepCounter),typeof(HeightCollision),typeof(CollisionDamage))]
public class CollisionHandler : MonoBehaviour {

    #region Variables

    #region AnimationVariables

    ParticleHandler[] ph = new ParticleHandler[2];
    Animator[] anim= new Animator[2];
    float[] inside = new float[2];
    float[] hanging = new float[2];
    float[] height = new float[2];
    AnimatorStateInfo[] asi = new AnimatorStateInfo[2];

    #endregion

    #region CollisionVariables

    StepCounter sc;
    HeightCollision hc;
    CollisionDamage cd;
    int StepDistance;
    float collisionOffsetSide = 0.9f;
    float[] origCollisionTime = new float[2];
    public float LungeHeightOffset = -0.5f;
    public float heightOffsetMult = 0.5f;
    float[] lungeOffset = new float[2];
    #endregion

    #region CollisionTriggers

    int WhoHitFirst;
    bool strongCollision = true;
    bool handGuardHit = false;
    bool miss = false;
    bool NoGuardCollision = false;
    bool NoStrongCollision = false;
    bool NoCollision = false;
    bool[] calculateCollision = new bool[2];

    #endregion

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

    #region TimerVariables

    [Header("DelayTimerVariables")]
    public float[] interruptTimer = new float[2];
    public float CollisionTimeWeak = 0.9f;
    public float CollisionTimeStrong = 0.9f;
    public float BaseDelay = 0.5f;
    public float SlowMoMult = 0.9f;
    float[] origSpeed = new float[2];
    #endregion

    #endregion

    public static int deflectsP1 = 0, deflectsP2 = 0;

    void Start () {
        sc = gameObject.GetComponent<StepCounter>();
        hc = gameObject.GetComponent<HeightCollision>();
        cd = gameObject.GetComponent<CollisionDamage>();
        anim[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<Animator>();
        anim[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<Animator>();
        ph[0] = GameObject.FindGameObjectWithTag("Player 1").GetComponentInChildren<ParticleHandler>();
        ph[1] = GameObject.FindGameObjectWithTag("Player 2").GetComponentInChildren<ParticleHandler>();
        //hac = gameObject.GetComponent<HandAnimationControl>();
        origSpeed[0] = anim[0].speed;
        origSpeed[1] = anim[1].speed;

        interruptTimer[1] = 0;
        interruptTimer[0] = 0;
        calculateCollision[0] = false;
        calculateCollision[1] = false;
    }
	
	void Update () {
        UpdateVariables();
        
        if(asi[0].IsTag("Swing") && asi[1].IsTag("Swing"))
        {
            AttackBoth(WhoHitFirst);
        }
        else if (asi[0].IsTag("Swing"))
        {
            Attack(0);
            WhoHitFirst = 0;
        }
        else if (asi[1].IsTag("Swing"))
        {
            Attack(1);
            WhoHitFirst = 1;
        }
        if (!asi[0].IsTag("Swing"))
        {
            calculateCollision[0] = true;
            cd.NoDamage(0);
        }
        if (!asi[1].IsTag("Swing"))
        {
            calculateCollision[1] = true;
            cd.NoDamage(1);
        }
        Timer();

        if((deflectsP1 == 3 || deflectsP2 == 3) && !earned)
        {
            AchievementManager.instance.SetProgressToAchievement("Parry!", 1);
            earned = true;
        }
    }

    bool earned;

    void UpdateVariables()
    {
        inside[0] = anim[0].GetFloat("Inside");
        hanging[0] = anim[0].GetFloat("Hanging");
        height[0] = anim[0].GetFloat("Height");
        inside[1] = anim[1].GetFloat("Inside");
        hanging[1] = anim[1].GetFloat("Hanging");
        height[1] = anim[1].GetFloat("Height");
        asi[0] = anim[0].GetCurrentAnimatorStateInfo(0);
        asi[1] = anim[1].GetCurrentAnimatorStateInfo(0);
        CheckLunge(asi[0], 0);
        CheckLunge(asi[1], 1);
        asi[0] = anim[0].GetCurrentAnimatorStateInfo(1);
        asi[1] = anim[1].GetCurrentAnimatorStateInfo(1);
    }
    void CheckLunge(AnimatorStateInfo asi, int player)
    {
        if(asi.IsName("Base Layer.LungeIdle") || asi.IsName("Base Layer.ToLungeIdle") || asi.IsName("Base Layer.Lunge1") || asi.IsName("Base Layer.Lunge2") || asi.IsName("Base Layer.LungeAdvanced1") || asi.IsName("Base Layer.LungeRetreat"))
        {
            lungeOffset[player] = LungeHeightOffset;
        }
        else
        {
            lungeOffset[player] = 0;
        }
    }
    void Attack(int player)
    {
        origCollisionTime[player] = anim[player].GetBool("Strong") ? CollisionTimeStrong : CollisionTimeWeak;
        float collidetime = origCollisionTime[player];
        collidetime = CheckCollideTime(player, origCollisionTime[player]);
        if (asi[player].normalizedTime > collidetime && calculateCollision[player])
        {
            
            calculateCollision[player] = false;
            if (CheckDistance(player))
            {
                if (CheckHeight(player))
                {
                    CalculateStrength(player);
                    if (!miss)
                    {
                        if (CheckQuard(player))
                        {
                            cd.NoDamage(player);
                            Deflect(player);
                        }
                        else
                        {
                            cd.ApplyDamage(player);
                            QuardBreak(player);
                        }
                        AudioManager.instance.PlaySwordClashSound();
                    }
                    else
                    {
                        cd.ApplyDamage(player);
                        OverExtend(player);
                        AudioManager.instance.PlaySwordsSwingSound();
                    }
                }
            }
        }
    }
    void AttackBoth(int FirstAttack)
    {
        int otherplayer = FirstAttack - 1 == -1 ? 1 : 0;
        origCollisionTime[FirstAttack] = anim[FirstAttack].GetBool("Strong") ? CollisionTimeStrong : CollisionTimeWeak;
        origCollisionTime[otherplayer]= anim[otherplayer].GetBool("Strong") ? CollisionTimeStrong : CollisionTimeWeak;
        float collidetime1 = origCollisionTime[FirstAttack];
        float collidetime2 = origCollisionTime[otherplayer];
        collidetime1 = CheckCollideTime(FirstAttack, origCollisionTime[FirstAttack]);
        collidetime2 = CheckCollideTime(otherplayer, origCollisionTime[otherplayer]);
        if (asi[FirstAttack].normalizedTime > collidetime1 && calculateCollision[FirstAttack])
        {

            calculateCollision[FirstAttack] = false;
            if (CheckDistance(FirstAttack))
            {
                if (CheckHeight(FirstAttack))
                {
                    CalculateStrength(FirstAttack);
                    if (!miss)
                    {
                        if (CheckQuard(FirstAttack))
                        {
                            cd.NoDamage(FirstAttack);
                            DeflectAttacker(FirstAttack);
                            calculateCollision[otherplayer] = false;
                        }
                        else
                        {
                            cd.ApplyDamage(FirstAttack);
                            QuardBreakAttacker(FirstAttack);
                            calculateCollision[otherplayer] = false;
                        }
                        AudioManager.instance.PlaySwordClashSound();
                    }
                    else
                    {
                        cd.ApplyDamage(FirstAttack);
                        OverExtend(FirstAttack);
                        AudioManager.instance.PlaySwordsSwingSound();
                    }
                }
            }
        }
        if (asi[otherplayer].normalizedTime > collidetime2 && calculateCollision[otherplayer])
        {

            calculateCollision[otherplayer] = false;
            if (CheckDistance(otherplayer))
            {
                if (CheckHeight(otherplayer))
                {
                    CalculateStrength(otherplayer);
                    if (!miss)
                    {
                        if (CheckQuard(otherplayer))
                        {
                            cd.NoDamage(otherplayer);
                            DeflectAttacker(otherplayer);
                        }
                        else
                        {
                            cd.ApplyDamage(otherplayer);
                            QuardBreakAttacker(otherplayer);
                        }
                        AudioManager.instance.PlaySwordClashSound();
                    }
                    else
                    {
                        cd.ApplyDamage(otherplayer);
                        OverExtend(otherplayer);
                        AudioManager.instance.PlaySwordsSwingSound();
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
        if (!CheckQuard(player))
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
    
    bool CheckQuard(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;

        float direction;
        direction = inside[player] * 2 - 1;

        float guard;
        guard = hanging[otherplayer] == inside[otherplayer] ? 1 : -1;

        if(direction == guard)
        {
            return true;
        }
        return false;
    }
    bool CheckDistance(int player)
    {
        StepDistance = -sc.GetStepDistance();

        int otherplayer = player - 1 == -1 ? 1 : 0;
        if(hanging[otherplayer] == 1)
        {
            NoGuardCollision = StepDistance >= 10 ? true : false;
            NoStrongCollision = StepDistance >= 10 ? true : false;
            NoCollision = StepDistance >= 10 ? true : false;
        }
        else
        {
            NoGuardCollision = StepDistance >= 10 ? true : false;
            NoStrongCollision = StepDistance >= 10 ? true : false;
            NoCollision = StepDistance >= 12 ? true : false;
        }
        
        return true;
    }
    bool CheckHeight(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;
        if(hanging[otherplayer] == 1)
        {
            if (hc.GetHeightOffset() + (height[player]* heightOffsetMult + lungeOffset[player]) < hc.GetBaseY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) > hc.GetMiddleY(otherplayer + 1) && !NoStrongCollision)
            {
                strongCollision = true;
                miss = false;
                handGuardHit = false;
            }
            else if (hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) < hc.GetHandleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) > hc.GetBaseY(otherplayer + 1) && !NoGuardCollision)
            {
                strongCollision = true;
                handGuardHit = true;
                miss = false;
            }
            else if (hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) < hc.GetMiddleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) > hc.GetTipY(otherplayer + 1) && !NoCollision)
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
            if (hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) > hc.GetBaseY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) < hc.GetMiddleY(otherplayer + 1) && !NoStrongCollision)
            {
                strongCollision = true;
                miss = false;
                handGuardHit = false;
            }
            else if (hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) > hc.GetHandleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) < hc.GetBaseY(otherplayer + 1) && !NoGuardCollision)
            {
                strongCollision = true;
                handGuardHit = true;
                miss = false;
            }
            else if (hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) > hc.GetMiddleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] * heightOffsetMult + lungeOffset[player]) < hc.GetTipY(otherplayer + 1) && !NoCollision)
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

    float CheckCollideTime(int player, float collideTime)
    {

        if (CheckQuard(player))
        {
            if(CheckHeightCollideTimeMult(player) != 0)
            {
                collideTime = collideTime * CheckDistanceCollideTimeMult(player);
            }
        }
        return collideTime;
    }
    float CheckDistanceCollideTimeMult(int player)
    {
        float mult = 1;
        StepDistance = -sc.GetStepDistance();
        StepDistance = StepDistance / 2;
        int otherplayer = player - 1 == -1 ? 1 : 0;
        if (hanging[otherplayer] == 1)
        {
            switch (StepDistance)
            {
                case 1:
                    mult = 0.3f;
                    break;
                case 2:
                    mult = 0.5f;
                    break;
                case 3:
                    mult = 0.8f;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (StepDistance)
            {
                case 1:
                    mult = 0.3f;
                    break;
                case 2:
                    mult = 0.5f;
                    break;
                case 3:
                    mult = 0.8f;
                    break;
                default:
                    break;
            }
        }

        return mult;
    }
    float CheckHeightCollideTimeMult(int player)
    {
        float mult = 1;
        int otherplayer = player - 1 == -1 ? 1 : 0;
        if (hanging[otherplayer] == 1)
        {
            if (hc.GetHeightOffset() + (height[player] + lungeOffset[player]) < hc.GetBaseY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] + lungeOffset[player]) > hc.GetMiddleY(otherplayer + 1) && !NoStrongCollision)
            {
                //strong
            }
            else if (hc.GetHeightOffset() + (height[player] + lungeOffset[player]) < hc.GetHandleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] + lungeOffset[player]) > hc.GetBaseY(otherplayer + 1) && !NoGuardCollision)
            {
                //guard
            }
            else if (hc.GetHeightOffset() + (height[player] + lungeOffset[player]) < hc.GetMiddleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] + lungeOffset[player]) > hc.GetTipY(otherplayer + 1) && !NoCollision)
            {
                //weak
            }
            else
            {
                mult = 0;
            }
        }
        else
        {
            if (hc.GetHeightOffset() + (height[player] + lungeOffset[player]) > hc.GetBaseY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] + lungeOffset[player]) < hc.GetMiddleY(otherplayer + 1) && !NoStrongCollision)
            {
                
            }
            else if (hc.GetHeightOffset() + (height[player] + lungeOffset[player]) > hc.GetHandleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] + lungeOffset[player]) < hc.GetBaseY(otherplayer + 1) && !NoGuardCollision)
            {
                
            }
            else if (hc.GetHeightOffset() + (height[player] + lungeOffset[player]) > hc.GetMiddleY(otherplayer + 1) && hc.GetHeightOffset() + (height[player] + lungeOffset[player]) < hc.GetTipY(otherplayer + 1) && !NoCollision)
            {
                
            }
            else
            {
                mult = 0;
            }
        }

        return mult;
    }

    void Deflect(int player)
    {
        
        int otherplayer = player - 1 == -1 ? 1 : 0;
        SetInterruptTimer(player, 0.1f + collisionStrength / 100);
        SetInterruptTimer(otherplayer, 0.1f + collisionStrength / 100);
        anim[player].SetBool("ALight", true);
        anim[otherplayer].SetBool("Deflect", true);
        if (!strongCollision)
        {
            anim[otherplayer].SetBool("light", true);
            MakeSparks(hc.GetTip(otherplayer+1), transform.rotation);
        }
        else
        {
            anim[otherplayer].SetBool("light", false);
            MakeSparks(hc.GetMiddle(otherplayer+1), transform.rotation);
        }

        if(!earned)
        {
            if(player == 0)
                deflectsP1++;
            else if(player == 1)
                deflectsP2++;
        }
        
    }
    void QuardBreak(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;
        anim[player].SetBool("AExtend", true);
        anim[otherplayer].SetBool("Interrupt", true);
        if (!strongCollision)
        {
            anim[otherplayer].SetBool("light", true);
            MakeSparks(hc.GetTip(otherplayer+1), transform.rotation);
        }
        else
        {
            anim[otherplayer].SetBool("light", false);
            MakeSparks(hc.GetMiddle(otherplayer+1), transform.rotation);
        }
        if (handGuardHit)
        {
            cd.DoDamage(CollisionDamage.Bodyparts.Hand, 1, otherplayer);
        }
        SetInterruptTimer(player, 0.1f + collisionStrength /2 / 100);
        SetInterruptTimer(otherplayer, 0.1f + collisionStrength / 100);

        if (player == 0)
            deflectsP1 = 0;
        else if (player == 1)
            deflectsP2 = 0;
    }
    void DeflectAttacker(int player)
    {

        int otherplayer = player - 1 == -1 ? 1 : 0;
        SetInterruptTimer(player, 0.1f + collisionStrength / 100);
        SetInterruptTimer(otherplayer, 0.05f + collisionStrength / 100);
        anim[player].SetBool("ALight", true);
        
        if (!strongCollision)
        {
            anim[otherplayer].SetBool("ALight", true);
            MakeSparks(hc.GetTip(otherplayer + 1), transform.rotation);
        }
        else
        {
            anim[otherplayer].SetBool("ADeflect", true);
            MakeSparks(hc.GetMiddle(otherplayer + 1), transform.rotation);
        }
    }
    void QuardBreakAttacker(int player)
    {
        int otherplayer = player - 1 == -1 ? 1 : 0;
        anim[player].SetBool("AExtend", true);
        anim[otherplayer].SetBool("AExtend", true);
        //if (!strongCollision)
        //{
        //    anim[otherplayer].SetBool("ALight", true);
        //    MakeSparks(hc.GetTip(otherplayer + 1), transform.rotation);
        //}
        //else
        //{
        //    anim[otherplayer].SetBool("light", false);
        //    MakeSparks(hc.GetMiddle(otherplayer + 1), transform.rotation);
        //}
        if (handGuardHit)
        {
            cd.DoDamage(CollisionDamage.Bodyparts.Hand, 1, otherplayer);
        }
        SetInterruptTimer(player, 0.05f + collisionStrength / 2 / 100);
        SetInterruptTimer(otherplayer, 0.1f + collisionStrength / 100);
    }
    void OverExtend(int player)
    {
        SetInterruptTimer(player,0.1f + collisionStrength / 100);
        anim[player].SetBool("AExtend", true);
    }

    void SetInterruptTimer(int player, float time)
    {
        interruptTimer[player] = time;
        //origSpeed[player] = anim[player].speed;
    }
    void Timer()
    {
        
        for(int i = 0; i < 2; i++)
        {
            float SlowSpeed;
            if (interruptTimer[i] > 0)
            {
                interruptTimer[i] -= Time.deltaTime;
                SlowSpeed = origSpeed[i] * SlowMoMult;
                anim[i].speed = SlowSpeed;
                ph[i].swoosh = false;
            }
            else
            {
                anim[i].speed = origSpeed[i];
                interruptTimer[i] = 0;
                anim[i].SetBool("Deflect", false);
                anim[i].SetBool("Interrupt", false);
                anim[i].SetBool("light", false);
                anim[i].SetBool("ADeflect", false);
                anim[i].SetBool("AExtend", false);
                anim[i].SetBool("ALight", false);
                ph[i].swoosh = true;
                
            }
        }
    }

    public float GetHeight(int player)
    {
        return height[player - 1];
    }

    public void SummonBlood(Vector3 position, Quaternion rotation)
    {
        ph[0].InstantiateBlood(position, rotation);
    }
    public void MakeSparks(Vector3 position, Quaternion rotation)
    {
        ph[0].InstantiateSpark(position, rotation);
    }

}
