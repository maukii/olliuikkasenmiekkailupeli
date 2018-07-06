using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenu, optionsMenu, playMenu;

    [SerializeField]
    GameObject menuCharacter;

    private void Start()
    {
        //mainMenu = GameObject.Find("MainMenu").gameObject;
        //optionsMenu = GameObject.Find("Options").gameObject;
        //playMenu = GameObject.Find("PlayMenu").gameObject;
        //blackScreen = GameObject.Find("Blackscreen").gameObject;
    }

    public void StartGame()
    {
        //mainMenu.SetActive(false);
        //optionsMenu.SetActive(false);
        //playMenu.SetActive(true);

        // TODO: save player modifications 

        menuCharacter.GetComponent<Animator>().CrossFade("MenuLunge", 0.2f);
        AudioManager.instance.PlaySound("swing");
        Invoke("LoadGameMenu", 1f);
    }

    void LoadGameMenu()
    {
        SceneManager.LoadScene("testifesti");
    }

    public void Options()
    {
        CloseAllMenus();
        optionsMenu.SetActive(true);
    }

    public void Back()
    {
        CloseAllMenus();
        mainMenu.SetActive(true);
    }

    public void CloseAllMenus()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        playMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

}
