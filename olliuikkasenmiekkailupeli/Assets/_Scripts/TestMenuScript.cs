using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestMenuScript : MonoBehaviour
{
    //Ensimmäinen pelaaja joka painaa A/X-nappia määritetään Pelaaja 1:ksi
    //Toinen pelaaja joka painaa A/X-nappia määritetään Pelaaja 2:ksi
    //Pelihahmojen tagit muutetaan MoveScriptissä

    public Text L_ControllerType, R_ControllerType, L_CharacterChoose, R_CharacterChoose, L_Ready, R_Ready;
    public float timerLeft = 0.5f;
    public float timerRight = 0.5f;

    public bool isLeftP1, isLeftP2, isRightP1, isRightP2, isLeftChoosing, isRightChoosing, isLeftReady, isRightReady;

    public static TestMenuScript MS;

    void Start()
    {
        MS = this;

        L_CharacterChoose.enabled = false;
        R_CharacterChoose.enabled = false;
        L_Ready.enabled = false;
        R_Ready.enabled = false;
    }

    void Update()
    {
        ControllerType();
        SideCheck();            //BUGI! Toinen pelaaja voi painaa toisen readyksi, jos ei ole vielä itse painanut.
        CharacterPick();
        PlayersReady();

        //Heitä oikeat tagit InputManageriin
    }

    void ControllerType()
    {
        //Tänne mahdolliset ohjain tai näppis/hiiri-jutut

        if (InputManager.IM.isXboxControllerP1 || InputManager.IM.isPSControllerP1 || InputManager.IM.isXboxControllerP2 || InputManager.IM.isPSControllerP2)
        {
            L_ControllerType.text = "Press A / X button to join";
            R_ControllerType.text = "Press A / X button to join";
        }

        if (InputManager.IM.isKeyboardAndMouseP1 || InputManager.IM.isKeyboardAndMouseP2)
        {
            L_ControllerType.text = "Please insert";
            R_ControllerType.text = "two controllers";
        }
    }

    void SideCheck()
    {
        if (InputManager.IM.P1_A && isLeftP1 == false && isRightP1 == false)
        {
            //P1 = left
            isLeftP1 = true;
            isRightP1 = false;
        }

        if (InputManager.IM.P1_A && isLeftP2 == true && isRightP1 == false)
        {
            //P1 = right
            isLeftP1 = false;
            isRightP1 = true;
        }

        if (InputManager.IM.P2_A && isLeftP2 == false && isRightP2 == false)
        {
            //P2 = left
            isLeftP2 = true;
            isRightP2 = false;
        }

        if (InputManager.IM.P2_A && isLeftP1 == true && isRightP2 == false)
        {
            //P2 = right
            isLeftP2 = false;
            isRightP2 = true;
        }
    }

    void CharacterPick()
    {
        //Tässä pelaaja valitsee hahmonsa

        if (isLeftP1 || isLeftP2)
        {
            L_ControllerType.enabled = false;
            L_CharacterChoose.enabled = true;

            isLeftChoosing = true;
        }

        if (isRightP1 || isRightP2)
        {
            R_ControllerType.enabled = false;
            R_CharacterChoose.enabled = true;

            isRightChoosing = true;
        }
    }

    void PlayersReady()
    {
        if (isLeftChoosing)
        {
            timerLeft -= Time.deltaTime;

            if (timerLeft <= 0.0f)
            {
                timerLeft = 0f;

                if (InputManager.IM.P1_A || InputManager.IM.P2_A) //Tässä aiemmin mainittu bugi... mieti miten erotellaan inputit
                {
                    isLeftReady = true;
                }
            }
        }

        if (isRightChoosing)
        {
            timerRight -= Time.deltaTime;

            if (timerRight <= 0.0f)
            {
                timerRight = 0f;

                if (InputManager.IM.P1_A || InputManager.IM.P2_A)
                {
                    isRightReady = true;
                }
            }
        }

        if (isLeftReady)
        {
            isLeftChoosing = false;
            L_CharacterChoose.enabled = false;
            L_Ready.enabled = true;
        }

        if (isRightReady)
        {
            isRightChoosing = false;
            R_CharacterChoose.enabled = false;
            R_Ready.enabled = true;
        }
    }
}