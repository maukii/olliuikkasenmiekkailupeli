using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {

    public Player player { get; private set; }

    public string horizontal;
    public string vertical;
    public string aButton;

    public int controllerNumber { get; set; }
    public float Horizontal { get; set; }

    private void Start()
    {
        player = GetComponent<Player>();
        SetControllerNumber(player.playerNumber);
    }

    public void SetControllerNumber(int number)
    {
        controllerNumber = number;
        horizontal = "Horizontal_P" + controllerNumber;
        vertical = "Vertical_P" + controllerNumber;
        aButton = "aButton_P" + controllerNumber;
    }

    public bool ButtonIsDown()
    {
        if(Input.GetButtonDown(aButton))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Update()
    {
        if(controllerNumber > 0)
        {
            Horizontal = Input.GetAxisRaw(horizontal);
        }
    }

}
