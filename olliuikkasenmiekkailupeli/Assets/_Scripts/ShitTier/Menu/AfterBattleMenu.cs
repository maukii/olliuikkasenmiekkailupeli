using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AfterBattleMenu : MonoBehaviour
{
    [SerializeField] GameObject UI;

    [SerializeField] GameObject[] buttons = new GameObject[2];

    [SerializeField] Sprite[] controllerButtons = new Sprite[2], keyboardButtons = new Sprite[2];
    [SerializeField] bool usingController;

    private void Start()
    {
        UI.SetActive(false);

        if (!InputManager.IM.isOnlyKeyboard)
            usingController = true;
        else
            usingController = false;

        if (usingController)
        {
            // use controller images
            buttons[0].GetComponent<Image>().sprite = controllerButtons[0];
            buttons[1].GetComponent<Image>().sprite = controllerButtons[1];
        }
        else
        {
            // use keyboard images
            buttons[1].GetComponent<Image>().sprite = keyboardButtons[0];
            buttons[2].GetComponent<Image>().sprite = keyboardButtons[1];
        }
    }

    private void Update()
    {

        if (GameHandler.instance.BattleEnded)
        {
            UI.SetActive(true);

            if (usingController)
            {
                if(InputManager.IM.P1_A || InputManager.IM.P2_A)
                {
                    PlayAgain();
                }
                else if(InputManager.IM.P1_B || InputManager.IM.P2_B)
                {
                    MainMenu();
                }
            }
            else
            {
                if(Input.GetKeyDown(KeyCode.Return))
                {
                    PlayAgain();
                }
                else if(Input.GetKeyDown(KeyCode.Escape))
                {
                    MainMenu();
                }
            }
        }
    }

    void PlayAgain()
    {
        Debug.Log("PlayAgain");
        GameHandler.instance.battleStarted = false;
        GameHandler.instance.battleEnded = false;
        //LevelChanger.instance.FadeToCharacterSelect();
        LevelChanger.instance.FadeToPlayAgain();
    }

    void MainMenu()
    {
        Debug.Log("BackToMain");
        MainMenuController.MMC.isTutorial = false;
        GameHandler.instance.BattleStarted = false;
        LevelChanger.instance.FadeToMain();
    }

}
