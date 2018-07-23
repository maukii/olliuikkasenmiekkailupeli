using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInputLayout : MonoBehaviour
{
    
    public int playerIndex;
    public int controllerLayout;

    public Text P1_Layout;
    public Text P2_Layout;

    void Start ()
    {
        playerIndex = GetComponentInChildren<HandAnimationControl>().PlayerNumber;
    }
	
	void Update ()
    {
        controllerLayout = GetComponentInChildren<HandAnimationControl>().controllerLayout;

        if (playerIndex == 1)
        {
            P1_Layout.text = "" + controllerLayout;
        }
        else if(playerIndex == 2)
        {
            P2_Layout.text = "" + controllerLayout;
        }
	}
}
