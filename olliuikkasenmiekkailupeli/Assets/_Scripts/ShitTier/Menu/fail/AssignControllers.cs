using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssignControllers : MonoBehaviour {

    public List<int> assignedControllers = new List<int>();
    public PlayerPanel[] playerPanels;

    Player player;
    PlayerInput input;

    private void Awake()
    {
        playerPanels = FindObjectsOfType<PlayerPanel>().OrderBy(t => t.playerNumber).ToArray();
    }

    public void Update()
    {
        for (int i = 1; i <= 2; i++)
        {
            if (assignedControllers.Contains(i))
                continue;
        
            if(Input.GetButton("aButton_P" + i))
            {
                AddPlayerController(i);
                break;
            }
        }

    }

    public void AddPlayerController(int controller)
    {
        assignedControllers.Add(controller);
        for (int i = 1; i < playerPanels.Length; i++)
        {
            if (playerPanels[i].hasControllerAssigned == false)
            {
                player.input.SetControllerNumber(controller);
                //return playerPanels[i].AssaignController(controller);
            }
        }
        //return null;
    }

}
