using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{

    [Header("Player 1")]
    public string P1_Hor;
    public string P1_Ver;
    public float P1_LS_X;
    public float P1_LS_Y;
    public float P1_RS_X;
    public float P1_RS_Y;
    public float P1_Dpad_X;
    public float P1_Dpad_Y;
    public float P1_LT;
    public float P1_RT;
    public float P1_Triggers;
    public bool P1_LB;
    public bool P1_RB;
    public bool P1_A;
    public bool P1_B;
    public bool P1_X;
    public bool P1_Y;
    public bool P1_Start;

    [Header("Player 2")]
    public string P2_Hor;
    public string P2_Ver;
    public float P2_LS_X;
    public float P2_LS_Y;
    public float P2_RS_X;
    public float P2_RS_Y;
    public float P2_Dpad_X;
    public float P2_Dpad_Y;
    public float P2_LT;
    public float P2_RT;
    public float P2_Triggers;
    public bool P2_LB;
    public bool P2_RB;
    public bool P2_A;
    public bool P2_B;
    public bool P2_X;
    public bool P2_Y;
    public bool P2_Start;

    #region InputGets
    public string GetHor(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_Hor;
            case 2:
                return P2_Hor;
        }
        return "";
    }
    public string GetVer(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_Ver;
            case 2:
                return P2_Ver;
        }
        return "";
    }
    public float GetLS_X(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_LS_X;
            case 2:
                return P2_LS_X;
        }
        return 0;
    }
    public float GetLS_Y(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_LS_Y;
            case 2:
                return P2_LS_Y;
        }
        return 0;
    }
    public float GetRS_X(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_RS_X;
            case 2:
                return P2_RS_X;
        }
        return 0;
    }
    public float GetRS_Y(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_RS_Y;
            case 2:
                return P2_RS_Y;
        }
        return 0;
    }
    public float GetDpad_X(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_Dpad_X;
            case 2:
                return P2_Dpad_X;
        }
        return 0;
    }
    public float GetDpad_Y(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_Dpad_Y;
            case 2:
                return P2_Dpad_Y;
        }
        return 0;
    }
    public float GetLT(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_LT;
            case 2:
                return P2_LT;
        }
        return 0;
    }
    public float GetRT(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_RT;
            case 2:
                return P2_RT;
        }
        return 0;
    }
    public float GetTriggers(int PlayerNumber)
    {
        switch(PlayerNumber)
        {
            case 1:
                return P1_Triggers;
            case 2:
                return P2_Triggers;
        }
        return 0;
}
    public bool GetA(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_A;
            case 2:
                return P2_A;
        }
        return false;
    }
    public bool GetB(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_B;
            case 2:
                return P2_B;
        }
        return false;
    }
    public bool GetX(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_X;
            case 2:
                return P2_X;
        }
        return false;
    }
    public bool GetY(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_Y;
            case 2:
                return P2_Y;
        }
        return false;
    }
    public bool GetLB(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_LB;
            case 2:
                return P2_LB;
        }
        return false;
    }
    public bool GetRB(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_RB;
            case 2:
                return P2_RB;
        }
        return false;
    }
    public bool GetStart(int PlayerNumber)
    {
        switch (PlayerNumber)
        {
            case 1:
                return P1_Start;
            case 2:
                return P2_Start;
        }
        return false;
    }
    #endregion

    [Header("Controller Check")]
    public bool isXboxControllerP1;
    public bool isXboxControllerP2;
    public bool isPSControllerP1;
    public bool isPSControllerP2;
    public bool isKeyboardAndMouseP1;
    public bool isKeyboardAndMouseP2;
    public bool isOnlyKeyboard;

    public bool isLeftP1;
    public bool isLeftP2;
    public bool isRightP1;
    public bool isRightP2;

    public static InputManager IM;

    void Awake()
    {
        if (IM == null)
        {
            DontDestroyOnLoad(gameObject);
            IM = this;
        }

        else if (IM != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ControllerCheck();
    }

    void Update()
    {
        ControllerCheck();
        ChooseInput();
        SideCheck();
    }
    
    void ControllerCheck()
    {
        string[] names = Input.GetJoystickNames();

        if (names.Length > 0)
        {
            if (names[0].Contains("XBOX 360") || names[0].Contains("Xbox One") || names[0].Contains("Controller (XBOX For windows") || names[0].Contains("Controller (Gamepad F310)"))
            {
                Debug.LogWarning(names[0] + " detected as joystick 1");
                isXboxControllerP1 = true;
                isKeyboardAndMouseP1 = false;
            }

            if (names[0].Contains("PLAYSTATION") || names[0].Contains("Wireless Controller"))
            {
                Debug.LogWarning(names[0] + " detected as joystick 1");
                isPSControllerP1 = true;
                isKeyboardAndMouseP1 = false;
            }

            if(names.Length > 1)
            {
                if (names[1].Contains("XBOX 360") || names[1].Contains("Xbox One") || names[1].Contains("Controller (XBOX For windows") || names[1].Contains("Controller (Gamepad F310)"))
                {
                    Debug.LogWarning(names[1] + " detected as joystick 2");
                    isXboxControllerP2 = true;
                    isKeyboardAndMouseP2 = false;
                }

                if (names[1].Contains("PLAYSTATION") || names[1].Contains("Wireless Controller"))
                {
                    Debug.LogWarning(names[1] + " detected as joystick 2");
                    isPSControllerP2 = true;
                    isKeyboardAndMouseP2 = false;
                }

                if (names[0].Length == 0 && names[1].Length > 0)
                {
                    Debug.LogWarning("Can't find joystick 1, using keyboard and mouse");
                    isXboxControllerP1 = false;
                    isPSControllerP1 = false;
                    isKeyboardAndMouseP1 = true;
                }

                if (names[1].Length == 0 && names[0].Length > 0)
                {
                    Debug.LogWarning("Can't find joystick 2, using keyboard and mouse");
                    isXboxControllerP2 = false;
                    isPSControllerP2 = false;
                    isKeyboardAndMouseP2 = true;
                }
            }

            if(names.Length == 1) // P2 no controller
            {
                isXboxControllerP2 = false;
                isPSControllerP2 = false;
                isKeyboardAndMouseP2 = true;
            }

        }

        if (names.Length == 0)
        {
            Debug.LogWarning("No controllers found, using keyboard");
            isXboxControllerP1 = false;
            isXboxControllerP2 = false;
            isPSControllerP1 = false;
            isPSControllerP2 = false;
            isKeyboardAndMouseP1 = false;
            isKeyboardAndMouseP2 = false;
            isOnlyKeyboard = true;
        }
    }

    void ChooseInput()
    {
        if (isXboxControllerP1)
        {
            P1_Hor = "Xbox_P1_HorizontalLeft";
            P1_Ver = "Xbox_P1_VerticalRight";
            P1_LS_X = Input.GetAxis("Xbox_P1_HorizontalLeft");
            P1_LS_Y = Input.GetAxis("Xbox_P1_VerticalLeft");
            P1_RS_X = Input.GetAxis("Xbox_P1_HorizontalRight");
            P1_RS_Y = Input.GetAxis("Xbox_P1_VerticalRight");
            P1_Dpad_X = Input.GetAxis("Xbox_P1_HorizontalDpad");
            P1_Dpad_Y = Input.GetAxis("Xbox_P1_VerticalDpad");
            P1_LT = Input.GetAxis("Xbox_P1_LT"); 
            P1_RT = Input.GetAxis("Xbox_P1_RT"); 
            P1_Triggers = Input.GetAxis("Xbox_P1_LT_RT");
            P1_LB = Input.GetKey(KeyCode.Joystick1Button4);
            P1_RB = Input.GetKey(KeyCode.Joystick1Button5);
            P1_A = Input.GetKey(KeyCode.Joystick1Button0);
            P1_B = Input.GetKey(KeyCode.Joystick1Button1);
            P1_X = Input.GetKey(KeyCode.Joystick1Button2);
            P1_Y = Input.GetKey(KeyCode.Joystick1Button3);
            P1_Start = Input.GetKey(KeyCode.Joystick1Button7);
        }  

        if (isXboxControllerP2)
        {
            P2_Hor = "Xbox_P2_HorizontalLeft";
            P2_Ver = "Xbox_P2_VerticalRight";
            P2_LS_X = Input.GetAxis("Xbox_P2_HorizontalLeft");
            P2_LS_Y = Input.GetAxis("Xbox_P2_VerticalLeft");
            P2_RS_X = Input.GetAxis("Xbox_P2_HorizontalRight");
            P2_RS_Y = Input.GetAxis("Xbox_P2_VerticalRight");
            P2_Dpad_X = Input.GetAxis("Xbox_P2_HorizontalDpad");
            P2_Dpad_Y = Input.GetAxis("Xbox_P2_VerticalDpad");
            P2_LT = Input.GetAxis("Xbox_P2_LT");
            P2_RT = Input.GetAxis("Xbox_P2_RT");
            P2_Triggers = Input.GetAxis("Xbox_P2_LT_RT");
            P2_LB = Input.GetKey(KeyCode.Joystick2Button4);
            P2_RB = Input.GetKey(KeyCode.Joystick2Button5);
            P2_A = Input.GetKey(KeyCode.Joystick2Button0);
            P2_B = Input.GetKey(KeyCode.Joystick2Button1);
            P2_X = Input.GetKey(KeyCode.Joystick2Button2);
            P2_Y = Input.GetKey(KeyCode.Joystick2Button3);
            P2_Start = Input.GetKey(KeyCode.Joystick2Button7);
        }

        if (isPSControllerP1)
        {
            P1_Hor = "PS_P1_HorizontalLeft";
            P1_Ver = "PS_P1_VerticalRight";
            P1_LS_X = Input.GetAxis("PS_P1_HorizontalLeft");
            P1_LS_Y = Input.GetAxis("PS_P1_VerticalLeft");
            P1_RS_X = Input.GetAxis("PS_P1_HorizontalRight");
            P1_RS_Y = Input.GetAxis("PS_P1_VerticalRight");
            P1_Dpad_X = Input.GetAxis("PS_P1_HorizontalDpad");
            P1_Dpad_Y = Input.GetAxis("PS_P1_VerticalDpad");
            P1_LT = Input.GetAxis("PS_P1_L2");
            P1_RT = Input.GetAxis("PS_P1_R2");
            P1_LB = Input.GetKey(KeyCode.Joystick1Button4);
            P1_RB = Input.GetKey(KeyCode.Joystick1Button5);
            P1_A = Input.GetKey(KeyCode.Joystick1Button1);
            P1_B = Input.GetKey(KeyCode.Joystick1Button2);
            P1_X = Input.GetKey(KeyCode.Joystick1Button0);
            P1_Y = Input.GetKey(KeyCode.Joystick1Button3);
            P1_Start = Input.GetKey(KeyCode.Joystick1Button9);
        }

        if (isPSControllerP2)
        {
            P2_Hor = "PS_P2_HorizontalLeft";
            P2_Ver = "PS_P2_VerticalRight";
            P2_LS_X = Input.GetAxis("PS_P2_HorizontalLeft");
            P2_LS_Y = Input.GetAxis("PS_P2_VerticalLeft");
            P2_RS_X = Input.GetAxis("PS_P2_HorizontalRight");
            P2_RS_Y = Input.GetAxis("PS_P2_VerticalRight");
            P2_Dpad_X = Input.GetAxis("PS_P2_HorizontalDpad");
            P2_Dpad_Y = Input.GetAxis("PS_P2_VerticalDpad");
            P2_LT = Input.GetAxis("PS_P2_L2");
            P2_RT = Input.GetAxis("PS_P2_R2");
            P2_LB = Input.GetKey(KeyCode.Joystick2Button4);
            P2_RB = Input.GetKey(KeyCode.Joystick2Button5);
            P2_A = Input.GetKey(KeyCode.Joystick2Button1);
            P2_B = Input.GetKey(KeyCode.Joystick2Button2);
            P2_X = Input.GetKey(KeyCode.Joystick2Button0);
            P2_Y = Input.GetKey(KeyCode.Joystick2Button3);
            P2_Start = Input.GetKey(KeyCode.Joystick2Button9);
        }

        if(isKeyboardAndMouseP1)
        {
            P1_Hor = "Keyboard_P1_Horizontal";
            P1_Ver = "MouseY";
            //P1_LS_X = Input.GetAxis("Xbox_P1_HorizontalLeft");
            //P1_LS_Y = Input.GetAxis("Xbox_P1_VerticalLeft");
            //P1_RS_X = Input.GetAxis("Xbox_P1_HorizontalRight");
            //P1_RS_Y = Input.GetAxis("Xbox_P1_VerticalRight");
            //P1_Dpad_X = Input.GetAxis("Xbox_P1_HorizontalDpad");
            //P1_Dpad_Y = Input.GetAxis("Xbox_P1_VerticalDpad");
            //P1_LT = Input.GetAxis("Xbox_P1_LT");
            //P1_RT = Input.GetAxis("Xbox_P1_RT");
            //P1_LB = Input.GetKey(KeyCode.Joystick1Button4);
            //P1_RB = Input.GetKey(KeyCode.Joystick1Button5);
            P1_A = Input.GetKey(KeyCode.Mouse0);
            P1_B = Input.GetKey(KeyCode.Mouse1);
            //P1_X = Input.GetKey(KeyCode.Joystick1Button2);
            //P1_Y = Input.GetKey(KeyCode.Joystick1Button3);
            //P1_Start = Input.GetKey(KeyCode.Joystick1Button7);
        }

        if(isKeyboardAndMouseP2)
        {
            P2_Hor = "Keyboard_P2_Horizontal";
            P2_Ver = "MouseY";
            //P2_LS_X = Input.GetAxis("Xbox_P2_HorizontalLeft");
            //P2_LS_Y = Input.GetAxis("Xbox_P2_VerticalLeft");
            //P2_RS_X = Input.GetAxis("Xbox_P2_HorizontalRight");
            //P2_RS_Y = Input.GetAxis("Xbox_P2_VerticalRight");
            //P2_Dpad_X = Input.GetAxis("Xbox_P2_HorizontalDpad");
            //P2_Dpad_Y = Input.GetAxis("Xbox_P2_VerticalDpad");
            //P2_LT = Input.GetAxis("Xbox_P2_LT");
            //P2_RT = Input.GetAxis("Xbox_P2_RT");
            //P2_LB = Input.GetKey(KeyCode.Joystick2Button4);
            //P2_RB = Input.GetKey(KeyCode.Joystick2Button5);
            P2_A = Input.GetKey(KeyCode.Mouse0);
            P2_B = Input.GetKey(KeyCode.Mouse1);
            //P2_X = Input.GetKey(KeyCode.Joystick2Button2);
            //P2_Y = Input.GetKey(KeyCode.Joystick2Button3);
            //P2_Start = Input.GetKey(KeyCode.Joystick2Button7);
        }

        if (isOnlyKeyboard)
        {
            P1_Hor = "KeyboardOnly_P1_Horizontal";
            P1_Ver = "KeyboardOnly_P1_Vertical";
            P2_Hor = "KeyboardOnly_P2_Horizontal";
            P2_Ver = "KeyboardOnly_P2_Vertical";

            #region P1_KeyboardOnly_Inputs
            //P1_LS_X = Input.GetAxis("Xbox_P1_HorizontalLeft");
            //P1_LS_Y = Input.GetAxis("Xbox_P1_VerticalLeft");
            //P1_RS_X = Input.GetAxis("Xbox_P1_HorizontalRight");
            //P1_RS_Y = Input.GetAxis("Xbox_P1_VerticalRight");
            //P1_Dpad_X = Input.GetAxis("Xbox_P1_HorizontalDpad");
            //P1_Dpad_Y = Input.GetAxis("Xbox_P1_VerticalDpad");
            //P1_LT = Input.GetAxis("Xbox_P1_LT");
            //P1_RT = Input.GetAxis("Xbox_P1_RT");
            //P1_LB = Input.GetKey(KeyCode.Joystick1Button4);
            //P1_RB = Input.GetKey(KeyCode.Joystick1Button5);
            P1_A = Input.GetKey(KeyCode.Q);
            P1_B = Input.GetKey(KeyCode.E);
            //P1_X = Input.GetKey(KeyCode.Joystick1Button2);
            //P1_Y = Input.GetKey(KeyCode.Joystick1Button3);
            //P1_Start = Input.GetKey(KeyCode.Joystick1Button7);
            #endregion

            #region P2_KeyboardOnly_Inputs
            //P2_LS_X = Input.GetAxis("Xbox_P2_HorizontalLeft");
            //P2_LS_Y = Input.GetAxis("Xbox_P2_VerticalLeft");
            //P2_RS_X = Input.GetAxis("Xbox_P2_HorizontalRight");
            //P2_RS_Y = Input.GetAxis("Xbox_P2_VerticalRight");
            //P2_Dpad_X = Input.GetAxis("Xbox_P2_HorizontalDpad");
            //P2_Dpad_Y = Input.GetAxis("Xbox_P2_VerticalDpad");
            //P2_LT = Input.GetAxis("Xbox_P2_LT");
            //P2_RT = Input.GetAxis("Xbox_P2_RT");
            //P2_LB = Input.GetKey(KeyCode.Joystick2Button4);
            //P2_RB = Input.GetKey(KeyCode.Joystick2Button5);
            P2_A = Input.GetKey(KeyCode.KeypadEnter);
            P2_B = Input.GetKey(KeyCode.Keypad0);
            //P2_X = Input.GetKey(KeyCode.Joystick2Button2);
            //P2_Y = Input.GetKey(KeyCode.Joystick2Button3);
            //P2_Start = Input.GetKey(KeyCode.Joystick2Button7);
            #endregion
        }

    }

    void SideCheck()
    {
        if(SceneManager.GetActiveScene().name == "MaunoManu") // MIGHT WANNA CHANGE
        {
            isLeftP1 = TestMenuScript.MS.isLeftP1;
            isLeftP2 = TestMenuScript.MS.isLeftP2;
            isRightP1 = TestMenuScript.MS.isRightP1;
            isRightP2 = TestMenuScript.MS.isRightP2;
        }
    }

    public void SetCorrectInputs()
    {
        GameObject.Find("P1").GetComponent<AlternativeMovement5>().SetInputAxis(P1_Hor, P1_Ver);
        GameObject.Find("P2").GetComponent<AlternativeMovement5>().SetInputAxis(P2_Hor, P2_Ver);
    }

}
/*
            ---XBOX---
            LS X axis = X axis
            LS Y axis = Y axis
            RS X axis = 4th axis
            RS Y axis = 5th axis
            D-pad X axis = 6th axis
            D-pad Y axis = 7th axis
            Triggers? = 3rd axis
            LT = 9th axis
            RT = 10th axis

            A button = Button 0
            B button = Button 1
            X button = Button 2
            Y button = Button 3
            LB = Button 4
            RB = Button 5
            Back button = Button 6
            Start button = Button 7
            LS = Button 8
            RS = Button 9

            ---PS4---
            LS X axis = X axis
            LS Y axis = Y axis
            RS X axis = 3rd axis
            RS Y axis = 6th axis
            D-pad X axis = 7th axis
            D-pad Y axis = 8th axis
            L2 = 4th axis (-1.0f to 1.0f range, unpressed is -1.0f)
            R2 = 5th axis (-1.0f to 1.0f range, unpressed is -1.0f)

            Square = Button 0
            X = Button 1
            Circle = Button 2
            Triangle = Button 3
            L1 = Button 4
            R1 = Button 5
            L2 = Button 6
            R2 = Button 7
            Share = Button 8
            Options = Button 9
            L3 = Button 10
            R3 = Button 11
            PS = Button 12
            PadPress = Button 13
*/
