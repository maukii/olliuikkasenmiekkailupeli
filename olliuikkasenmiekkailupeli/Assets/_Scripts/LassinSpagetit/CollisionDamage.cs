using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {

    [SerializeField]
    GameObject P1, P2, swordPrefab;

    bool throat;

    CollisionHandler ch;

    public enum Bodyparts { Head, Torso, Leg, Arm, Hand }
    Bodyparts bodyparthit;

    [SerializeField]
    int[] HeadTreshold = new int[2] { 2, 2 };
    [SerializeField]
    int[] TorsoTreshold = new int[2] { 2, 2 };
    [SerializeField]
    int[] LegTreshold = new int[2] { 3, 3 };
    [SerializeField]
    int[] ArmTreshold = new int[2] { 3, 3 };
    [SerializeField]
    int[] HandTreshold = new int[2] { 4, 4 };

    int player = -1;
    bool[] applyDamage = new bool[2];
    int[] AttackStrength = new int[2];

    private void Start()
    {
        throat = false;

        ch = GetComponent<CollisionHandler>();
        P1 = GameObject.FindGameObjectWithTag("Player 1");
        P2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    public void ApplyDamage(int player, int Damage)
    {
        applyDamage[player] = true;
        AttackStrength[player] = Damage;
        this.player = player;
    }
    public void NoDamage(int player)
    {
        applyDamage[player] = false;
        AttackStrength[player] = 0;
    }
    public void DissableDamageTo(Bodyparts part)
    {
        switch (part)
        {
            case Bodyparts.Head:
                HeadTreshold[0] = -100;
                HeadTreshold[1] = -100;
                break;
            case Bodyparts.Torso:
                TorsoTreshold[0] = -100;
                TorsoTreshold[1] = -100;
                break;
            case Bodyparts.Leg:
                LegTreshold[0] = -100;
                LegTreshold[1] = -100;
                break;
            case Bodyparts.Arm:
                ArmTreshold[0] = -100;
                ArmTreshold[1] = -100;
                break;
            case Bodyparts.Hand:
                HandTreshold[0] = -100;
                HandTreshold[1] = -100;
                break;
        }
    }
    public void GetCollision(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (player != -1)
        {
            if (applyDamage[player])
            {
                int otherplayer = player - 1 == -1 ? 1 : 0;
                applyDamage[player] = false;
                switch (col.gameObject.name)
                {
                    case "alaselkä.L":
                        DoDamage(Bodyparts.Torso, AttackStrength[player], otherplayer);
                        break;
                    case "reisi.R":
                        DoDamage(Bodyparts.Leg, AttackStrength[player], otherplayer);
                        break;
                    case "pohje.R":
                        DoDamage(Bodyparts.Leg, AttackStrength[player], otherplayer);
                        break;
                    case "selkä.L":
                        DoDamage(Bodyparts.Torso, AttackStrength[player], otherplayer);
                        break;
                    case "selkä.L.001":
                        DoDamage(Bodyparts.Torso, AttackStrength[player], otherplayer);
                        break;
                    case "hauis.R":
                        DoDamage(Bodyparts.Arm, AttackStrength[player], otherplayer);
                        break;
                    case "ranne.R":
                        DoDamage(Bodyparts.Arm, AttackStrength[player], otherplayer);
                        break;
                    case "pää":
                        DoDamage(Bodyparts.Head, AttackStrength[player], otherplayer);
                        break;
                    default:
                        break;
                }
                ch.SummonBlood(col.contacts[0].point, Quaternion.FromToRotation(transform.up, col.contacts[0].normal));

                if (player == 0)
                    CollisionHandler.deflectsP1 = 0;
                else if (player == 1)
                    CollisionHandler.deflectsP2 = 0;

            }
            
        }
    }
    public void DoDamage(Bodyparts part, int amount, int player)
    {
        this.player = -1;
        switch (part)
        {
            case Bodyparts.Head:
                HeadTreshold[player] -= amount;
                break;
            case Bodyparts.Torso:
                TorsoTreshold[player] -= amount;
                break;
            case Bodyparts.Leg:
                LegTreshold[player] -= amount;
                break;
            case Bodyparts.Arm:
                ArmTreshold[player] -= amount;
                break;
            case Bodyparts.Hand:
                HandTreshold[player] -= amount;
                break;
        }
        PlayDamageAnimation(part, player);
        CheckHealth(player);
    }

    void PlayDamageAnimation(Bodyparts body, int player)
    {
        //Play

        int bodypart = (int)body;
        var anim = (player == 0 ? P1 : P2).GetComponent<AlternativeMovement5>().GetActiveAnimator();
        anim.SetInteger("Bodypart", bodypart);
        anim.SetTrigger("TakeDamage");
        AudioManager.instance.PlayLightHitSound();
        Debug.Log("osuma animaatio");
    }

    void CheckHealth(int player)
    {
        if (HeadTreshold[player] <= 0 && HeadTreshold[player] >= -99) Die(Bodyparts.Head, player);
        if (TorsoTreshold[player] <= 0 && HeadTreshold[player] >= -99) Die(Bodyparts.Torso, player);
        if (LegTreshold[player] <= 0 && HeadTreshold[player] >= -99) Die(Bodyparts.Leg, player);
        if (ArmTreshold[player] <= 0 && HeadTreshold[player] >= -99) Die(Bodyparts.Arm, player);
        if (HandTreshold[player] <= 0 && HeadTreshold[player] >= -99) Die(Bodyparts.Hand, player);
        if(HandTreshold[player] == 1 && ArmTreshold[player] == 1 && LegTreshold[player] == 1 && TorsoTreshold[player] == 1 && HeadTreshold[player] == 1)
        {
            Debug.Log("let him die!");
            AchievementManager.instance.SetProgressToAchievement("Death by thousand cuts", 1);
        }

    }

    void Die(Bodyparts part, int player)
    {
        if (!TutorialManager.TM.deathLock)
        {
            int playerModel = (player == 0 ? GameHandler.instance.GetPlayer1Model() : GameHandler.instance.GetPlayer2Model());
            var animator = (player == 0 ? P1 : P2).GetComponent<AlternativeMovement5>().GetActiveAnimator();

            switch (part)
            {
                case Bodyparts.Head:
                    Debug.Log("pää kuolema");
                    AchievementManager.instance.SetProgressToAchievement("Headshot", 1);
                    if(player == 0)
                    {                      
                        if (animator.GetInteger("StartAnim") == 0)
                        {
                            animator.SetBool("throat", true);
                        }
                    }
                    else if(player == 1)
                    {
                        if (animator.GetInteger("StartAnim") == 0)
                        {
                            animator.SetBool("throat", true);
                        }
                    }
                    throat = animator.GetBool("throat");
                    break;
                case Bodyparts.Torso:
                    Debug.Log("Perus Död");
                    break;
                case Bodyparts.Leg:
                    Debug.Log("jalaka hajoaa");
                    break;
                case Bodyparts.Arm:
                    Debug.Log("ase putoaa");
                    AchievementManager.instance.SetProgressToAchievement("Disarmed", 1);
                    break;
                case Bodyparts.Hand:
                    Debug.Log("ase putoaa");
                    AchievementManager.instance.SetProgressToAchievement("Disarmed", 1);
                    break;
            }
            Debug.Log("död");

            int bodypart = (int)part;

            var anim = (player == 0 ? P1 : P2).GetComponent<AlternativeMovement5>().GetActiveAnimator();
            anim.SetInteger("Bodypart", bodypart);
            anim.SetTrigger("Die");
            AudioManager.instance.PlaySoundeffect("Heavy hit placeholder");
            AudioManager.instance.PlaySoundeffect("Touche");

            // achievement stuff
            int gamesPlayed = PlayerPrefs.GetInt("gamesPlayed");
            PlayerPrefs.SetInt("gamesPlayed", gamesPlayed++);
            AchievementManager.instance.AddProgressToAchievement("Master the blade", 1);

            if (bodypart > 2 || throat/* && playerModel != 2 && playerModel != 5 */)
            {
                DeactivateSword(player);
                DropAnim(player);
            }
        }
    }

    private void DeactivateSword(int player)
    {
        (player == 0 ? P1 : P2).GetComponent<DeactiveSwords>().DeactivateSowrds(player);
    }

    private void DropAnim(int player)
    {
        var playerPos = (player == 0 ? P1 : P2);
        var sword = Instantiate(swordPrefab, playerPos.transform.position, playerPos.transform.rotation);
        var playerModel = playerPos.GetComponent<AlternativeMovement5>().GetActiveAnimator();

        sword.transform.position = playerModel.transform.position;
        sword.GetComponent<Animator>().SetTrigger("Drop");
    }

}
