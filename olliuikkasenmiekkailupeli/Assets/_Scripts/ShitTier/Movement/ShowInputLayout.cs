using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInputLayout : MonoBehaviour
{
    
    public int playerIndex;
    public int controllerLayout;

    public Text Left_Layout;
    public Text Right_Layout;

    void Start ()
    {
        playerIndex = GetComponentInChildren<HandAnimationControl>().PlayerNumber;
    }
	
	void Update ()
    {
        controllerLayout = GetComponentInChildren<HandAnimationControl>().GetControllerLayout();

        #region PlayerSides
        if (playerIndex == 1 && InputManager.IM.isLeftP1)
        {
            Left_Layout.text = "" + controllerLayout;
        }
        else if(playerIndex == 1 && InputManager.IM.isRightP1)
        {
            Right_Layout.text = "" + controllerLayout;
        }
        else if(playerIndex == 2 && InputManager.IM.isLeftP2)
        {
            Left_Layout.text = "" + controllerLayout;
        }
        else if(playerIndex == 2 && InputManager.IM.isRightP2)
        {
            Right_Layout.text = "" + controllerLayout;
        }
        #endregion

    }
}
