using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BetterMainMenu : MonoBehaviour
{
    Settings settings;
    BetterMainMenu script;

    public GameObject openMenu;

    public GameObject mainMenuUI;
    public GameObject optionsMenuUI;

    [SerializeField]
    Animator cameraAnim;

    public Slider[] volumeSliders;

    public Animator anim;
    public GameObject currentNode;
    public Image tb1, tb2, tb3, tb4;

    [Header("-- Settings --")]
    public GameObject volumeSlider;
    public GameObject backButton;

    public float timer, defaultTimer, ver, hor;

    public bool canInteract;

    float dampTime = 0.2f;

    void Start()
    {
        openMenu = mainMenuUI;

        cameraAnim.SetBool("MainMenu", true);

        script = FindObjectOfType<BetterMainMenu>();
        settings = FindObjectOfType<Settings>();
        cameraAnim = Camera.main.GetComponent<Animator>();

        canInteract = true;

        timer = 0.5f;   //Tällä voi vaihtaa nopeutta millä hahmo vaihtaa miekan paikkaa
        defaultTimer = timer;
    }

    void Update()
    {
        ver = Input.GetAxisRaw("Vertical");
        hor = Input.GetAxisRaw("Horizontal");

        HandMove();
        CheckNode();
        TitleBars();
    }

    void HandMove()
    {
        if (currentNode == null)
        {
            currentNode = GameObject.Find("StartButton");
            anim.SetFloat("Blend", 1);

            canInteract = true;
        }

        if (currentNode.name == "StartButton" && ver < 0 && canInteract)
        {
            currentNode.name = "OptionsButton";
            canInteract = false;
        }

        if (currentNode.name == "OptionsButton" && ver < 0 && canInteract)
        {
            currentNode.name = "CreditsButton";
            canInteract = false;
        }

        if (currentNode.name == "CreditsButton" && ver < 0 && canInteract)
        {
            currentNode.name = "ExitButton";
            canInteract = false;
        }

        if (currentNode.name == "ExitButton" && ver < 0 && canInteract)
        {
            currentNode.name = "StartButton";
            canInteract = false;
        }

        if (currentNode.name == "StartButton" && ver > 0 && canInteract)
        {
            currentNode.name = "ExitButton";
            canInteract = false;
        }

        if (currentNode.name == "OptionsButton" && ver > 0 && canInteract)
        {
            currentNode.name = "StartButton";
            canInteract = false;
        }

        if (currentNode.name == "CreditsButton" && ver > 0 && canInteract)
        {
            currentNode.name = "OptionsButton";
            canInteract = false;
        }

        if (currentNode.name == "ExitButton" && ver > 0 && canInteract)
        {
            currentNode.name = "CreditsButton";
            canInteract = false;
        }

        if (currentNode.name == "VolumeSlider" && ver > 0)
        {
            currentNode.name = "BackButton";
            canInteract = false;
        }

        if (currentNode.name == "VolumeSlider" && ver < 0)
        {
            currentNode.name = "BackButton";
            canInteract = false;
        }

        if (currentNode.name == "VolumeSlider" && hor > 0 && AudioManager.instance.musicVolumePercent < 1)
        {
            //AudioManager.instance.AddVolume();
            canInteract = false;
        }

        if (currentNode.name == "VolumeSlider" && hor < 0 && AudioManager.instance.musicVolumePercent > 0)
        {
            //AudioManager.instance.LessVolume();
            canInteract = false;
        }

        if (currentNode.name == "BackButton" && ver > 0)
        {
            currentNode.name = "VolumeSlider";
            canInteract = false;
        }

        if (currentNode.name == "BackButton" && ver < 0)
        {
            currentNode.name = "VolumeSlider";
            canInteract = false;
        }

    }

    void CheckNode()
    {
        if (currentNode != null)
        {
            if (!canInteract)
            {
                timer = timer - Time.deltaTime;

                if (timer < 0)
                {
                    canInteract = true;
                }
            }

            if (canInteract)
            {
                timer = defaultTimer;
            }

            if (currentNode.name == "StartButton")
            {
                anim.SetFloat("Blend", 1, dampTime, Time.deltaTime);

                tb1.enabled = true;
                tb2.enabled = false;
                tb3.enabled = false;
                tb4.enabled = false;
            }

            if (currentNode.name == "OptionsButton")
            {
                anim.SetFloat("Blend", 0.35f, dampTime, Time.deltaTime);

                tb1.enabled = false;
                tb2.enabled = true;
                tb3.enabled = false;
                tb4.enabled = false;
            }

            if (currentNode.name == "CreditsButton")
            {
                anim.SetFloat("Blend", -0.35f, dampTime, Time.deltaTime);

                tb1.enabled = false;
                tb2.enabled = false;
                tb3.enabled = true;
                tb4.enabled = false;
            }

            if (currentNode.name == "ExitButton")
            {
                anim.SetFloat("Blend", -1, dampTime, Time.deltaTime);

                tb1.enabled = false;
                tb2.enabled = false;
                tb3.enabled = false;
                tb4.enabled = true;
            }
        }
    }

    void TitleBars()
    {
        if (openMenu == mainMenuUI)
        {
            if (!InputManager.IM.isOnlyKeyboard)
            {
                if (tb1.enabled && InputManager.IM.P1_A || tb1.enabled && InputManager.IM.P2_A)
                {
                    //Start game
                    Debug.Log("START GAME");
                }

                if (tb2.enabled && InputManager.IM.P1_A || tb2.enabled && InputManager.IM.P2_A)
                {
                    //Options
                    Debug.Log("OPTIONS");
                    Options();
                }

                if (tb3.enabled && InputManager.IM.P1_A || tb3.enabled && InputManager.IM.P2_A)
                {
                    //Credits
                    Debug.Log("CREDITS");
                }

                if (tb4.enabled && InputManager.IM.P1_A || tb4.enabled && InputManager.IM.P2_A)
                {
                    //Exit
                    Debug.Log("EXIT");
                    Application.Quit();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (tb1.enabled)
                    {
                        //Start game
                        Debug.Log("START GAME");
                    }
                    else if (tb2.enabled)
                    {
                        //Options
                        Debug.Log("OPTIONS");
                        Options();
                    }
                    else if (tb3.enabled)
                    {
                        //Credits
                        Debug.Log("CREDITS");
                    }
                    else if (tb4.enabled)
                    {
                        //Exit
                        Debug.Log("EXIT");
                        Application.Quit();
                    }
                }
            }
        }
        else if (openMenu == optionsMenuUI)
        {
            if (!InputManager.IM.isOnlyKeyboard)
            {
                if (currentNode == volumeSlider)
                {
                    if(hor > 0 && AudioManager.instance.musicVolumePercent < 1)
                    {
                        //AudioManager.instance.AddVolume();
                    }
                }
                else if (currentNode == backButton)
                {
                    if(InputManager.IM.P1_A || InputManager.IM.P2_A)
                    {
                        Back();
                    }
                }
            }
            else
            {
                if(currentNode == volumeSlider)
                {
                    // change volume logic
                }
                else if(currentNode == backButton)
                {
                    if(Input.GetKeyDown(KeyCode.Return))
                    {
                        Back(); 
                    }
                }
            }
        }
    }

    public void Back()
    {
        anim.SetBool("SettingsMenu", false);
        anim.SetBool("MainMenu", true);

        openMenu = mainMenuUI;
        currentNode = GameObject.Find("StartButton");
    }

    public void Play()
    {
        Debug.Log("START");
    }

    public void Options()
    {
        openMenu = optionsMenuUI;
        currentNode = volumeSlider;

        cameraAnim.SetBool("MainMenu", false);
        cameraAnim.SetBool("SettingsMenu", true);
    }

    public void Credits()
    {
        cameraAnim.SetTrigger("Credits");
    }

    public void Quit()
    {
        Application.Quit();
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