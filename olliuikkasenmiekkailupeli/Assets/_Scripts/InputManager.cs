using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Player 1")]
    public float P1_LS_X;
    public float P1_LS_Y;
    public float P1_RS_X;
    public float P1_RS_Y;
    public float P1_Dpad_X;
    public float P1_Dpad_Y;
    public float P1_LT;
    public float P1_RT;
    public bool P1_LB;
    public bool P1_RB;
    public bool P1_A;
    public bool P1_B;
    public bool P1_X;
    public bool P1_Y;
    public bool P1_Start;

    [Header("Player 2")]
    public float P2_LS_X;
    public float P2_LS_Y;
    public float P2_RS_X;
    public float P2_RS_Y;
    public float P2_Dpad_X;
    public float P2_Dpad_Y;
    public float P2_LT;
    public float P2_RT;
    public bool P2_LB;
    public bool P2_RB;
    public bool P2_A;
    public bool P2_B;
    public bool P2_X;
    public bool P2_Y;
    public bool P2_Start;

    [Header("Controller Check")]
    public bool isXboxControllerP1;
    public bool isXboxControllerP2;
    public bool isPSControllerP1;
    public bool isPSControllerP2;
    public bool isKeyboardAndMouseP1;
    public bool isKeyboardAndMouseP2;

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
            if (names[0].Contains("XBOX 360") || names[0].Contains("Xbox One"))
            {
                Debug.Log(names[0] + " detected as joystick 1");
                isXboxControllerP1 = true;
                isKeyboardAndMouseP1 = false;
            }

            if (names[0].Contains("PLAYSTATION") || names[0].Contains("Wireless Controller"))
            {
                Debug.Log(names[0] + " detected as joystick 1");
                isPSControllerP1 = true;
                isKeyboardAndMouseP1 = false;
            }

            if (names[1].Contains("XBOX 360") || names[1].Contains("Xbox One"))
            {
                Debug.Log(names[1] + " detected as joystick 2");
                isXboxControllerP2 = true;
                isKeyboardAndMouseP2 = false;
            }

            if (names[1].Contains("PLAYSTATION") || names[1].Contains("Wireless Controller"))
            {
                Debug.Log(names[1] + " detected as joystick 2");
                isPSControllerP2 = true;
                isKeyboardAndMouseP2 = false;
            }
        }

        if (names.Length == 0)
        {
            Debug.Log("No controllers found, using keyboard and mouse");
            isXboxControllerP1 = false;
            isXboxControllerP2 = false;
            isPSControllerP1 = false;
            isPSControllerP2 = false;
            isKeyboardAndMouseP1 = true;
            isKeyboardAndMouseP2 = true;
        }

        if (names[0].Length == 0 && names[1].Length > 0)
        {
            Debug.Log("Can't find joystick 1, using keyboard and mouse");
            isXboxControllerP1 = false;
            isPSControllerP1 = false;
            isKeyboardAndMouseP1 = true;
        }

        if (names[1].Length == 0 && names[0].Length > 0)
        {
            Debug.Log("Can't find joystick 2, using keyboard and mouse");
            isXboxControllerP2 = false;
            isPSControllerP2 = false;
            isKeyboardAndMouseP2 = true;
        }
    }

    void ChooseInput()
    {
        if (isXboxControllerP1)
        {   
            P1_LS_X = Input.GetAxis("Xbox_P1_HorizontalLeft");
            P1_LS_Y = Input.GetAxis("Xbox_P1_VerticalLeft");
            P1_RS_X = Input.GetAxis("Xbox_P1_HorizontalRight");
            P1_RS_Y = Input.GetAxis("Xbox_P1_VerticalRight");
            P1_Dpad_X = Input.GetAxis("Xbox_P1_HorizontalDpad");
            P1_Dpad_Y = Input.GetAxis("Xbox_P1_VerticalDpad");
            P1_LT = Input.GetAxis("Xbox_P1_LT");
            P1_RT = Input.GetAxis("Xbox_P1_RT");
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
            P2_LS_X = Input.GetAxis("Xbox_P2_HorizontalLeft");
            P2_LS_Y = Input.GetAxis("Xbox_P2_VerticalLeft");
            P2_RS_X = Input.GetAxis("Xbox_P2_HorizontalRight");
            P2_RS_Y = Input.GetAxis("Xbox_P2_VerticalRight");
            P2_Dpad_X = Input.GetAxis("Xbox_P2_HorizontalDpad");
            P2_Dpad_Y = Input.GetAxis("Xbox_P2_VerticalDpad");
            P2_LT = Input.GetAxis("Xbox_P2_LT");
            P2_RT = Input.GetAxis("Xbox_P2_RT");
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
    }

    void SideCheck()
    {
        isLeftP1 = TestMenuScript.MS.isLeftP1; //Muista vaihtaa oikean scriptin nimiseksi
        isLeftP2 = TestMenuScript.MS.isLeftP2;
        isRightP1 = TestMenuScript.MS.isRightP1;
        isRightP2 = TestMenuScript.MS.isRightP2;
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
