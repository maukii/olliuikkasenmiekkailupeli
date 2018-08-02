using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestMenuScript : MonoBehaviour
{
    //Ensimmäinen pelaaja joka painaa A/X-nappia määritetään Pelaaja 1:ksi
    //Toinen pelaaja joka painaa A/X-nappia määritetään Pelaaja 2:ksi
    //Pelihahmojen tagit muutetaan MoveScriptissä
    //Tämä script varmaankin toiseen gameobjektiin kuin InputManager

    public Text L_ControllerType, R_ControllerType, L_CharacterChoose, R_CharacterChoose, L_Ready, R_Ready; //Muista laittaa tekstit, kuvat yms. näihin
    public Transform L, R; //Muista laittaa pelaajat näihin
    public float timerLeft = 0.5f;
    public float timerRight = 0.5f;

    public bool isLeftP1, isLeftP2, isRightP1, isRightP2, isLeftChoosing, isRightChoosing, isLeftReady, isRightReady;

    public static TestMenuScript MS;  //Vaihda nimi varsinaiseen scriptiin

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
        SideCheck();
        CharacterPick();
        PlayersReady();
        MoveCamera();
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
            L_ControllerType.text = "Press controller button ";
            R_ControllerType.text = "or mouse button to join";
        }

        if(InputManager.IM.isOnlyKeyboard)
        {
            L_ControllerType.text = "Press Q or keypad";        // what buttons join game?
            R_ControllerType.text = " Enter to join";        // what buttons join game?
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

        //Tässä pelaaja valitsee hahmonsa
    }

    void PlayersReady()
    {
        if (isLeftChoosing)
        {
            timerLeft -= Time.deltaTime;

            if (timerLeft <= 0.0f)
            {
                timerLeft = 0f;

                if (L.gameObject.tag == "Player 1" && InputManager.IM.P1_A)
                {
                    isLeftReady = true;
                    L.gameObject.GetComponent<PlayerInfo>().ready = true;
                    L.gameObject.GetComponent<PlayerInfo>().ChooseCharacter();
                    GameHandler.instance.SetPlayer1Model(L.gameObject.GetComponent<PlayerInfo>().modelIndex);
                }

                if (L.gameObject.tag == "Player 2" && InputManager.IM.P2_A)
                {
                    isLeftReady = true;
                    L.gameObject.GetComponent<PlayerInfo>().ready = true;
                    L.gameObject.GetComponent<PlayerInfo>().ChooseCharacter();
                    GameHandler.instance.SetPlayer2Model(L.gameObject.GetComponent<PlayerInfo>().modelIndex);
                }


            }
        }

        if (isRightChoosing)
        {
            timerRight -= Time.deltaTime;

            if (timerRight <= 0.0f)
            {
                timerRight = 0f;

                if (R.gameObject.tag == "Player 1" && InputManager.IM.P1_A)
                {
                    isRightReady = true;
                    R.gameObject.GetComponent<PlayerInfo>().ready = true;
                    R.gameObject.GetComponent<PlayerInfo>().ChooseCharacter();
                    GameHandler.instance.SetPlayer1Model(R.gameObject.GetComponent<PlayerInfo>().modelIndex);

                }

                if (R.gameObject.tag == "Player 2" && InputManager.IM.P2_A)
                {
                    isRightReady = true;
                    R.gameObject.GetComponent<PlayerInfo>().ready = true;
                    R.gameObject.GetComponent<PlayerInfo>().ChooseCharacter();
                    GameHandler.instance.SetPlayer2Model(R.gameObject.GetComponent<PlayerInfo>().modelIndex);
                }
            }
        }

        if (isLeftReady)
        {
            isLeftChoosing = false;
            L_CharacterChoose.enabled = false;
            L_Ready.enabled = true;

            if (L.gameObject.tag == "Player 1" && InputManager.IM.P1_B)
            {
                L_Ready.enabled = false;
                L_CharacterChoose.enabled = true;
                isLeftChoosing = true;
                isLeftReady = false;
                L.gameObject.GetComponent<PlayerInfo>().ready = false;

            }

            if (L.gameObject.tag == "Player 2" && InputManager.IM.P2_B)
            {
                L_Ready.enabled = false;
                L_CharacterChoose.enabled = true;
                isLeftChoosing = true;
                isLeftReady = false;
                L.gameObject.GetComponent<PlayerInfo>().ready = false;

            }
        }

        if (isRightReady)
        {
            isRightChoosing = false;
            R_CharacterChoose.enabled = false;
            R_Ready.enabled = true;

            if (R.gameObject.tag == "Player 1" && InputManager.IM.P1_B)
            {
                R_Ready.enabled = false;
                R_CharacterChoose.enabled = true;
                isRightChoosing = true;
                isRightReady = false;
                R.gameObject.GetComponent<PlayerInfo>().ready = false;
            }

            if (R.gameObject.tag == "Player 2" && InputManager.IM.P2_B)
            {
                R_Ready.enabled = false;
                R_CharacterChoose.enabled = true;
                isRightChoosing = true;
                isRightReady = false;
                R.gameObject.GetComponent<PlayerInfo>().ready = false;
            }
        }
    }

    public float delayToLoadNextScene = 1f;

    void MoveCamera()
    {
        if (isLeftReady && isRightReady)
        {
            L_Ready.enabled = false;
            R_Ready.enabled = false;

            Camera.main.GetComponent<PlayableDirector>().Play();

            LevelChanger.instance.FadeToNextLevel();
            AudioManager.instance.FadeOutMusic();
        }
    }
}