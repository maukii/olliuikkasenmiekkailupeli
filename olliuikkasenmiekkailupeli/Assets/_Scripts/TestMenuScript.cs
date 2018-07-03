using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestMenuScript : MonoBehaviour
{
    public Text P1_ControllerType, P2_ControllerType, P1_Ready, P2_Ready;
    public Transform P1, P2;

    public float speed = 15f;

    void Start ()
    {
        
    }

	void Update ()
    {
        ControllerType();

        P1.transform.Rotate(Vector3.up, -speed * Time.deltaTime);
        P2.transform.Rotate(Vector3.up, speed * Time.deltaTime);

        if (InputManager.IM.P1_A)
        {
            P1_Ready.text = "READY!";
        }

        if (InputManager.IM.P2_A)
        {
            P2_Ready.text = "READY!";
        }
    }

    void ControllerType()
    {
        if (InputManager.IM.isXboxControllerP1 || InputManager.IM.isPSControllerP1)
        {
            //Ilmoita, että pelaaja 1 käyttää ohjainta
            P1_ControllerType.text = "Controller";
            //Vaihda hahmoa/muuta stuffia
        }

        if (InputManager.IM.isXboxControllerP2 || InputManager.IM.isPSControllerP2)
        {
            //Ilmoita, että pelaaja 2 käyttää ohjainta
            P2_ControllerType.text = "Controller";
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