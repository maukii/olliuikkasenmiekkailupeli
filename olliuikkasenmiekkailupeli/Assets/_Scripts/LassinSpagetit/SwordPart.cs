using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPart : MonoBehaviour {

	public void ChangePlayer(int player)
    {
        if(player == 2)
        {
            gameObject.tag = "SwordPointP2";
        }
        else
        {
            gameObject.tag = "SwordPointP1";
        }
    }
}
