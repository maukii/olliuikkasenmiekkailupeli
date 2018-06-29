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

    public static InputManager IM;

    void Start()
    {
        IM = this;
        ControllerCheck();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.F1))
        {
            ControllerCheck();
        }

        ChooseInput();
    }

    void ControllerCheck()
    {
        string[] names = Input.GetJoystickNames();

        if (names.Length > 0)
        {
            if (names[0].Contains("XBOX 360") || names[0].Contains("Xbox One"))
            {
                Debug.Log("Xbox controller detected as joystick 1");
                isXboxControllerP1 = true;
            }

            if (names[0].Contains("PLAYSTATION") || names[0].Contains("Wireless Controller"))
            {
                Debug.Log("PlayStation controller detected as joystick 1");
                isPSControllerP1 = true;
            }

            if (names[1].Contains("XBOX 360") || names[1].Contains("Xbox One"))
            {
                Debug.Log("Xbox controller detected as joystick 2");
                isXboxControllerP2 = true;
            }

            if (names[1].Contains("PLAYSTATION") || names[1].Contains("Wireless Controller"))
            {
                Debug.Log("PlayStation controller detected as joystick 2");
                isPSControllerP2 = true;
            }
        }

        else
        {
            Debug.Log("No controllers found");
            isXboxControllerP1 = false;
            isXboxControllerP2 = false;
            isPSControllerP1 = false;
            isPSControllerP2 = false;
        }

        if (names[0].Length == 0)
        {
            Debug.Log("Can't find joystick 1");
            isXboxControllerP1 = false;
            isPSControllerP1 = false;
        }

        if (names[1].Length == 0)
        {
            Debug.Log("Can't find joystick 2");
            isXboxControllerP2 = false;
            isPSControllerP2 = false;
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

            /*
            LS X axis = X axis          Input.GetAxis("Xbox_P1_HorizontalLeft")
            LS Y axis = Y axis          Input.GetAxis("Xbox_P1_VerticalLeft")
            RS X axis = 4th axis        Input.GetAxis("Xbox_P1_HorizontalRight")
            RS Y axis = 5th axis        Input.GetAxis("Xbox_P1_VerticalRight")
            D-pad X axis = 6th axis     Input.GetAxis("Xbox_P1_HorizontalDpad")
            D-pad Y axis = 7th axis     Input.GetAxis("Xbox_P1_VerticalDpad")
            Triggers? = 3rd axis        Input.GetAxis("Xbox_P1_LT_RT")
            LT = 9th axis               Input.GetAxis("Xbox_P1_LT")
            RT = 10th axis              Input.GetAxis("Xbox_P1_RT")

            A button = Button 0         Input.GetButton("Xbox_P1_A")
            B button = Button 1         Input.GetButton("Xbox_P1_B")
            X button = Button 2         Input.GetButton("Xbox_P1_X")
            Y button = Button 3         Input.GetButton("Xbox_P1_Y")
            LB = Button 4               Input.GetButton("Xbox_P1_LB")
            RB = Button 5               Input.GetButton("Xbox_P1_RB")
            Back button = Button 6      Input.GetButton("Xbox_P1_Back")
            Start button = Button 7     Input.GetButton("Xbox_P1_Start")
            LS = Button 8               Input.GetButton("Xbox_P1_LS")
            RS = Button 9               Input.GetButton("Xbox_P1_RS")
            */
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

            /*
            LS X axis = X axis          Input.GetAxis("Xbox_P2_HorizontalLeft")
            LS Y axis = Y axis          Input.GetAxis("Xbox_P2_VerticalLeft")
            RS X axis = 4th axis        Input.GetAxis("Xbox_P2_HorizontalRight")
            RS Y axis = 5th axis        Input.GetAxis("Xbox_P2_VerticalRight")
            D-pad X axis = 6th axis     Input.GetAxis("Xbox_P2_HorizontalDpad")
            D-pad Y axis = 7th axis     Input.GetAxis("Xbox_P2_VerticalDpad")
            Triggers? = 3rd axis        Input.GetAxis("Xbox_P2_LT_RT")
            LT = 9th axis               Input.GetAxis("Xbox_P2_LT")
            RT = 10th axis              Input.GetAxis("Xbox_P2_RT")

            A button = Button 0         Input.GetButton("Xbox_P2_A")
            B button = Button 1         Input.GetButton("Xbox_P2_B")
            X button = Button 2         Input.GetButton("Xbox_P2_X")
            Y button = Button 3         Input.GetButton("Xbox_P2_Y")
            LB = Button 4               Input.GetButton("Xbox_P2_LB")
            RB = Button 5               Input.GetButton("Xbox_P2_RB")
            Back button = Button 6      Input.GetButton("Xbox_P2_Back")
            Start button = Button 7     Input.GetButton("Xbox_P2_Start")
            LS = Button 8               Input.GetButton("Xbox_P2_LS")
            RS = Button 9               Input.GetButton("Xbox_P2_RS")
            */
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

            /*
            LS X axis = X axis                                          Input.GetAxis("PS_P1_HorizontalLeft")
            LS Y axis = Y axis                                          Input.GetAxis("PS_P1_VerticalLeft")
            RS X axis = 3rd axis                                        Input.GetAxis("PS_P1_HorizontalRight")
            RS Y axis = 6th axis                                        Input.GetAxis("PS_P1_VerticalRight")
            D-pad X axis = 7th axis                                     Input.GetAxis("PS_P1_HorizontalDpad")
            D-pad Y axis = 8th axis                                     Input.GetAxis("PS_P1_VerticalDpad")
            L2 = 4th axis (-1.0f to 1.0f range, unpressed is -1.0f)     Input.GetAxis("PS_P1_L2")
            R2 = 5th axis (-1.0f to 1.0f range, unpressed is -1.0f)     Input.GetAxis("PS_P1_R2")

            Square = Button 0                                           Input.GetButton("PS_P1_Square")
            X = Button 1                                                Input.GetButton("PS_P1_X")
            Circle = Button 2                                           Input.GetButton("PS_P1_Circle")
            Triangle = Button 3                                         Input.GetButton("PS_P1_Triangle")
            L1 = Button 4                                               Input.GetButton("PS_P1_L1")
            R1 = Button 5                                               Input.GetButton("PS_P1_R1")
            L2 = Button 6                                               Input.GetButton("PS_P1_L2")
            R2 = Button 7                                               Input.GetButton("PS_P1_R2")
            Share = Button 8                                            Input.GetButton("PS_P1_Share")
            Options = Button 9                                          Input.GetButton("PS_P1_Options")
            L3 = Button 10                                              Input.GetButton("PS_P1_L3")
            R3 = Button 11                                              Input.GetButton("PS_P1_R3")
            PS = Button 12                                              Input.GetButton("PS_P1_PS")
            PadPress = Button 13                                        Input.GetButton("PS_P1_PadPress")
            */
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

            /*
            LS X axis = X axis                                          Input.GetAxis("PS_P2_HorizontalLeft")
            LS Y axis = Y axis                                          Input.GetAxis("PS_P2_VerticalLeft")
            RS X axis = 3rd axis                                        Input.GetAxis("PS_P2_HorizontalRight")
            RS Y axis = 6th axis                                        Input.GetAxis("PS_P2_VerticalRight")
            D-pad X axis = 7th axis                                     Input.GetAxis("PS_P2_HorizontalDpad")
            D-pad Y axis = 8th axis                                     Input.GetAxis("PS_P2_VerticalDpad")
            L2 = 4th axis (-1.0f to 1.0f range, unpressed is -1.0f)     Input.GetAxis("PS_P2_L2")
            R2 = 5th axis (-1.0f to 1.0f range, unpressed is -1.0f)     Input.GetAxis("PS_P2_R2")

            Square = Button 0                                           Input.GetButton("PS_P2_Square")
            X = Button 1                                                Input.GetButton("PS_P2_X")
            Circle = Button 2                                           Input.GetButton("PS_P2_Circle")
            Triangle = Button 3                                         Input.GetButton("PS_P2_Triangle")
            L1 = Button 4                                               Input.GetButton("PS_P2_L1")
            R1 = Button 5                                               Input.GetButton("PS_P2_R1")
            L2 = Button 6                                               Input.GetButton("PS_P2_L2")
            R2 = Button 7                                               Input.GetButton("PS_P2_R2")
            Share = Button 8                                            Input.GetButton("PS_P2_Share")
            Options = Button 9                                          Input.GetButton("PS_P2_Options")
            L3 = Button 10                                              Input.GetButton("PS_P2_L3")
            R3 = Button 11                                              Input.GetButton("PS_P2_R3")
            PS = Button 12                                              Input.GetButton("PS_P2_PS")
            PadPress = Button 13                                        Input.GetButton("PS_P2_PadPress")
            */
        }
    }
}