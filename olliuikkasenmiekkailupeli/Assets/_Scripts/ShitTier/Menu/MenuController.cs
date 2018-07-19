using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    float dampTime = 0.5f;

    [SerializeField]
    Slider[] volumeSliders;

    [SerializeField]
    GameObject mainMenu, settingsMenu;

    Animator anim;
    Animator characterAnim;

    [SerializeField]
    Menu activeMenu;

    [SerializeField]
    GameObject activeNode;

    float hor, ver, timer = 0.5f, defaultTimer;

    [SerializeField]
    int index;

    bool canInteract;

    enum Menu { MainMenu, Settings, };
    public GameObject[] mainmenuNodes, settingsNodes;
    public GameObject[] mainmenuHighlights, settingsHighlights;

    void Start()
    {
        DisableHighlights(Menu.MainMenu);
        DisableHighlights(Menu.Settings);
        mainmenuHighlights[index].SetActive(true);

        mainMenu.gameObject.SetActive(true);
        settingsMenu.gameObject.SetActive(false);

        anim = Camera.main.GetComponent<Animator>();
        characterAnim = GameObject.Find("miekkailija_v5.2").gameObject.GetComponent<Animator>();
        anim.SetBool("MainMenu", true);
        anim.SetBool("SettingsMenu", false);

        activeMenu = Menu.MainMenu;
        activeNode = mainmenuNodes[0];
        defaultTimer = timer;

        volumeSliders[0].value = AudioManager.instance.musicVolumePercent;
        volumeSliders[1].value = AudioManager.instance.sfxVolumePercent;
    }

    void Update()
    {
        GetInput();
        ChangeNode();
        ButtonLogic();
    }

    void DisableHighlights(Menu menu)
    {
        if(menu == Menu.MainMenu)
        {
            for (int i = 0; i < mainmenuHighlights.Length; i++)
            {
                mainmenuHighlights[i].SetActive(false);
            }
        }
        else if(menu == Menu.Settings)
        {
            for (int i = 0; i < settingsHighlights.Length; i++)
            {
                settingsHighlights[i].SetActive(false);
            }
        }
    }

    private void ChangeNode()
    {
        if (!canInteract)
        {
            timer = timer - Time.deltaTime;

            if (timer < 0)
            {
                canInteract = true;
            }

            if (canInteract)
                timer = defaultTimer;
        }

        if (activeMenu == Menu.MainMenu)
        {
            if (ver >= .5f && canInteract)
            {
                ToggleDown();
            }
            else if (ver <= -0.5f && canInteract)
            {
                ToggleUp();
            }

            if(activeNode == mainmenuNodes[0])
                characterAnim.SetFloat("Blend", 1, dampTime, Time.deltaTime);

            else if(activeNode == mainmenuNodes[1])
                characterAnim.SetFloat("Blend", 0.35f, dampTime, Time.deltaTime);

            else if (activeNode == mainmenuNodes[2])
                characterAnim.SetFloat("Blend", -0.35f, dampTime, Time.deltaTime);

            else if (activeNode == mainmenuNodes[3])
                characterAnim.SetFloat("Blend", -1, dampTime, Time.deltaTime);

        }
        else if (activeMenu == Menu.Settings) // make 2nd index or -->
        {
            if (ver >= .5f && canInteract)
            {
                ToggleDown();
            }
            else if(ver <= -.5f && canInteract)
            {
                ToggleUp();
            }
        }
    }

    void ToggleUp()
    {
        index++;
        if(activeMenu == Menu.MainMenu)
        { 
            if (index > mainmenuNodes.Length - 1)
            {
                index = 0;
            }
            canInteract = false;

            DisableHighlights(Menu.MainMenu);
            mainmenuHighlights[index].SetActive(true);
        }
        if(activeMenu == Menu.Settings)
        {
            if (index > settingsNodes.Length - 1)
            {
                index = 0;
            }
            canInteract = false;

            DisableHighlights(Menu.Settings);
            settingsHighlights[index].SetActive(true);
        }
    }

    void ToggleDown()
    {
        index--;
        if(activeMenu == Menu.MainMenu)
        {
            if(index < 0)
            {
                index = mainmenuNodes.Length - 1;
            }
            canInteract = false;

            DisableHighlights(Menu.MainMenu);
            mainmenuHighlights[index].SetActive(true);
        }
        if(activeMenu == Menu.Settings)
        {
            if (index < 0)
            {
                index = settingsNodes.Length - 1;
            }
            canInteract = false;

            DisableHighlights(Menu.Settings);
            settingsHighlights[index].SetActive(true);
        }
    }

    private void ButtonLogic()
    {
        if (activeMenu == Menu.MainMenu) // mainMenu
        {
            activeNode = mainmenuNodes[index];

            if (!InputManager.IM.isOnlyKeyboard) // controller
            {
                if ((InputManager.IM.P1_A || InputManager.IM.P2_A) && canInteract)
                {
                    if (activeNode == mainmenuNodes[0])
                    {
                        // START
                        Debug.Log("Start");
                        AudioManager.instance.FadeOutMusic();
                        Invoke("LoadNextScene", 1.5f);
                        canInteract = false;
                    }
                    else if (activeNode == mainmenuNodes[1])
                    {
                        // OPTIONS
                        Options();
                        Debug.Log("Options");
                        canInteract = false;
                    }
                    else if (activeNode == mainmenuNodes[2])
                    {
                        // CREDITS
                        Debug.Log("Credits");
                        canInteract = false;
                    }
                    else if (activeNode == mainmenuNodes[3])
                    {
                        // EXIT
                        Application.Quit();
                        canInteract = false;
                    }
                }
            }
            else // keyboard
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (activeNode == mainmenuNodes[0])
                    {
                        // START
                        Debug.Log("START");
                    }
                    else if (activeNode == mainmenuNodes[1])
                    {
                        // OPTIONS
                        Options();
                        Debug.Log("Options");
                    }
                    else if (activeNode == mainmenuNodes[2])
                    {
                        // CREDITS
                        Debug.Log("Credits");
                    }
                    else if (activeNode == mainmenuNodes[3])
                    {
                        // EXIT
                        Application.Quit();
                    }
                }
            }
        }
        else if (activeMenu == Menu.Settings) // settings
        {
            activeNode = settingsNodes[index];

            if (!InputManager.IM.isOnlyKeyboard)
            {
                if (activeNode == settingsNodes[0])
                {
                    if(hor >= 1 && volumeSliders[0].value < 1 && canInteract)
                    {
                        AudioManager.instance.AddVolume(0);
                        canInteract = false;
                    }
                    if(hor <= -1 && volumeSliders[0].value > 0 && canInteract)
                    {
                        AudioManager.instance.LessVolume(0);
                        canInteract = false;
                    }
                }
                else if(activeNode == settingsNodes[1])
                {
                    if (hor >= 1 && volumeSliders[1].value < 1 && canInteract)
                    {
                        AudioManager.instance.AddVolume(1);
                        canInteract = false;
                    }
                    if (hor <= -1 && volumeSliders[1].value > 0 && canInteract)
                    {
                        AudioManager.instance.LessVolume(1);
                        canInteract = false;
                    }
                }
                else if (activeNode == settingsNodes[2])
                {
                    if ((InputManager.IM.P1_A || InputManager.IM.P2_A) && canInteract)
                    {
                        Back();
                    }
                }
            }
            else // keyboard
            {
                if (activeNode == settingsNodes[1])
                {
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        Back();
                    }
                }
            }
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Options()
    {
        Invoke("DisableMain", 2);
        settingsMenu.gameObject.SetActive(true);

        activeMenu = Menu.Settings;

        DisableHighlights(Menu.Settings);
        settingsHighlights[index].SetActive(true);

        anim.SetBool("SettingsMenu", true);
        anim.SetBool("MainMenu", false);
        canInteract = false;
    }

    public void Back()
    {
        Invoke("DisableSettings", 2);
        mainMenu.gameObject.SetActive(true);

        activeMenu = Menu.MainMenu;

        DisableHighlights(Menu.MainMenu);
        mainmenuHighlights[index].SetActive(true);

        anim.SetBool("MainMenu", true);
        anim.SetBool("SettingsMenu", false);
        canInteract = false;
    }

    void DisableMain()
    {
        mainMenu.gameObject.SetActive(false);
    }

    void DisableSettings()
    {
        settingsMenu.gameObject.SetActive(false);
    }

    void GetInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }

}
