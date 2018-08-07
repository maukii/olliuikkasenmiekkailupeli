using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {

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
    bool applyDamage;

    private void Start()
    {
        ch = GetComponent<CollisionHandler>();
    }

    public void StartCollisionDetection(int player)
    {
        this.player = player;
    }
    public void ApplyDamage()
    {
        applyDamage = true;
    }
    public void NoDamage()
    {
        applyDamage = false;
    }
    public void DissableDamageTo(Bodyparts part)
    {
        switch (part)
        {
            case Bodyparts.Head:
                HeadTreshold[0] = -1;
                HeadTreshold[1] = -1;
                break;
            case Bodyparts.Torso:
                TorsoTreshold[0] = -1;
                TorsoTreshold[1] = -1;
                break;
            case Bodyparts.Leg:
                LegTreshold[0] = -1;
                LegTreshold[1] = -1;
                break;
            case Bodyparts.Arm:
                ArmTreshold[0] = -1;
                ArmTreshold[1] = -1;
                break;
            case Bodyparts.Hand:
                HandTreshold[0] = -1;
                HandTreshold[1] = -1;
                break;
        }
    }
    public void GetCollision(Collision col)
    {
        Debug.Log(col.gameObject.name);
        if (player != -1 && applyDamage)
        {
            int otherplayer = player - 1 == -1 ? 1 : 0;
            switch (col.gameObject.name)
            {
                case "alaselkä.L":
                    DoDamage(Bodyparts.Torso, 1, otherplayer);
                    break;
                case "reisi.R":
                    DoDamage(Bodyparts.Leg, 1, otherplayer);
                    break;
                case "pohje.R":
                    DoDamage(Bodyparts.Leg, 1, otherplayer);
                    break;
                case "selkä.L":
                    DoDamage(Bodyparts.Torso, 1, otherplayer);
                    break;
                case "selkä.L.001":
                    DoDamage(Bodyparts.Torso, 1, otherplayer);
                    break;
                case "hauis.R":
                    DoDamage(Bodyparts.Arm, 1, otherplayer);
                    break;
                case "ranne.R":
                    DoDamage(Bodyparts.Arm, 1, otherplayer);
                    break;
                case "pää":
                    DoDamage(Bodyparts.Head, 1, otherplayer);
                    break;
                default:
                    break;
            }
            ch.SummonBlood(col.contacts[0].point, Quaternion.FromToRotation(transform.up, col.contacts[0].normal));
            applyDamage = false;
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
        Debug.Log("osuma animaatio");
    }

    void CheckHealth(int player)
    {
        if (HeadTreshold[player] == 0) Die(Bodyparts.Head);
        if (TorsoTreshold[player] == 0) Die(Bodyparts.Torso);
        if (LegTreshold[player] == 0) Die(Bodyparts.Leg);
        if (ArmTreshold[player] == 0) Die(Bodyparts.Arm);
        if (HandTreshold[player] == 0) Die(Bodyparts.Hand);
        if(HandTreshold[player] == 1 && ArmTreshold[player] == 1 && LegTreshold[player] == 1 && TorsoTreshold[player] == 1 && HeadTreshold[player] == 1)
        {
            Debug.Log("let him die!");
        }

    }

    void Die(Bodyparts part)
    {
        switch (part)
        {
            case Bodyparts.Head:
                Debug.Log("pää kuolema");
                break;
            case Bodyparts.Torso:
                Debug.Log("Perus Död");
                break;
            case Bodyparts.Leg:
                Debug.Log("jalaka hajoaa");
                break;
            case Bodyparts.Arm:
                Debug.Log("ase putoaa");
                break;
            case Bodyparts.Hand:
                Debug.Log("ase putoaa");
                break;
        }
        Debug.Log("död");
    }
}
