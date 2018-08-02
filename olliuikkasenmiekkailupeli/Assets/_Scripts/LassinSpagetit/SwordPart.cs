using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPart : MonoBehaviour {

	public void ChangePlayer(int player)
    {
        Debug.Log(player);
        if(player == 2)
        {
            gameObject.tag = "SwordPointsP2";
        }
        else
        {
            gameObject.tag = "SwordPointsP1";
        }
    }
}
