using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPanel : MonoBehaviour {

    public Player player { get; set; }
    public int playerNumber;
    public bool hasControllerAssigned;

    public void AssaignController(int controller)
    {
        Debug.Log("Setting Player to controller");
        hasControllerAssigned = true;
        player.input.SetControllerNumber(controller);
    }
}
