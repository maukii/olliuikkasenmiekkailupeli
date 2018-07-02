using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public float horL, horR, horDpad, verL, verR, verDpad, LT, RT;
    public bool buttonLB, buttonRB, buttonA, buttonB, buttonX, buttonY, buttonStart;

    void Start()
    {
        
    }

    void Update()
    {
        SelectInput();

        //Huom! Triggereissä arvot Xbox-ohjaimilla 0 -> 1 ja PS4-ohjaimilla -1 -> 1
        //Rajoita myös pelaajien min. ja max. distancea toisistaan
    }

    void SelectInput()
    {
        if (this.gameObject.tag == "Player 1")
        {
            horL = InputManager.IM.P1_LS_X;
            verL = InputManager.IM.P1_LS_Y;
            horDpad = InputManager.IM.P1_Dpad_X;
            horR = InputManager.IM.P1_RS_X;
            verR = InputManager.IM.P1_RS_Y;
            verDpad = InputManager.IM.P1_Dpad_Y;
            LT = InputManager.IM.P1_LT;
            RT = InputManager.IM.P1_RT;
            buttonLB = InputManager.IM.P1_LB;
            buttonRB = InputManager.IM.P1_RB;
            buttonA = InputManager.IM.P1_A;
            buttonB = InputManager.IM.P1_B;
            buttonX = InputManager.IM.P1_X;
            buttonY = InputManager.IM.P1_Y;
            buttonStart = InputManager.IM.P1_Start;
        }

        if (this.gameObject.tag == "Player 2")
        {
            horL = InputManager.IM.P2_LS_X;
            verL = InputManager.IM.P2_LS_Y;
            horDpad = InputManager.IM.P2_Dpad_X;
            horR = InputManager.IM.P2_RS_X;
            verR = InputManager.IM.P2_RS_Y;
            verDpad = InputManager.IM.P2_Dpad_Y;
            LT = InputManager.IM.P2_LT;
            RT = InputManager.IM.P2_RT;
            buttonLB = InputManager.IM.P2_LB;
            buttonRB = InputManager.IM.P2_RB;
            buttonA = InputManager.IM.P2_A;
            buttonB = InputManager.IM.P2_B;
            buttonX = InputManager.IM.P2_X;
            buttonY = InputManager.IM.P2_Y;
            buttonStart = InputManager.IM.P2_Start;
        }
    }
}