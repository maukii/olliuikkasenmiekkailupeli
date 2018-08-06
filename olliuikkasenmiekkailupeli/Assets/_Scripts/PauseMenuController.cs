using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    public KeyCode startButton; // can have own for p1 and p2

    [SerializeField] GameObject PauseMenuUI, MovelistUI, OptionsUI;
    public static bool gameIsPaused = false;

    [SerializeField] Menu activeMenu;
    [SerializeField] GameObject activeNode;

    [SerializeField]
    float timer = 0.5f, defaultTimer;
        
    float hor, ver;
    int index;
    public bool canInteract;

    enum Menu { PauseMenu, Movelist, Options, };
    [SerializeField] GameObject[] pauseMenuNodes, optionsNodes;
    [SerializeField] GameObject[] pauseMenuHighlights, optionsHighlights;

    [SerializeField] Slider[] volumeSliders;

    void Start()
    {
        foreach (Slider slider in volumeSliders)
        {
            slider.value = 0.1f;
        }

        PauseMenuUI.gameObject.SetActive(false);
        MovelistUI.gameObject.SetActive(false);
        OptionsUI.gameObject.SetActive(false);

        DisableHighlights(Menu.PauseMenu);
        DisableHighlights(Menu.Options);

        activeMenu = Menu.PauseMenu;
        activeNode = pauseMenuNodes[0];
        defaultTimer = timer;

        volumeSliders[0].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[1].value = AudioManager.instance.sfxVolumePercent;
    }

    private void Update()
    {
        if(GameHandler.instance.BattleStarted)
        {
            MenuLogic();
        }

        GetInput();
        ChangeNode();
        ButtonLogic();
    }

    void DisableHighlights(Menu menu)
    {
        if (menu == Menu.PauseMenu)
        {
            for (int i = 0; i < pauseMenuHighlights.Length; i++)
            {
                pauseMenuHighlights[i].SetActive(false);
            }
        }
        else if (menu == Menu.Options)
        {
            for (int i = 0; i < optionsHighlights.Length; i++)
            {
                optionsHighlights[i].SetActive(false);
            }
        }
    }

    private void MenuLogic()
    {
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(startButton))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    private void ChangeNode()
    {
        if (!canInteract)
        {
            timer -= Time.unscaledDeltaTime;

            if (timer <= 0)
            {
                timer = defaultTimer;
                canInteract = true;
            }
        }

        if (activeMenu == Menu.PauseMenu)
        {
            if (ver >= .5f && canInteract)
            {
                ToggleDown();
            }
            else if (ver <= -0.5f && canInteract)
            {
                ToggleUp();
            }

        }
        else if (activeMenu == Menu.Options)
        {
            if (ver >= .5f && canInteract)
            {
                ToggleDown();
            }
            else if (ver <= -.5f && canInteract)
            {
                ToggleUp();
            }
        }
    }

    void ToggleUp()
    {
        index++;

        if (activeMenu == Menu.PauseMenu)
        {
            if (index > pauseMenuNodes.Length - 1)
            {
                index = 0;
            }
            canInteract = false;

            DisableHighlights(Menu.PauseMenu);
            pauseMenuHighlights[index].SetActive(true);
        }

        if (activeMenu == Menu.Options)
        {
            if (index > optionsNodes.Length - 1)
            {
                index = 0;
            }
            canInteract = false;

            DisableHighlights(Menu.Options);
            optionsHighlights[index].SetActive(true);
        }
    }

    void ToggleDown()
    {
        index--;

        if (activeMenu == Menu.PauseMenu)
        {
            if (index < 0)
            {
                index = pauseMenuNodes.Length - 1;
            }
            canInteract = false;

            DisableHighlights(Menu.PauseMenu);
            pauseMenuHighlights[index].SetActive(true);
        }

        if (activeMenu == Menu.Options)
        {
            if (index < 0)
            {
                index = optionsNodes.Length - 1;
            }
            canInteract = false;

            DisableHighlights(Menu.Options);
            optionsHighlights[index].SetActive(true);
        }
    }

    private void ButtonLogic()
    {
        if (activeMenu == Menu.PauseMenu) // pauseMenu
        {
            activeNode = pauseMenuNodes[index];

            if (!InputManager.IM.isOnlyKeyboard) // controller
            {
                if ((InputManager.IM.P1_A || InputManager.IM.P2_A || Input.GetKeyDown(KeyCode.Return)) && canInteract)
                {
                    if (activeNode == pauseMenuNodes[0])
                    {
                        // RESUME
                        Debug.Log("Resume");
                        Resume();
                        canInteract = false;
                    }
                    else if (activeNode == pauseMenuNodes[1])
                    {
                        // MOVELIST
                        Debug.Log("Movelist");
                        Movelist();
                        canInteract = false;
                    }
                    else if (activeNode == pauseMenuNodes[2])
                    {
                        // OPTIONS
                        Debug.Log("Options");
                        Options();
                        canInteract = false;
                    }
                    else if (activeNode == pauseMenuNodes[3])
                    {
                        // EXIT
                        Exit();
                        canInteract = false;
                    }
                }
            }
            else // keyboard
            {
                if (Input.GetKeyDown(KeyCode.Return) && canInteract)
                {
                    if (activeNode == pauseMenuNodes[0])
                    {
                        // RESUME
                        Debug.Log("Resume");
                        Resume();
                        canInteract = false;
                    }
                    else if (activeNode == pauseMenuNodes[1])
                    {
                        // MOVELIST
                        Debug.Log("Movelist");
                        Movelist();
                        canInteract = false;
                    }
                    else if (activeNode == pauseMenuNodes[2])
                    {
                        // OPTIONS
                        Debug.Log("Options");
                        Options();
                        canInteract = false;
                    }
                    else if (activeNode == pauseMenuNodes[3])
                    {
                        // EXIT
                        Exit();
                        canInteract = false;
                    }
                }
            }
        }

        else if (activeMenu == Menu.Movelist)
        {
            if ((InputManager.IM.P1_A || InputManager.IM.P2_A || Input.GetKeyDown(KeyCode.Return)) && canInteract)
            {
                Back(1);
            }
        }

        else if (activeMenu == Menu.Options) // settings
        {
            activeNode = optionsNodes[index];

            if (activeNode == optionsNodes[0]) // volume
            {
                if (hor >= .5f && volumeSliders[0].value < 1 && canInteract)
                {
                    AudioManager.instance.AddVolume(0);
                    canInteract = false;
                    volumeSliders[index].value = AudioManager.instance.musicVolumePercent;
                }
                if (hor <= -.5 && volumeSliders[0].value > 0 && canInteract)
                {
                    AudioManager.instance.LessVolume(0);
                    canInteract = false;
                    volumeSliders[index].value = AudioManager.instance.musicVolumePercent;
                }
            }
            else if (activeNode == optionsNodes[1]) // sfx
            {
                if (hor >= .5f && volumeSliders[1].value < 1 && canInteract)
                {
                    AudioManager.instance.AddVolume(1);
                    canInteract = false;
                    volumeSliders[index].value = AudioManager.instance.sfxVolumePercent;
                }
                if (hor <= -.5f && volumeSliders[1].value > 0 && canInteract)
                {
                    AudioManager.instance.LessVolume(1);
                    canInteract = false;
                    volumeSliders[index].value = AudioManager.instance.sfxVolumePercent;
                }
            }
            else if (activeNode == optionsNodes[2])
            {
                if ((InputManager.IM.P1_A || InputManager.IM.P2_A || Input.GetKeyDown(KeyCode.Return)) && canInteract)
                {
                    Back(2);
                }
            }
        }
    }

    public void Back(int num)
    {
        index = num;
        timer = 1f;

        PauseMenuUI.gameObject.SetActive(true);
        MovelistUI.gameObject.SetActive(false);
        OptionsUI.gameObject.SetActive(false);

        activeMenu = Menu.PauseMenu;

        DisableHighlights(Menu.PauseMenu);
        pauseMenuHighlights[index].SetActive(true);
        activeNode = pauseMenuNodes[index];

        canInteract = false;
    }

    public void Pause()
    {
        Debug.Log("Pause");
        PauseMenuUI.gameObject.SetActive(true);
        pauseMenuHighlights[index].SetActive(true);
        gameIsPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        Debug.Log("Resume");
        PauseMenuUI.gameObject.SetActive(false);
        gameIsPaused = false;
        Time.timeScale = 1f;
    }

    public void Movelist()
    {
        timer = 1f;

        PauseMenuUI.gameObject.SetActive(false);
        MovelistUI.gameObject.SetActive(true);

        activeMenu = Menu.Movelist;

        Debug.Log("Movelist");
        canInteract = false;
    }

    public void Options()
    {
        index = 0;
        timer = 1f;

        PauseMenuUI.gameObject.SetActive(false);
        OptionsUI.gameObject.SetActive(true);

        activeMenu = Menu.Options;

        DisableHighlights(Menu.Options);
        optionsHighlights[index].SetActive(true);

        canInteract = false;
    }

    public void Exit()
    {
        Debug.Log("Exit");
        SceneManager.LoadScene(0);  //TOIMII, MUTTA RIKKOO MENU-UKON KÄDEN
    }

    void GetInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
    }

    public void SetMasterVolume(float value)
    {
        value = AudioManager.instance.masterVolumePercent;
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        value = AudioManager.instance.musicVolumePercent;
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        value = AudioManager.instance.sfxVolumePercent;
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }
}