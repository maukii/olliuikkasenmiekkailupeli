using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactiveSwords : MonoBehaviour
{

    [SerializeField] GameObject[] P1_swords, P2_swords;

    public void DeactivateSowrds(int playerNumber)
    {
        if(playerNumber == 0)
        {
            for (int i = 0; i < P1_swords.Length; i++)
            {
                P1_swords[i].gameObject.SetActive(false);
            }
        }
        else if(playerNumber == 1)
        {
            for (int i = 0; i < P2_swords.Length; i++)
            {
                P2_swords[i].gameObject.SetActive(false);
            }
        }
    }
}
