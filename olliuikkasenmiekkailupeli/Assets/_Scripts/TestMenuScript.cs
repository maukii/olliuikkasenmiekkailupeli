using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestMenuScript : MonoBehaviour
{
    public Text P1_ControllerType, P2_ControllerType;

    void Start ()
    {

    }

	void Update ()
    {
        ControllerType();
    }

    void ControllerType()
    {
        if (InputManager.IM.isXboxControllerP1)
        {
            //Ilmoita, että pelaaja 1 käyttää Xbox-ohjainta
            P1_ControllerType.text = "P1 = Xbox";
            //Vaihda hahmoa/muuta stuffia
        }

        if (InputManager.IM.isXboxControllerP2)
        {
            //Ilmoita, että pelaaja 2 käyttää Xbox-ohjainta
            P2_ControllerType.text = "P2 = Xbox";
            //Vaihda hahmoa/muuta stuffia
        }

        if (InputManager.IM.isPSControllerP1)
        {
            //Ilmoita, että pelaaja 1 käyttää PlayStation-ohjainta
            P1_ControllerType.text = "P1 = PS4";
            //Vaihda hahmoa/muuta stuffia
        }

        if (InputManager.IM.isPSControllerP2)
        {
            //Ilmoita, että pelaaja 2 käyttää PlayStation-ohjainta
            P2_ControllerType.text = "P2 = PS4";
            //Vaihda hahmoa/muuta stuffia
        }

        if (InputManager.IM.isKeyboardAndMouseP1)
        {
            //Ilmoita, että pelaaja 1 käyttää näppäimistöä ja hiirtä
            P1_ControllerType.text = "No controllers detected, using keyboard and mouse";
            //Vaihda hahmoa/muuta stuffia
        }

        if (InputManager.IM.isKeyboardAndMouseP2)
        {
            //Ilmoita, että pelaaja 2 käyttää näppäimistöä ja hiirtä
            P2_ControllerType.text = "No controllers detected, using keyboard and mouse";
            //Vaihda hahmoa/muuta stuffia
        }
    }
}