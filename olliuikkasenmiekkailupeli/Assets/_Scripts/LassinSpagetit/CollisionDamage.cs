using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour {

    CollisionHandler ch;
    public enum Bodyparts { Head, Torso, Leg, Arm, Hand }
    int[] HeadTreshold = new int[2] { 2, 2 };
    int[] TorsoTreshold = new int[2] { 2, 2 };
    int[] LegTreshold = new int[2] { 3, 3 };
    int[] ArmTreshold = new int[2] { 3, 3 };
    int[] HandTreshold = new int[2] { 4, 4 };
    int player = -1;

    private void Start()
    {
        ch = GetComponent<CollisionHandler>();
    }

    public void StartCollisionDetection(int player)
    {
        this.player = player;
    }

    public void GetCollision(Collision col)
    {
        if (player != -1)
        {
            switch (col.gameObject.name)
            {
                case "alaselkä.L":
                    DoDamage(Bodyparts.Torso, 1, player);
                    break;
                case "reisi.R":
                    DoDamage(Bodyparts.Leg, 1, player);
                    break;
                case "pohje.R":
                    DoDamage(Bodyparts.Leg, 1, player);
                    break;
                case "selkä.L":
                    DoDamage(Bodyparts.Torso, 1, player);
                    break;
                case "selkä.L.001":
                    DoDamage(Bodyparts.Torso, 1, player);
                    break;
                case "hauis.R":
                    DoDamage(Bodyparts.Arm, 1, player);
                    break;
                case "ranne.R":
                    DoDamage(Bodyparts.Arm, 1, player);
                    break;
                case "pää":
                    DoDamage(Bodyparts.Head, 1, player);
                    break;
                default:
                    break;
            }
            ch.SummonBlood(col.contacts[0].point, Quaternion.FromToRotation(transform.up, col.contacts[0].normal));
        }
    }
    void DoDamage(Bodyparts part, int amount, int player)
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
        if (HeadTreshold[player] <= 0) Die();
        if (TorsoTreshold[player] <= 0) Die();
        if (LegTreshold[player] <= 0) Die();
        if (ArmTreshold[player] <= 0) Die();
        if (HandTreshold[player] <= 0) Die();

    }

    void Die()
    {
        //Play ja kerro että peli loppu
        Debug.Log("död");
    }
}
