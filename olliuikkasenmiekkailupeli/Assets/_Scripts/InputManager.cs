using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool isXboxControllerP1, isXboxControllerP2, isPS4ControllerP1, isPS4ControllerP2;

    private int Xbox_One_Controller = 0;
    private int PS4_Controller = 0;

    void Update()
    {
        string[] names = Input.GetJoystickNames();
        for (int x = 0; x < names.Length; x++)
        {
            print(names[x].Length);
            if (names[x].Length == 19)
            {
                PS4_Controller = 1;
                Xbox_One_Controller = 0;
            }
            if (names[x].Length == 33)
            {
                PS4_Controller = 0;
                Xbox_One_Controller = 1;
            }
        }
        if (Xbox_One_Controller == 1)
        {
            Debug.Log("Xbox controller detected");
            isXboxControllerP1 = true;
        }
        else if (PS4_Controller == 1)
        {
            Debug.Log("PS4 controller detected");
            isPS4ControllerP1 = true;
        }
        else
        {
            Debug.Log("No controllers detected");
            isXboxControllerP1 = false;
            isPS4ControllerP1 = false;
        }
    }
}

/*
        ----- Xbox -----
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
    
        ----- PS4 -----
        LS X axis = X axis
        LS Y axis = Y axis (inverted?)
        RS X axis = 3rd axis
        RS Y axis = 4th axis
        D-pad X axis = 7th axis
        D-pad Y axis = 8th axis (inverted?)
        L2 = 5th axis (-1.0f to 1.0f range, unpressed is -1.0f)
        R2 = 6th axis (-1.0f to 1.0f range, unpressed is -1.0f)

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

    Xbox + Xbox
    Xbox + PS4
    PS4 + PS4
    
    if (isXboxControllerP1 == true)
    {
        
    }

    if (isXboxControllerP2 == true)
    {
        
    }

    if (isPS4ControllerP1 == true)
    {
        
    }

    if (isPS4ControllerP2 == true)
    {
        
    }

    */