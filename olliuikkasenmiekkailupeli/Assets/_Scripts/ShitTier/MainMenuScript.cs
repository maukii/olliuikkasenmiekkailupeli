using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public Text player1, player2;
    public bool player1ready, player2ready;

    [SerializeField]
    GameObject mainMenu, optionsMenu, playMenu;

    public void FirstClick()
    {
        player1.text = "";
        player1ready = true;
    }

    public void SecondClick()
    {
        player2.text = "";
        player2ready = true;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        playMenu.SetActive(true);
    }

    public void Options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
        playMenu.SetActive(false);
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        playMenu.SetActive(false);
        player1ready = false;
        player2ready = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

}
